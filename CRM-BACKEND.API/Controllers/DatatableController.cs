using Entities.Models.DatabaseCreation;
using Entities.Models.DataTableCreation;
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
        IRepositoryManager repository;

        public DatatableController(IRepositoryManager _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTable([FromQuery]DataTableService service)
        {
            var schema  = await repository.Datatable.GetTableSchema(service);

            return Ok(schema);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable(DataTableService service)
        {
            var value = await repository.Datatable.CreateTable(service);

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
            await repository.Datatable.DeleteTable(service);

            return Ok("УДАЛИЛОСЬ.");
        }
    }
}
