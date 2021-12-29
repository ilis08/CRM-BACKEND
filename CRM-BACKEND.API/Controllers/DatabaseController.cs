using Entities.Models.DatabaseCreation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_BACKEND.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        IRepositoryManager repository;

        public DatabaseController(IRepositoryManager _repository)
        {
            repository = _repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDatabase(Database database)
        {
            if (await repository.Database.CreateDatabase(database))
            {
                return Ok("Database is created succesfully.");
            }
            else
            {
                return Ok("Database was not created");
            }


        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDatabase(Database database)
        {
            await repository.Database.DeleteDatabase(database);

            return Ok("Database is deleted succesfully.");
        }
    }
}
