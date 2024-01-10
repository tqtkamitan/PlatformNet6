namespace OAuth20.Server.Models
{
    public class ClientStore
    {
        public IEnumerable<Client> Clients = new[]
        {
            new Client
            {
                ClientName = "platformnet .Net 6",
                ClientId = "platformnet6",
                ClientSecret = "123456789",
                AllowedScopes = new[]{ "openid", "profile"},
                GrantType = GrantTypes.Code,
                IsActive = true,
                ClientUri = "https://localhost:7026",
                RedirectUri = "https://localhost:7026/signin-oidc"
            }
        };
    }
}
