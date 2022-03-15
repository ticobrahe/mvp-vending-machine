using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Persistance.Jwt
{
    public class UserAccessToken
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }

        public UserAccessToken(SecurityToken securityToken)
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            ExpiresIn = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        }
    }
}
