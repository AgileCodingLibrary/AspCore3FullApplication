using LittlePacktBookstore.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LittlePacktBookstore.Common
{
    public class ApiAuthenticationHelper
    {

        internal static readonly string Key = "DD62DAA7-4E9E-405D-BC9E-5A5508233843";
        internal static readonly string Issuer = "LittlePacktBookStore";
        internal static readonly string Audience = "App_User";

        internal static SymmetricSecurityKey GetKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }

        internal static SigningCredentials GetCredentials()
        {
            return new SigningCredentials(GetKey(), SecurityAlgorithms.HmacSha512);
        }

        internal static Claim[] GetClaims(SiteUser user)
        {
            return new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }

        internal static JwtSecurityToken GetSecurityToken(Claim[] claims, SigningCredentials credentials)
        {
            return new JwtSecurityToken(Issuer, Audience, claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
        }
    }
}
