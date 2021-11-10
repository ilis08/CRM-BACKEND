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

        [HttpPost]
        public async Task<IActionResult> CreateTable([FromQuery]Database database, [FromBody]Table table)
        {
            await repository.Datatable.CreateTable(database, table);

            return Ok("СОЗДАЛОСЬ");
        }
    }
}
