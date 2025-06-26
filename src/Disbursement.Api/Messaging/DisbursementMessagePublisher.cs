using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Disbursement.Api.Messaging
{
    public class DisbursementStatusMessage
    {
        [JsonPropertyName("Amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("DisbursementId")]
        public string DisbursementId { get; set; } = string.Empty;
        [JsonPropertyName("LoanId")]
        public string LoanId { get; set; } = string.Empty;
        [JsonPropertyName("Status")]
        public string Status { get; set; } = string.Empty;
    }

    public interface IDisbursementMessagePublisher
    {
        Task PublishDisbursementAsync(DisbursementStatusMessage message);
    }

    public class ConsoleDisbursementMessagePublisher : IDisbursementMessagePublisher
    {
        private readonly ILogger<ConsoleDisbursementMessagePublisher> _logger;
        public ConsoleDisbursementMessagePublisher(ILogger<ConsoleDisbursementMessagePublisher> logger)
        {
            _logger = logger;
        }
        public Task PublishDisbursementAsync(DisbursementStatusMessage message)
        {
            _logger.LogInformation($"Published disbursement message: {System.Text.Json.JsonSerializer.Serialize(message)}");
            return Task.CompletedTask;
        }
    }
}
