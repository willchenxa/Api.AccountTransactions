using Api.AccountTransactions.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Api.AccountTransactions.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAccountTransactions();
        Task<string> CreateTransaction(Transaction transaction);
        Task<string> UpdateTransaction(Transaction transaction, string transactionID);

    }
    public class TransactionService : ITransactionService
    {
        private readonly List<Transaction> _inMemoryTransactions = new List<Transaction>();

        public Task<string> CreateTransaction(Transaction transaction)
        {
            _inMemoryTransactions.Add(transaction);

            return Task.FromResult(Constant.TransactionResponse.CreatedSuccessful);
        }

        public async Task<IEnumerable<Transaction>> GetAccountTransactions()
        {
            return await Task.FromResult(_inMemoryTransactions);
        }

        public async Task<string> UpdateTransaction(Transaction transaction, string transactionID)
        {
            var updateTransactionIndex = _inMemoryTransactions.FindIndex(t => t.id == transactionID);

            _inMemoryTransactions[updateTransactionIndex] = transaction;

            return await Task.FromResult(Constant.TransactionResponse.UpdatedSucessful);
        }
    }
}
