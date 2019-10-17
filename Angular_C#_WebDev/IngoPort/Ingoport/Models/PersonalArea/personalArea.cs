// <copyright file="personalArea.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PersonalArea
    {
        public string Name { get; set; }

        public string SecondName { get; set; }

        public string Department { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Skype { get; set; }

        public double Rank { get; set; }

        public virtual IEnumerable<User> Teammates { get; set; }
    }
}