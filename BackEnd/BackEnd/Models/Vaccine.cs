using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEnd.Models
{
    public class Vaccine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("price")]
        public Int32 price { get; set; }

        [BsonElement("function")]
        public string function { get; set; }

        [BsonElement("description")]
        public string description { get; set; }

        [BsonElement("active")]
        public bool active { get; set; }

        [BsonElement("create_date")]
        public DateTime create_date { get; set; }
    }

    public class CartItem : Vaccine
    {
        public string key { get; set; }
    }
}
