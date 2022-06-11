using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtToken
{
    public class Manager
    {
        private SecurityKey SecurityKey { get; }
        private SigningCredentials SigningCredentials { get; }
        private JwtSecurityTokenHandler TokenHandler { get; set; }
        private TokenValidationParameters TokenValidationParameters { get; set; }

        public Manager(string privateKey, string algoritm = SecurityAlgorithms.HmacSha512, TokenValidationParameters parameters = null)
        {
            SecurityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(privateKey));

            SigningCredentials = new SigningCredentials(SecurityKey, algoritm);

            TokenValidationParameters = parameters ?? new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = SecurityKey
            };

            TokenHandler = new JwtSecurityTokenHandler();
        }

        public string CreateToken(Dictionary<string, object> claims = null, DateTime? expired = null)
        {
            var jwtSecurityToken = TokenHandler.CreateJwtSecurityToken(
                new SecurityTokenDescriptor
                {
                    Claims = claims,
                    Expires = expired ?? DateTime.Now.AddHours(1),
                    SigningCredentials = SigningCredentials
                });

            return TokenHandler.WriteToken(jwtSecurityToken);
        }

        public bool ValidateToken(string token, out JwtSecurityToken jwtToken)
        {
            jwtToken = null;

            try
            {
                TokenHandler.ValidateToken(token, TokenValidationParameters, out var validatedToken);

                jwtToken = TokenHandler.ReadJwtToken(token);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
