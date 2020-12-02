using System.Threading.Tasks;

namespace Shared.AzureServiceBusProxy
{
    public interface IService
    {
        string ErrorMessage { get; set; }
        bool HasError { get; }
        string ConnectionString { get; set; }

        Task<MessageFacade> CreateMessageAsync(string queueName, string message);

        Task<MessageFacade> PeekMessageAsync(string queueName);

        Task<MessageFacade> ReadMessageAsync(string queueName);

    }
}