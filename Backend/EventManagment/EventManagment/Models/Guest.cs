using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagment.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("EventId")]
        public Event? Event { get; set; }
    }

}
