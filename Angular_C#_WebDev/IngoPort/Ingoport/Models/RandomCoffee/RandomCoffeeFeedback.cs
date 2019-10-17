namespace Ingoport.Models
{

    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RandomCoffeeFeedback", Schema = "FBR")]

    public class RandomCoffeeFeedback
    {
        public long Id { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Column("UserId")]
        public long UserId { get; set; }

        public int Star { get; set; }

        public string FeedBack { get; set; }
    }
}
