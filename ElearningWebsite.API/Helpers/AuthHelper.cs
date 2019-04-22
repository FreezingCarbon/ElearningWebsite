using System.IdentityModel.Tokens.Jwt;

namespace ElearningWebsite.API.Helpers
{
    public class AuthHelper
    {
        public  AuthHelper() { }
        public bool BasicAuth(string tokenString, int userId, string claimName)
        {

            var token = tokenString.Remove(0, 7); // remove Bearer
            // Decode token
            var tokenHandler = new JwtSecurityTokenHandler(); 
            var jsonToken = tokenHandler.ReadJwtToken(token);
            foreach(var claim in jsonToken.Claims)
            {
                if(claim.Type == "nameid") {
                    if(int.Parse(claim.Value) != userId) {
                        return false;
                    }
                }
                if(claim.Type == "role") {
                    if(claim.Value != claimName) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}