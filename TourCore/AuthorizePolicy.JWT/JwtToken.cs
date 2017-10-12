using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthorizePolicy.JWT
{
    /// <summary>
    /// JWTToken生成类
    /// </summary>
    public class JwtToken
    {
        public static readonly string token_type = "Bearer";
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static dynamic BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,
                audience: permissionRequirement.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(permissionRequirement.Expiration),
                signingCredentials: permissionRequirement.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new
            {
                Status = true,
                access_token = encodedJwt,
                expires_in = permissionRequirement.Expiration.TotalMilliseconds,
                token_type = token_type,
            };
            return responseJson;
        }
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string BuildJwtTokenString(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,
                audience: permissionRequirement.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(permissionRequirement.Expiration),
                signingCredentials: permissionRequirement.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return (token_type + " " + encodedJwt);
        }
    }
}
