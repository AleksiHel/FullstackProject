using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;


namespace Project.Models
{
    public class Message
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [Required(ErrorMessage = "Email required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string FullName { get; set; }

        [Required(ErrorMessage = "Message required")]
        [MinLength(10, ErrorMessage = "Message too short")]
        [MaxLength(500, ErrorMessage = "Message too long")]
        public string ContactMessage { get; set; }

        [Required]
        public DateTime DateSubmitted { get; set; } = DateTime.Today;

        [Required]
        public bool Answered { get; set; } = false;
        [Required]
        public string Subject { get; set; }
    }
}
