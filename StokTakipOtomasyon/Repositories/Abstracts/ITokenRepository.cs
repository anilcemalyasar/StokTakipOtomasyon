using Microsoft.AspNetCore.Identity;

namespace StokTakipOtomasyon.Repositories.Abstracts
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
