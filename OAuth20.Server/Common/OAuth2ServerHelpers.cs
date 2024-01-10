namespace OAuth20.Server.Common
{
    public class OAuth2ServerHelpers
    {
        public static IList<string> CodeChallenegMethodsSupport = new List<string>()
        {
            Constants.Plain,
            Constants.SHA256
        };


        public static IList<string> OpenIdConnectScopes = new List<string>()
        {
            Constants.OpenId,
            Constants.Profile,
            Constants.Email,
            Constants.Address,
            Constants.Phone
        };
    }
}
