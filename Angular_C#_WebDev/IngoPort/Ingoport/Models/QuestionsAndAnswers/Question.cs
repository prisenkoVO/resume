using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ingoport.Models
{
    [Table("Questions", Schema = "FBR")]
    public class Question
    {
        [Key]
        public long Id { get; set; }

        public string Text { get; set; }

        [Column("QuestionTopicId")]
        public long QuestionTopicId { get; set; }

        [ForeignKey("QuestionTopicId")]
        public virtual QuestionTopic QuestionTopic { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
