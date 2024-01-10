﻿using Microsoft.AspNetCore.Mvc;
using OAuth20.Server.Services;
using OAuth20.Server.Validations;

namespace OAuth20.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenRevocationController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenRevocationValidation _tokenRevocationValidation;
        private readonly ITokenRevocationService _tokenRevocationService;
        public TokenRevocationController(IHttpContextAccessor httpContextAccessor,
            ITokenRevocationValidation tokenRevocationValidation,
            ITokenRevocationService tokenRevocationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenRevocationValidation = tokenRevocationValidation;
            _tokenRevocationService = tokenRevocationService;
        }

        [HttpPost("RevokToken")]
        public async Task<IActionResult> RevokToken()
        {
            var clientValidationResult = await _tokenRevocationValidation.ValidateAsync(_httpContextAccessor.HttpContext);
            if (!clientValidationResult.Succeeded)
                return Unauthorized(clientValidationResult.Error);

            var tokenRevokeResult = await _tokenRevocationService.RevokeTokenAsync(_httpContextAccessor.HttpContext,
                clientValidationResult.Client.ClientId);

            if (tokenRevokeResult.Succeeded)
                return Ok("token_revoked");
            return NotFound(tokenRevokeResult.Error);
        }
    }
}
