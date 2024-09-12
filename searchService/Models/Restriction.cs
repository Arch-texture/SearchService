using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace searchService.Models
{
    public class Restriction
    {
        [BsonId]
       [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string UUID { get; set; } = null!;
        public string Reason { get; set; } = null!;
    }
}