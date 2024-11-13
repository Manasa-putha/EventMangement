using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventManagment.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string EventName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }  // OrganizerID
        [ForeignKey("UserId")]
        public User? User { get; set; }
        // public ICollection<Guest>? Guests { get; set; } = new List<Guest>();

        public Budget? Budget { get; set; }
      //  public bool IsBooked { get; set; }
        public ICollection<Guest>? Guests { get; set; } = new List<Guest>();
        //public Event EventDetails { get; internal set; }
        //public RegistereventDto PersonalInfo { get; internal set; }
    }

}
