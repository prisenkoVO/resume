// <copyright file="RegistrationService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Newtonsoft.Json;
    using Ingoport.Interfaces;
    using Ingoport.Models;
    using System.Security.Cryptography;
    using System;

    public class RegistrationService : IRegistration
    {
        private readonly List<Claim> claims;
        private readonly IAuthorization authorization;
        private readonly UserContext UserContext;

        public RegistrationService(IAuthorization authorization, UserContext UserContext)
        {
            this.authorization = authorization;
            this.UserContext = UserContext;
            this.claims = new List<Claim>();
        }

        public string RegisterUser(User user)
        {
            var role = this.UserContext.Roles.FirstOrDefault(c => c.Id == user.RoleId);
            user.Role = role;
            user.Password = encryptPassword(user.Password);
            this.claims.Add(new Claim("user_id", user.Id.ToString()));
            this.claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                ["name"] = user.FirstName + " " + user.LastName,
                ["token"] = this.authorization.GetToken(this.claims),
            };
            this.UserContext.Users.Add(user);
            this.UserContext.SaveChanges();
            return JsonConvert.SerializeObject(dict);
        }

        public string encryptPassword(string pass)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var hashed = new Rfc2898DeriveBytes(pass, salt, 10000);
            byte[] hash = hashed.GetBytes(20);

            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string saveHashedPassword = Convert.ToBase64String(hashBytes);

            return saveHashedPassword;
        }
    }
}