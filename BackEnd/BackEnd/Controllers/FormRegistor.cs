using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormRegistor : Controller
    {
        // POST: api/formregistor/insertformregistor
        [HttpPost("insertformregistor")]
        public Models.FormRegistor InsertFormRegistor(Models.FormRegistor formRegistor)
        {
            //Connect MongoDB:
            Models.MongoDBSettings.ConnectToMongoService();
            var form_registor = Models.MongoDBSettings.database.GetCollection<Models.FormRegistor>("FormRegistor");
            //Connect RedisDB:
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();

            Int32 total = 0;
            Int32 length = formRegistor.customers.Count();
            for (Int32 i = 0; i < length; i++)
            {
                for (Int32 j = 0; j < formRegistor.customers[i].item.Count(); j++)
                {
                    Int32 tempt = (Int32)formRegistor.customers[i].item[j].price;
                    total += tempt;
                }
            }
            formRegistor.active = true;
            formRegistor.create_date = DateTime.Today;
            formRegistor.total = total;

            form_registor.InsertOne(formRegistor);
            for (Int32 i = 0; i < length; i++)
            {
                for (Int32 j = 0; j < formRegistor.customers[i].item.Count(); j++)
                {
                    db.KeyDelete(formRegistor.customers[i].item[j].key);
                }
            }
            return formRegistor;
        }
    }
}
