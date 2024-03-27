using Dapper;
using DapperSPTest.Abstract;
using DapperSPTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DapperSPTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(IDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            using (var conn = _context.CreateConnection())
            {
                var result = await conn.QueryAsync<Invoice>("GetInvoices", commandType: CommandType.StoredProcedure);

                return Ok(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<Invoice>("GetInvoiceById",new
                {
                    Id = id
                } ,commandType: CommandType.StoredProcedure);

                if(result is null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, string invoiceName)
        {
            using (var conn = _context.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<Invoice>("GetInvoiceById", new
                {
                    Id = id
                }, commandType: CommandType.StoredProcedure);

                if (result is null)
                {
                    return NotFound();
                }

                var result2 = await conn.QueryFirstOrDefaultAsync<Invoice>("UpdateInvoice", new
                {
                    Id = id,
                    InvoiceName = invoiceName
                }, commandType: CommandType.StoredProcedure);

                return Ok(result2);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateInvoice(string invoiceName)
        {
            using (var conn = _context.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<Invoice>("CreateInvoice", new
                {
                    InvoiceName = invoiceName
                }, commandType: CommandType.StoredProcedure);

                return Ok(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<Invoice>("GetInvoiceById", new
                {
                    Id = id
                }, commandType: CommandType.StoredProcedure);

                if (result is null)
                {
                    return NotFound();
                }

                var result2 = await conn.ExecuteAsync("DeleteInvoice", new
                {
                    Id = id
                }, commandType: CommandType.StoredProcedure);

                

                return Ok("Deleted" + result2);
            }
        }
    }
}
