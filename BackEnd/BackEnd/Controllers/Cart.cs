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
    public class Cart : Controller
    {
        // POST: api/cart/addcart/{user_id}
        [HttpPost("addcart/{user_id}")]
        public int AddCart(string user_id, Models.Vaccine vaccine)
        {
            //Connect DB:
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();

            string key = user_id + ":" + vaccine.id;

            if (db.KeyExists(key))
                return 0;
            else
            {
                db.HashSet(key, "id", vaccine.id);
                db.HashSet(key, "name", vaccine.name);
                db.HashSet(key, "price", vaccine.price);
                db.HashSet(key, "function", vaccine.function);
                db.HashSet(key, "description", vaccine.description);
                return 1;
            }
        }

        //Get: api/cart/getcart/{user_id}
        [HttpGet("getcart/{user_id}")]
        public IEnumerable<Models.Vaccine> GetCart(string user_id)
        {
            //Connect DB:
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            var server = redis.GetServer("localhost", 6379);
            Models.Vaccine vaccine = new Models.Vaccine();
            vaccine.active = true;

            foreach (var keys in server.Keys(pattern: user_id + ":" + "*"))
            {
                vaccine.id = db.HashGet(keys, "id").ToString();
                vaccine.name = db.HashGet(keys, "name").ToString();
                vaccine.price = (int)db.HashGet(keys, "price");
                vaccine.function = db.HashGet(keys, "function").ToString();
                vaccine.description = db.HashGet(keys, "description").ToString();
                yield return vaccine;
            }
        }
    }
}
