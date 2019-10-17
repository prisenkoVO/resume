namespace Ingoport.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserStatus", Schema = "FBR")]
    public class UserStatus
    {
        public long Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Column("UserId")]
        public long UserId { get; set; }

        //Status can be: 0 - free, 1 - found a pain, (maybe?) 2 - already been on RC this week, 3 - banned
        public int Status { get; set; }

    }
}
