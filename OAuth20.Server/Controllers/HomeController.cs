﻿using Microsoft.AspNetCore.Mvc;
using OAuth20.Server.OauthRequest;
using OAuth20.Server.Services.CodeService;
using OAuth20.Server.Services;
using OAuth20.Server.Services.Users;

namespace OAuth20.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizeResultService _authorizeResultService;
        private readonly ICodeStoreService _codeStoreService;
        private readonly IUserManagerService _userManagerService;
        public HomeController(IHttpContextAccessor httpContextAccessor, IAuthorizeResultService authorizeResultService,
            ICodeStoreService codeStoreService, IUserManagerService userManagerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizeResultService = authorizeResultService;
            _codeStoreService = codeStoreService;
            _userManagerService = userManagerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Authorize(AuthorizationRequest authorizationRequest)
        {
            var result = _authorizeResultService.AuthorizeRequest(_httpContextAccessor, authorizationRequest);

            if (result.HasError)
                return RedirectToAction("Error", new { error = result.Error });

            var loginModel = new OpenIdConnectLoginRequest
            {
                RedirectUri = result.RedirectUri,
                Code = result.Code,
                RequestedScopes = result.RequestedScopes,
                Nonce = result.Nonce
            };


            return View("Login", loginModel);
        }

        public IActionResult Error(string error)
        {
            return View(error);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(OpenIdConnectLoginRequest loginRequest)
        {
            if (!loginRequest.IsValid())
                return RedirectToAction("Error", new { error = "invalid_request" });

            var userLoginResult = await _userManagerService.LoginUserByOpenIdAsync(loginRequest);

            if (userLoginResult.Succeeded)
            {
                var result = _codeStoreService.UpdatedClientDataByCode(loginRequest.Code, loginRequest.RequestedScopes,
                loginRequest.UserName, nonce: loginRequest.Nonce);
                if (result != null)
                {

                    loginRequest.RedirectUri = loginRequest.RedirectUri + "&code=" + loginRequest.Code;
                    return Redirect(loginRequest.RedirectUri);
                }
            }

            return RedirectToAction("Error", new { error = "invalid_request" });
        }

        public JsonResult Token()
        {
            var result = _authorizeResultService.GenerateToken(_httpContextAccessor);

            if (result.HasError)
                return Json("0");

            return Json(result);
        }

        [HttpPost]
        public JsonResult Token(TokenRequest tokenRequest)
        {
            var result = _authorizeResultService.GenerateToken(tokenRequest);

            if (result.HasError)
                return Json(new
                {
                    error = result.Error,
                    error_description = result.ErrorDescription
                });

            return Json(result);
        }
    }
}
