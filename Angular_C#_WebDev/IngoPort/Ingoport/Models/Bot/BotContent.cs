// <copyright file="BotContent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BotContents", Schema = "FBR")]
    public class BotContent
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<BotCommand> BotCommands { get; set; }
    }
}