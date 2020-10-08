using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Extensions
{
    public static class IdentityExtensions
    {
        public static TKey UserId<TKey>(this ClaimsPrincipal user)
        {
            var stringId = user.Claims
                .Single(c => c.Type == ClaimTypes.NameIdentifier).Value.Trim();

            if (typeof(TKey) == typeof(string))
            {
                return (TKey) Convert.ChangeType(stringId, typeof(TKey));
            }

            if (typeof(TKey) == typeof(int) || typeof(TKey) == typeof(long))
            {
                return (TKey) Convert.ChangeType(stringId, typeof(TKey));
            }

            if (typeof(TKey) == typeof(Guid))
            {
                return (TKey) Convert.ChangeType(new Guid(stringId), typeof(TKey));
            }

            throw new Exception("Invalid type provided");
        }

        public static Guid UserId(this ClaimsPrincipal user)
        {
            return user.UserId<Guid>();
        }

        public static List<Claim> UserRoles(this ClaimsPrincipal user)
        {
            var claims = user.Claims;
            return claims.Where(c => c.Type == ClaimTypes.Role).ToList();
        }

        public static string GenerateJWT(IEnumerable<Claim> claims, string signingKey, string issuer, int expiresInDays)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var singingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var expires = DateTime.Now.AddDays(expiresInDays);
            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                null,
                expires,
                singingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }

}