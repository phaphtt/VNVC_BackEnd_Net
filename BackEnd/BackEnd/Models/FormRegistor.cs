using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEnd.Models
{
    public class CustomerRegistor
    {
        public Customer customer { get; set; }
        public List<CartItem> item { get; set; }
    }
    public class FormRegistor
    {
        public List<CustomerRegistor> customers { get; set; }
        public Int32 total { get; set; }

        [BsonElement("payment_name")]
        public string payment_name { get; set; }

        [BsonElement("payment_phone")]
        public string payment_phone { get; set; }

        [BsonElement("payment_email")]
        public string payment_email { get; set; }

        [BsonElement("payment_cccd")]
        public string payment_cccd { get; set; }

        [BsonElement("customer_id")]
        public string customer_id { get; set; }

        [BsonElement("payment_address")]
        public string payment_address { get; set; }

        [BsonElement("payment_city")]
        public string payment_city { get; set; }

        [BsonElement("payment_district")]
        public string payment_district { get; set; }

        [BsonElement("payment_commune")]
        public string payment_commune { get; set; }

        [BsonElement("payment_type")]
        public string payment_type { get; set; }

        [BsonElement("active")]
        public bool active { get; set; }

        [BsonElement("create_date")]
        public DateTime create_date { get; set; }
    }
}
