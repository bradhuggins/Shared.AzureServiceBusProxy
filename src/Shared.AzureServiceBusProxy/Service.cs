#region Using Statements
using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;
#endregion

namespace Shared.AzureServiceBusProxy
{
    // https://docs.microsoft.com/en-us/azure/service-bus-messaging/

    public class Service : IService
    {
        public string ErrorMessage { get; set; }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(this.ErrorMessage); }
        }

        private string _connectionString;

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    throw new System.Exception("Error: ConnectionString not set!");
                }
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        public async Task<MessageFacade> CreateMessageAsync(string queueName, string message)
        {
            MessageFacade toReturn = null;
            try
            {
                await using ServiceBusClient client = new ServiceBusClient(this.ConnectionString);

                ServiceBusSender sender = client.CreateSender(queueName);

                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);

                await sender.SendMessageAsync(serviceBusMessage);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
            return toReturn;
        }

        public async Task<MessageFacade> PeekMessageAsync(string queueName)
        {
            MessageFacade toReturn = null;
            try
            {
                await using ServiceBusClient client = new ServiceBusClient(this.ConnectionString);

                ServiceBusReceiver receiver = client.CreateReceiver(queueName);

                ServiceBusReceivedMessage peekedMessage = await receiver.PeekMessageAsync();

                if (peekedMessage != null)
                {
                    toReturn = new MessageFacade()
                    {
                        MessageId = peekedMessage.MessageId,
                        MessageText = peekedMessage.Body.ToString(),
                        Subject = peekedMessage.Subject
                    };
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
            return toReturn;
        }

        public async Task<MessageFacade> ReadMessageAsync(string queueName)
        {
            MessageFacade toReturn = null;
            try
            {
                await using ServiceBusClient client = new ServiceBusClient(this.ConnectionString);

                ServiceBusReceiver receiver = client.CreateReceiver(queueName);

                ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

                toReturn = new MessageFacade()
                {
                    MessageId = receivedMessage.MessageId,
                    MessageText = receivedMessage.Body.ToString(),
                    Subject = receivedMessage.Subject
                };                

            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
            return toReturn;
        }


        public async Task<MessageFacade> PeekSubscriptionMessageAsync(string topicName, string subscription)
        {
            MessageFacade toReturn = null;
            try
            {
                await using ServiceBusClient client = new ServiceBusClient(this.ConnectionString);

                ServiceBusReceiver receiver = client.CreateReceiver(topicName, subscription);

                ServiceBusReceivedMessage peekedMessage = await receiver.PeekMessageAsync();

                if (peekedMessage != null)
                {
                    toReturn = new MessageFacade()
                    {
                        MessageId = peekedMessage.MessageId,
                        MessageText = peekedMessage.Body.ToString(),
                        Subject = peekedMessage.Subject
                    };
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
            return toReturn;
        }

        public async Task<MessageFacade> ReadSubscriptionMessageAsync(string topicName, string subscription)
        {
            MessageFacade toReturn = null;
            try
            {
                await using ServiceBusClient client = new ServiceBusClient(this.ConnectionString);

                ServiceBusReceiver receiver = client.CreateReceiver(topicName, subscription);

                ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

                toReturn = new MessageFacade()
                {
                    MessageId = receivedMessage.MessageId,
                    MessageText = receivedMessage.Body.ToString(),
                    Subject = receivedMessage.Subject
                };

            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
            return toReturn;
        }

    }
}
