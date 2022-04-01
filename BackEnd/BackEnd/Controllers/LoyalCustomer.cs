using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyalCustomer : Controller
    {
        // POST: api/loyalcustomer/insertcustomer
        [HttpPost("insertcustomer")]
        public string InsertLoyalCustomer(Models.LoyalCustomer customer)
        {
            //Connect DB:
            Models.MongoDBSettings.ConnectToMongoService();
            var loyal_customer = Models.MongoDBSettings.database.GetCollection<Models.LoyalCustomer>("LoyalCustomer");
            int length = (int)loyal_customer.Find(s => s.active == true || s.active == false).Count() + 1;

            if (loyal_customer.Find(s => s.cccd == customer.cccd).Count() != 0)
            {
                return "Thông tin khách hàng đã tồn tại. Đăng ký khách hàng thân thiết thất bại!!!";
            }
            else
            {
                customer.active = true;
                customer.create_date = DateTime.Today;
                customer.loyal_customer_id = "LOYALCUSTOMER" + Convert.ToString(length);
                loyal_customer.InsertOne(customer);
                return "Đăng ký khách hàng thân thiết thành công!!! Mã khách hàng thân thiết: "+customer.loyal_customer_id;
            }
        }

        // GET: api/loyalcustomer/getinfo/{loyal_customer_id}
        [HttpGet("getinfo/{loyal_customer_id}")]
        public Models.LoyalCustomer GetInfoLoyalCustomer(string loyal_customer_id)
        {
            //Connect DB:
            Models.MongoDBSettings.ConnectToMongoService();
            var loyal_customer = Models.MongoDBSettings.database.GetCollection<Models.LoyalCustomer>("LoyalCustomer");
            return loyal_customer.Find(s => s.loyal_customer_id == loyal_customer_id).FirstOrDefault();
        }
    }
}
