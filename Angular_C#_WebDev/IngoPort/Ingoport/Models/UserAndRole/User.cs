
﻿// <copyright file="User.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    [Table("Users", Schema ="FBR")]
    public class User
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Birth { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Office { get; set; }

        public string Team { get; set; }

        public double Rank { get; set; }

        public string Password { get; set; }

        public virtual ICollection<News> News { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        [Column("RoleId")]
        public long RoleId { get; set; }

        public virtual ICollection<UserTask> Tasks { get; set; }

        public virtual ICollection<UserTask> TasksIGaveAsMentor { get; set; }

        public string Photo { get; set; }

    }
}