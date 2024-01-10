﻿using System.ComponentModel;

namespace OAuth20.Server.Models
{
    internal enum AuthorizationGrantTypesEnum: byte
    {
        [Description("code")]
        Code,

        [Description("Implicit")]
        Implicit,

        [Description("ClientCredentials")]
        ClientCredentials,

        [Description("refresh_token")] 
        RefreshToken,

        [Description("ResourceOwnerPassword")]
        ResourceOwnerPassword
    }
}
