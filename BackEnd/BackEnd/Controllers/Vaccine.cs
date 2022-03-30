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
        // GET: api/vaccine/all
        [HttpGet("getAll")]
        public IEnumerable<Models.Vaccine> GetAllVaccine()
        {
            //Connect DB:
            Models.MongoDBSettings.ConnectToMongoService();
            var vaccine = Models.MongoDBSettings.database.GetCollection<Models.Vaccine>("Vaccine");
            return vaccine.Find(s => s.active == true).ToList();
        }
    }
}
