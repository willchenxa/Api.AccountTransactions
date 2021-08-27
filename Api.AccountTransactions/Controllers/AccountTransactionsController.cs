using Api.AccountTransactions.Swagger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace Api.AccountTransactions.Controllers
{
    [ApiController]
    [Route("transactions")]
    public class AccountTransactionsController : ControllerBase
    {
        private readonly ILogger<AccountTransactionsController> _logger;

        public AccountTransactionsController(ILogger<AccountTransactionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(200, typeof(SwaggerExamples.ReturnTransactionsExample))]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await Task.FromResult(new List<Transaction>());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(Transaction), typeof(SwaggerExamples.TransactionRequestExample))]
        public async Task<ActionResult> CreateTransaction(Transaction transaction)
        {
            return Ok();
        }

        [HttpPut("{transactionID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(Transaction), typeof(SwaggerExamples.TransactionRequestExample))]
        public async Task<ActionResult> UpdateTransaction([FromBody] Transaction transaction, string transactionID)
        {
            return Ok();
        }
    }
}
