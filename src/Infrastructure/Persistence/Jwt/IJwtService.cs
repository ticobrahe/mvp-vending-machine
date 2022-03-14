using Domain.Entities;

namespace Persistance.Jwt
{
    public interface IJwtService
    {
        UserAccessToken GenerateAsync(User user);
    }
}
