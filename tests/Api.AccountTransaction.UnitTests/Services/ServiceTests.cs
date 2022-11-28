using Api.AccountTransaction.UnitTests.MockData;
using Api.AccountTransactions.Exception;
using Api.AccountTransactions.Services;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.AccountTransaction.UnitTests.Services
{
    public class ServiceTests : IClassFixture<TransactionContextFixture>
    {
        private readonly TransactionContextFixture _fixture;
        private readonly TransactionService _service;

        public ServiceTests(TransactionContextFixture fixture)
        {
            _fixture = fixture;
            _service = new TransactionService(_fixture.DBContext);
        }

        [Fact]
        public async Task should_return_all_account_transactions()
        {
            // Arrange

            // Act
            var result = await _service.GetAccountTransactions();

            //Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task should_return_success_response_when_post_transaction()
        {
            // Arrange

            // Act
            var result = await _service.CreateTransaction(TransactionMockData.NewTransactionRequest);

            //Assert
            Assert.Equal(TransactionMockData.CreateResponse, result.Message);
        }

        [Fact]
        public async Task should_throw_exception_when_transaction_exists()
        {
            // Arrange

            // Act
            Func<Task> act = async () => await _service.CreateTransaction(TransactionMockData.ExistingTransactionRequest);

            //Assert
            await act.Should().ThrowAsync<HttpResponseException>();
        }

        [Fact]
        public async Task should_return_success_response_when_update_transaction()
        {
            // Arrange

            // Act
            var result = await _service.UpdateTransaction(TransactionMockData.ExistingTransactionRequest, TransactionMockData.ExistingTransactionId);

            //Assert
            Assert.Equal(TransactionMockData.UpdatedResponse, result.Message);
        }

        [Fact]
        public async Task should_throw_exception_when_transaction_doesnot_exists()
        {
            // Arrange

            // Act
            Func<Task> act = async () => await _service.UpdateTransaction(TransactionMockData.ExistingTransactionRequest, TransactionMockData.NonExistenceTransactionId);

            //Assert
            await act.Should().ThrowAsync<HttpResponseException>();
        }
    }
}
