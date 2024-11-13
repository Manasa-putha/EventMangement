using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventManagment.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event? Event { get; set; }
        public decimal Expenses { get; set; }
        //  public string Description { get; set; }
        public decimal Revenue { get; set; }


    }
}
