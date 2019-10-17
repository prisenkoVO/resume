
namespace Ingoport.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserMeeting", Schema = "FBR")]
    public class UserMeeting
    {
        public long Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Column("UserId")]
        public long UserId { get; set; }

        [ForeignKey("RandomCoffeeId")]
        public RandomCoffee RandomCoffee { get; set; }

        [Column("RandomCoffeeId")]
        public long RandomCoffeeId { get; set; }

        public int Star { get; set; }

        public string Feedback { get; set; }
    }
}
