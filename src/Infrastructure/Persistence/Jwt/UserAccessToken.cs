using System.IdentityModel.Tokens.Jwt;

namespace Persistance.Jwt
{
    public class UserAccessToken
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }

        public UserAccessToken(JwtSecurityToken securityToken)
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            TokenType = "Bearer";
            ExpiresIn = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        }
    }
}
