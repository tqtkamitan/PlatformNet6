﻿using OAuth20.Server.OauthResponse;

namespace OAuth20.Server.Services
{
    public interface ITokenRevocationService
    {
        public async Task<TokenRecovationResponse> RevokeTokenAsync(HttpContext httpContext, string clientId)
    }
}