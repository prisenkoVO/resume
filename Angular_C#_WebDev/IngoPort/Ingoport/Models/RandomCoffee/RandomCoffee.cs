namespace Ingoport.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RandomCoffee", Schema = "FBR")]

    public class RandomCoffee
    {
        public long Id { get; set; }

        public DateTime DateTime { get; set; }

        //Status can be: 0 - created, 1 - accepted by both users, 2 - canceled, 3 - held
        public int Status { get; set; }
    }
}
