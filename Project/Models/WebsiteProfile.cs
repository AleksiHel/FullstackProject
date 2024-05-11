using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class WebsiteProfile
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId UserId { get; set; }

        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Biography { get; set; }
        [Required]
        public string Palmares { get; set;}
        

        public string? InstagramHandle {  get; set; }
        public string? FacebookHandle { get; set; }
        public string? LinkedinHandle { get; set; }
        public string? YoutubeHandle {  get; set; }




    }
}
