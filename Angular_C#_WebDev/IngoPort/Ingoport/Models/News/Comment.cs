using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ingoport.Models
{
    [Table("Comments", Schema = "FBR")]

    public class Comment
    {
        public long id { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Column("UserId")]
        public long UserId { get; set; }

        public string commentText { get; set; }

        [ForeignKey("NewsId")]
        public virtual News News { get; set; }

        [Column("NewsId")]
        public long NewsId { get; set; }
    }
}
