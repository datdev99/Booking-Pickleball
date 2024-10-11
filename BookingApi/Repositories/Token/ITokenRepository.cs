using Microsoft.AspNetCore.Identity;

namespace BookingApi.Repositories.Token
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
