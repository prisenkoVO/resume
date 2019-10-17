// <copyright file="Mentor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Models
{
    using System.Collections.Generic;

    public class Mentor : User
    {
        public List<User> Students { get; set; }
    }
}
