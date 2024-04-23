using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Service
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public string ServiceDescription { get; set; }

        [Required]
        [BsonRepresentation(BsonType.Decimal128)]

        public double? ServicePrice { get; set; }


        public string AdditionalInformation { get; set; }

    }
}
