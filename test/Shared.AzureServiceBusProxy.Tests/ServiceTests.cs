using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Shared.AzureServiceBusProxy.Tests
{
    [TestClass]
    public class ServiceTests
    {
        private const string _connectionString = "[ENTER_CONNECTION_STRING_HERE]";
        private const string _queueName = "myqueue";
        private const string _topicName = "mytopic";
        private const string _subscription = "mysubscription";

        private string SampleMessage
        {
            get
            {
                return Guid.NewGuid().ToString()
                + "/t" + DateTime.UtcNow
                + "/t" + "unit test";
            }
        }

        [TestMethod]
        public void HasError_True_Test()
        {
            // Arrange
            Service target = new Service();
            target.ErrorMessage = "error";

            // Act
            var actual = target.HasError;

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasError_False_Test()
        {
            // Arrange
            Service target = new Service();

            // Act
            var actual = target.HasError;

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ConnectionString_Error_Test()
        {
            // Arrange
            Service target = new Service();
            bool hasError = false;
            // Act
            try
            {
                var connectionString = target.ConnectionString;
            }
            catch (Exception ex)
            {
                hasError = true;
            }

            // Assert
            Assert.IsTrue(hasError);
        }

        [TestMethod]
        public async Task CreateMessageAsync_Test()
        {
            // Arrange
            Service target = new Service();
            target.ConnectionString = _connectionString;

            // Act
            await target.CreateMessageAsync(_queueName, this.SampleMessage);

            // Assert
            Assert.IsFalse(target.HasError);
        }

        [TestMethod]
        public async Task PeekMessageAsync_Test()
        {
            // Arrange
            Service target = new Service();
            target.ConnectionString = _connectionString;
            await target.CreateMessageAsync(_queueName, this.SampleMessage);

            // Act
            var actual = await target.PeekMessageAsync(_queueName);

            // Assert
            Assert.IsFalse(target.HasError);
        }

        [TestMethod]
        public async Task ReadMessageAsync_Test()
        {
            // Arrange
            Service target = new Service();
            target.ConnectionString = _connectionString;
            await target.CreateMessageAsync(_queueName, this.SampleMessage);

            // Act
            var actual = await target.ReadMessageAsync(_queueName);

            // Assert
            Assert.IsFalse(target.HasError);
        }

        [TestMethod]
        public async Task PeekSubscriptionMessageAsync_Test()
        {
            // Arrange
            Service target = new Service();
            target.ConnectionString = _connectionString;
            await target.CreateMessageAsync(_topicName, this.SampleMessage);

            // Act
            var actual = await target.PeekSubscriptionMessageAsync(_topicName, _subscription);

            // Assert
            Assert.IsFalse(target.HasError);
        }

        [TestMethod]
        public async Task ReadSubscriptionMessageAsync_Test()
        {
            // Arrange
            Service target = new Service();
            target.ConnectionString = _connectionString;
            await target.CreateMessageAsync(_topicName, this.SampleMessage);

            // Act
            var actual = await target.ReadSubscriptionMessageAsync(_topicName, _subscription);

            // Assert
            Assert.IsFalse(target.HasError);
        }
    }
}
