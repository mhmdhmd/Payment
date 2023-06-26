using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Payment.Application.Interfaces.Repositories;
using Payment.Domain.DomainModels.Order;
using Payment.Domain.DomainModels.Payment;
using Payment.Domain.DomainModels.Shopper;
using Payment.Infrastructure.Persistence.DbContext;
using Payment.Infrastructure.Persistence.Repositories;

namespace Payment.Tests.Infrastructure.Repositories
{
    [TestFixture]
    public class PaymentHistoryRepositoryTests
    {
        private IMyDbContext dbContext;
        private PaymentHistoryRepository repository;

        [SetUp]
        public void Setup()
        {
            dbContext = Substitute.For<IMyDbContext>();
            repository = new PaymentHistoryRepository(dbContext);
        }

        [Test]
        public async Task GetByIdAsync_ExistingId_ShouldReturnPaymentHistory()
        {
            // Arrange
            var paymentHistory = new PaymentHistoryEntity { Id = 1, PayDate = DateTime.Now, Status = 1 };
            dbContext.GetDbSet<PaymentHistoryEntity>().FindAsync(1).Returns(paymentHistory);

            // Act
            var result = await repository.GetByIdAsync(1);

            // Assert
            Assert.AreEqual(paymentHistory, result);
        }

        [Test]
        public async Task GetByIdAsync_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            dbContext.GetDbSet<PaymentHistoryEntity>().FindAsync(Arg.Any<int>()).ReturnsNull();

            // Act
            var result = await repository.GetByIdAsync(1);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task AddAsync_ValidPaymentHistory_ShouldAddToDbContext()
        {
            // Arrange
            var paymentHistory = new PaymentHistoryEntity { Id = 1, PayDate = DateTime.Now, Status = 1 };

            // Act
            await repository.AddAsync(paymentHistory);

            // Assert
            await dbContext.GetDbSet<PaymentHistoryEntity>().Received(1).AddAsync(paymentHistory);
            await dbContext.Received(1).SaveChangesAsync();
        }

        [Test]
        public async Task UpdateAsync_ValidPaymentHistory_ShouldUpdateInDbContext()
        {
            // Arrange
            var paymentHistory = new PaymentHistoryEntity { Id = 1, PayDate = DateTime.Now, Status = 1 };

            // Act
            await repository.UpdateAsync(paymentHistory);

            // Assert
            dbContext.GetDbSet<PaymentHistoryEntity>().Received(1).Update(paymentHistory);
            await dbContext.Received(1).SaveChangesAsync();
        }

        [Test]
        public async Task DeleteAsync_ValidPaymentHistory_ShouldRemoveFromDbContext()
        {
            // Arrange
            var paymentHistory = new PaymentHistoryEntity { Id = 1, PayDate = DateTime.Now, Status = 1 };

            // Act
            await repository.DeleteAsync(paymentHistory);

            // Assert
            dbContext.GetDbSet<PaymentHistoryEntity>().Received(1).Remove(paymentHistory);
            await dbContext.Received(1).SaveChangesAsync();
        }
    }
}