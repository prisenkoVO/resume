namespace Ingoport.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Answers", Schema = "FBR")]
    public class Answer
    {
        [Key]
        public long Id { get; set; }

        public string Text { get; set; }

        [Column("QuestionId")]
        public long QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}
