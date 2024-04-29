using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class ServiceViewModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public string ServiceDescription { get; set; }

        [Required]
        [BsonRepresentation(BsonType.Decimal128)]

        public double ServicePrice { get; set; }

        public string? AdditionalInformation { get; set; }
        public IFormFile? TitleImage { get; set; }

    }
}
