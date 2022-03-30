using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class MongoDBSettings
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoConnection = "mongodb+srv://admin:admin1234@cluster0.0sigg.mongodb.net/Cluster0?retryWrites=true&w=majority";
        public static string MongoDatabase = "N02_VNVC";

        internal static void ConnectToMongoService()
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
            }
            catch
            {
                throw;
            }
        }
    }
}
