// <copyright file="Like.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Likes", Schema = "FBR")]

    public class Like
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("NewsId")]
        public virtual News News { get; set; }
    }
}