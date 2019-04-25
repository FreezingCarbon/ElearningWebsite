using System.IdentityModel.Tokens.Jwt;

namespace ElearningWebsite.API.Helpers
{
    public static class AuthHelper
    {
        /// <summary>
        /// Check if the token is authorized
        /// </summary>
        /// <param name="tokenString">Token get from header(include Bearer)</param>
        /// <param name="userId">Id of the user</param>
        /// <param name="claimName">Role of user</param>
        public static bool BasicAuth(string tokenString, int userId, string claimName)
        {
            if(tokenString == null) {
                return false;
            }
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
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
           using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++) {
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

    }
}