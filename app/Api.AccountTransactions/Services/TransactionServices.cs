using Api.AccountTransactions.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.AccountTransactions.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAccountTractions();
        Task CreateTrasaction(Transaction transaction);
        Task UpdateTransaction(Transaction transaction);

    }
    public class TransactionService : ITransactionService
    {
        private readonly List<Transaction> _inMemoryTransactions = new List<Transaction>();

        public Task CreateTrasaction(Transaction transaction)
        {
            _inMemoryTransactions.Add(transaction);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Transaction>> GetAccountTractions()
        {
            return await Task.FromResult(_inMemoryTransactions);
        }

        public Task UpdateTransaction(Transaction transaction)
        {
            return Task.CompletedTask;
        }
    }
}
