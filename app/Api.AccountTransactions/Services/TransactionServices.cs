global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;

namespace Api.AccountTransactions.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAccountTransactions();
        Task<TransactionResponse> CreateTransaction(Transaction transaction);
        Task<TransactionResponse> UpdateTransaction(Transaction transaction, string transactionID);

    }
    public class TransactionService : ITransactionService
    {
        private readonly TransactionDbContext _dbContext;

        public TransactionService(TransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<TransactionResponse> CreateTransaction(Transaction transaction)
        {
            var entity = _dbContext.Find(typeof(Transaction), transaction.id);

            if (entity != null)
                throw new HttpResponseException
                {
                    Status = 400,
                    Value = Constant.TransactionResponseMessages.TransactionExists
                };

            SetCustomerState(transaction);

            _dbContext.Entry(transaction).State = EntityState.Added;
            _dbContext.Set<Transaction>().Add(transaction);
            _dbContext.SaveChanges();

            return Task.FromResult(new TransactionResponse { Message = Constant.TransactionResponseMessages.CreatedSuccessful });
        }

        public async Task<IEnumerable<Transaction>> GetAccountTransactions()
        {
            return await Task.FromResult(_dbContext.Set<Transaction>().Include(t => t.owner));
        }

        public async Task<TransactionResponse> UpdateTransaction(Transaction transaction, string transactionID)
        {
            var entity = _dbContext.Transactions.Include(e => e.owner)
                .FirstOrDefault(t => t.id == transactionID);

            if (entity == null)
                throw new HttpResponseException
                {
                    Status = 400,
                    Value = Constant.TransactionResponseMessages.TransactionDoesNotExists
                };

            SetCustomerState(transaction);

            _dbContext.Entry(entity).CurrentValues.SetValues(transaction);
            _dbContext.SaveChanges();

            return await Task.FromResult(new TransactionResponse { Message = Constant.TransactionResponseMessages.UpdatedSucessful });
        }

        private void SetCustomerState(Transaction transaction)
        {
            var entity = _dbContext.Find(typeof(Customer), transaction.owner.id);
            if (entity == null)
            {
                _dbContext.Entry(transaction.owner).State = EntityState.Added;
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
