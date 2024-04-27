using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class MessageViewModel
    {

        [Required(ErrorMessage = "Email required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name required")]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Message required")]
        [MinLength(10, ErrorMessage = "Message too short")]
        [MaxLength(500, ErrorMessage = "Message too long")]
        public string ContactMessage { get; set; }

        [Required]
        public string Subject { get; set; }

        public List<Service> Services;

        public string Selected { get; set; } = "placeholder test to not give empty when going from form to controller";

        //[Required]
        //public DateTime DateSubmitted { get; set; }

        //[Required]
        //public bool Answered { get; set; } = false;

    }
}
