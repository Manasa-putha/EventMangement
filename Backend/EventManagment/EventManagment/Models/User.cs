using EventManagment.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagment.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        public string userName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string AlternativeNumber { get; set; } = string.Empty;
        //public string Role { get; set; }
        public string? Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int KW_Allowed { get; set; }
        public int BasePrice { get; set; }
        public UserType UserType { get; set; } = UserType.None;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string PinCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        //public ICollection<Event>? Events { get; set; }
        // Navigation property
        //  public ICollection<Bill>? Bills { get; set; }
        // public AccountStatus AccountStatus { get; set; } = AccountStatus.UNAPROOVED;
        // Navigation properties
        public ICollection<Event>? OrganizedEvents { get; set; }
        public ICollection<Guest>? RegisteredEvents { get; set; }
    }
  

 



}
