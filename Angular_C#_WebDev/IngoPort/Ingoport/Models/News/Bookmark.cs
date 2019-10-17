namespace Ingoport.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Bookmarks", Schema = "FBR")]
    public class Bookmark
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("NewsId")]
        public virtual News News { get; set; }

        [Column("UserId")]
        public long UserId { get; set; }

        [Column("NewsId")]
        public long NewsId { get; set; }
    }
}
