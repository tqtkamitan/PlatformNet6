using OAuth20.Server.Validations.Response;

namespace OAuth20.Server.Validations
{
    public interface ITokenRevocationValidation
    {
        Task<TokenRevocationValidationResponse> ValidateAsync(HttpContext context);
    }
}