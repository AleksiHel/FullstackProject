using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Article
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId AuthorId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content {  get; set; }
        //[Required]
        public bool IsPublic { get; set; }
        [Required]
        public DateTime PublishingDate { get; set; }

        

    }
}
