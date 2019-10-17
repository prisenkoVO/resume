// <copyright file="News.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    [Table("News", Schema = "FBR")]

    public class News
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string Photo { get; set; }

        [Column("UserId")]
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        //public virtual ImageBD Image { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Bookmark> Bookmarks { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public bool IsDeleted { get; set; }
    }
}
