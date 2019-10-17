using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ingoport.Models
{
    [Table("Tasks", Schema = "FBR")]
    public class UserTask
    {
        [Key]
        public long Id { get; set; }

        public bool Done { get; set; }

        public string Title { get; set; }

        public string TaskBody { get; set; }

        public string Owner { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime DateTime { get; set; }


        [InverseProperty("TasksIGaveAsMentor")]
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        [InverseProperty("Tasks")]
        [ForeignKey("InternId")]
        public virtual User Intern { get; set; }

        public long AuthorId { get; set; }

        public long InternId { get; set; }

        public string FlagColor { get; set; }

        public bool IsDeleted { get; set; }
    }
}
