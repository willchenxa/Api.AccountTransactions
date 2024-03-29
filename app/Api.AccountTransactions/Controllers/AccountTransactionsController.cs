﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.AccountTransactions.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [ValidationFilter]
    //[Authorize]
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
        [SwaggerResponse(200, "This API endpoint takes no parameters and returns a list of transactions.", typeof(IEnumerable<Transaction>))]
        [SwaggerResponseExample(200, typeof(SwaggerExamples.ReturnTransactionsExample))]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            var transactions = await _service.GetAccountTransactions();
            return Ok(transactions);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(200, "Posts a new transaction to a specific account", typeof(TransactionResponse))]
        [SwaggerRequestExample(typeof(Transaction), typeof(SwaggerExamples.TransactionRequestExample))]
        [SwaggerResponseExample(200, typeof(SwaggerExamples.CreatedTransactionExample))]
        public async Task<ActionResult<TransactionResponse>> CreateTransaction(Transaction transaction)
        {
            return Ok(await _service.CreateTransaction(transaction));
        }

        [HttpPut("{transactionID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(200, "Updates a transaction", typeof(string))]
        [SwaggerRequestExample(typeof(Transaction), typeof(SwaggerExamples.TransactionRequestExample))]
        [SwaggerResponseExample(200, typeof(SwaggerExamples.UpdateTransactionExample))]
        public async Task<ActionResult<TransactionResponse>> UpdateTransaction([FromBody] Transaction transaction, string transactionID)
        {
            return Ok(await _service.UpdateTransaction(transaction, transactionID));
        }
    }
}
