using EventManagment.Models;

namespace EventManagment.Dtos
{
    public class RegistereventDto
    {
        public int UserId { get; set; }
        //public string UserName { get; set; }
        //public string MobileNumber { get; set; }
    }
    public class EventDto
    {
        public int Id { get; set; }
        public string EventName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public BudgetDto? Budget { get; set; }
        public List<GuestDto> Guests { get; set; } = new List<GuestDto>();
        public RegistereventDto?PersonalInfo { get; internal set; }
        public Event? EventDetails { get; internal set; }
    }


  

}
