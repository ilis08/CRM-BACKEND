using Entities.Models.DatabaseCreation;
using LoggerService;
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
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;

        public DatabaseController(IRepositoryManager _repository, ILoggerManager _logger)
        {
            repository = _repository;
            logger = _logger;
            logger.LogDebug("NLog injected into DatabaseController");
        }

        [HttpGet]
        public async Task<IActionResult> GetDatabases()
        {
            var databases = await repository.Database.GetDatabasesAsync();

            return Ok(databases);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDatabase(Database database)
        {
            if (await repository.Database.CreateDatabaseAsync(database))
            {
                logger.LogInfo("Database is created.");
                return Ok("Database is created succesfully.");
            }
            else
            {
                logger.LogWarning("Datatabe was not created.");
                return Ok("Database was not created");
            }


        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDatabase(Database database)
        {
            await repository.Database.DeleteDatabaseAsync(database);

            return Ok("Database is deleted succesfully.");
        }
    }
}
