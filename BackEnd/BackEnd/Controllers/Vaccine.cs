using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Cors;


namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Vaccine : Controller
    {
        // GET: api/vaccine/getall
        [HttpGet("getAll")]
        public IEnumerable<Models.Vaccine> GetAllVaccine()
        {
            //Connect DB:
            Models.MongoDBSettings.ConnectToMongoService();
            var vaccine = Models.MongoDBSettings.database.GetCollection<Models.Vaccine>("Vaccine");
            return vaccine.Find(s => s.active == true).ToList();
        }

        //GET: api/vaccine/getinfo/{vaccine_name}
        [HttpGet("getinfo/{vaccine_name}")]
        public IEnumerable<Models.Vaccine> GetInfoVaccine(string vaccine_name)
        {
            //Connect DB:
            Models.MongoDBSettings.ConnectToMongoService();
            var vaccine_info = Models.MongoDBSettings.database.GetCollection<Models.Vaccine>("Vaccine");
            var filter = Builders<Models.Vaccine>.Filter.Where(s => s.name.ToLower().Contains(vaccine_name.Trim().ToLower()));
            return vaccine_info.Find(filter).ToList();
        }

    }
}
