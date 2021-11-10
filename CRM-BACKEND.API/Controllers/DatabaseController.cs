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
        public async Task<IActionResult> CreateDatabase(string name)
        {
            await repository.Database.CreateDatabase(name);

            return Ok("Database is created succesfully.");
        }

        [HttpDelete]
        public IActionResult DeleteDatabase(string name)
        {
            repository.Database.DeleteDatabase(name);

            return Ok("Database is deleted succesfully.");
        }
    }
}
