using Entities.Models.DatabaseCreation;
using Entities.Models.DataTableCreation;
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
    public class DatatableController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;

        public DatatableController(IRepositoryManager _repository, ILoggerManager _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTable([FromQuery] DataTableService service)
        {
            var schema = await repository.Datatable.GetTableSchemaAsync(service);

            return Ok(schema);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable(DataTableService service)
        {
            var value = await repository.Datatable.CreateTableAsync(service);

            if (value)
            {
                return Ok("Table was succesfully created.");
            }
            else
            {
                return BadRequest("Table was not created.");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTable(DataTableService service)
        {
            await repository.Datatable.DeleteTableAsync(service);

            return Ok("УДАЛИЛОСЬ.");
        }
    }
}
