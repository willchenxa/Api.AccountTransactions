using Api.AccountTransactions.Dtos;
using Api.AccountTransactions.Services;
using Api.AccountTransactions.Swagger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.AccountTransactions.Controllers
{
    [ApiController]
    [Route("transactions")]
    public class AccountTransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;
        private readonly ILogger<AccountTransactionsController> _logger;

        public AccountTransactionsController(ITransactionService service, ILogger<AccountTransactionsController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(200, typeof(SwaggerExamples.ReturnTransactionsExample))]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            var transactions = await _service.GetAccountTractions();
            return Ok(transactions);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(Transaction), typeof(SwaggerExamples.TransactionRequestExample))]
        public async Task<ActionResult> CreateTransaction(Transaction transaction)
        {
            await _service.CreateTrasaction(transaction);
            return Ok();
        }

        [HttpPut("{transactionID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(Transaction), typeof(SwaggerExamples.TransactionRequestExample))]
        public async Task<ActionResult> UpdateTransaction([FromBody] Transaction transaction, string transactionID)
        {
            await _service.UpdateTransaction(transaction);
            return Ok();
        }
    }
}
