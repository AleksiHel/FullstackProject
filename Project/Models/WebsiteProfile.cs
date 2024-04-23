﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class WebsiteProfile
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber {  get; set; }
        [Required]
        public string PageName { get; set; }

        public List<string> Palmares { get; set;}
        public string ProfileText { get; set; }

        


    }
}
