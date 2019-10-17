// <copyright file="AuthService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using Ingoport.Interfaces;
    using Ingoport.Models;
    using System.Security.Cryptography;

    public class AuthService : IAuthorization
    {
        private readonly UserContext UserContext;
        private readonly List<Claim> claims;
        private readonly Dictionary<object, string> userData;
        public readonly ICheckFile checkFile;

        public AuthService(UserContext UserContext, ICheckFile checkFile)
        {
            this.UserContext = UserContext;
            this.claims = new List<Claim>();
            this.userData = new Dictionary<object, string> { };
            this.checkFile = checkFile;
        }

        public string CheckForAvailability(User user)
        {
            if (this.UserContext.Users.Any(c => c.Email == user.Email && decryptedPassword(c.Password, user.Password)))
            {
                this.claims.Add(new Claim("user_id", this.UserContext.Users.FirstOrDefault(c => c.Email == user.Email).Id.ToString()));
                this.claims.Add(new Claim(ClaimTypes.Role, this.UserContext.Roles.Join(this.UserContext.Users, c => c.Id, p => p.RoleId, (c, p) => new { role = c.Name, user = p.Email }).FirstOrDefault(c => c.user == user.Email).role));
                User data = this.UserContext.Users.FirstOrDefault(c => c.Email == user.Email);
                this.userData["name"] = data.FirstName + " " + data.LastName;
                this.userData["token"] = this.GetToken(this.claims);
                this.userData["photo"] = data.Photo;
                return JsonConvert.SerializeObject(this.userData);

            }

            throw new UnauthorizedAccessException();
        }

        public string GetToken(List<Claim> claimss)
        {
            string securityKey = this.checkFile.GetFile()["key"];
            var symSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var signingCredentials = new SigningCredentials(symSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials,
                claims: claimss
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string DecodeToken(string jwt)
        {
            try
            {
                var decode_token = new JwtSecurityToken(jwtEncodedString: jwt);
                return decode_token.Claims.First(c => c.Type == "user_id").Value;
            }
            catch
            {
                return "Неверные входнныые данные";
            }
        }

        public bool decryptedPassword(string savedHashedPassword, string userPassword)
        {
            try
            {
                byte[] fromDB = Convert.FromBase64String(savedHashedPassword);

                byte[] salt = new byte[16];

                Array.Copy(fromDB, 0, salt, 0, 16);

                var decryptedTryPassword = new Rfc2898DeriveBytes(userPassword, salt, 10000);

                byte[] hashed = decryptedTryPassword.GetBytes(20);

                bool ok = true;

                for(int i = 0 ; i < 20 ; i++)
                {
                    if (fromDB[i + 16] != hashed[i])
                    {
                        ok = false;
                        break;
                    }
                }
                return ok;
            }
            catch
            {
                return false;
            }
        }
    }
}