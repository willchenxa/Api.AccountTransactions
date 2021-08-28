using Api.AccountTransaction.UnitTests.MockData;
using Api.AccountTransactions.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.AccountTransaction.UnitTests
{
    public class TransactionContextFixture : IDisposable
    {
        public TransactionDbContext DBContext;

        public TransactionContextFixture()
        {
            InitContext();
        }

        private void InitContext()
        {
            var options = new DbContextOptionsBuilder<TransactionDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            DBContext = new TransactionDbContext(options);

            DBContext.AddRange(TransactionMockData.SuccessTransactions);
            DBContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DBContext.Dispose();
            }
        }
    }
}
