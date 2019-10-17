// <copyright file="Intern.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Ingoport.Models
{
    using System.Collections.Generic;

    public class Intern: User
    {
       public List<User> Mentors { get; set; }
    }
}
