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
        public IEnumerable<Models.CartItem> GetCart(string user_id)
        {
            //Connect DB:
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            var server = redis.GetServer("localhost", 6379);
            Models.CartItem item = new Models.CartItem();
            item.active = true;

            foreach (var keys in server.Keys(pattern: user_id + ":" + "*"))
            {
                item.id = db.HashGet(keys, "id").ToString();
                item.name = db.HashGet(keys, "name").ToString();
                item.price = (int)db.HashGet(keys, "price");
                item.function = db.HashGet(keys, "function").ToString();
                item.description = db.HashGet(keys, "description").ToString();
                item.key = keys;
                yield return item;
            }
        }

        //Delete: api/cart/deleteitem/{key}
        [HttpDelete("deleteitem/{key}")]
        public string DeleteItemCart(string key)
        {
            //Connect DB:
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            if (db.KeyExists(key))
            {
                db.KeyDelete(key);
                return "Xóa sản phẩm trong giỏ hàng thành công";
            }
            else
                return "Sản phẩm không tồn tại trong giỏ hàng. Xóa sản phẩm trong giỏ hàng thất bại";
        }
    }
}
