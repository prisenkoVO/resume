using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ingoport.Models
{
    [Table("QuestionTopic", Schema = "FBR")]
    public class QuestionTopic
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

    }
}
