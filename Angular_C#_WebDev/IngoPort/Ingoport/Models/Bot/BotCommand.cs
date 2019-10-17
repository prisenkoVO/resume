// <copyright file="BotCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BotCommands", Schema = "FBR")]
    public class BotCommand
    {
        public int Id { get; set; }

        [ForeignKey("BotContentId")]
        public virtual BotContent BotContent { get; set; }

        [Column("BotContentId")]
        public int BotContentId { get; set; }

        public string Commands { get; set; }
    }
}
