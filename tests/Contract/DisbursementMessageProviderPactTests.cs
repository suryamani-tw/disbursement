using System;
using System.IO;
using System.Text.Json;
using PactNet.Verifier;
using Xunit;
using Xunit.Abstractions;
using Disbursement.TestHelpers;
using Disbursement.Api.Messaging;

namespace Disbursement.ContractTests
{
    public class DisbursementMessageProviderPactTests
    {
        private readonly ITestOutputHelper _output;
        private readonly IPactVerifier _verifier;
        private const string PactMessagesEndpoint = "http://localhost:49152/pact-messages";

        public DisbursementMessageProviderPactTests(ITestOutputHelper output)
        {
            _output = output;
            var config = new PactVerifierConfig
            {
                Outputters = new[] { new XUnitOutput(_output) },
                LogLevel = PactNet.PactLogLevel.Debug
            };
            _verifier = new PactVerifier("disbursement", config).WithHttpEndpoint(new Uri(PactMessagesEndpoint));
        }
        
        [Fact]
        public void EnsureDisbursementApiHonoursMessagePactWithConsumer()
        {
            var pactFile = Path.Combine("..", "..", "..", "..", "..", "pacts", "message", "installment-disbursement.json");

            // Configuration is already set in the constructor, using _verifier directly
            var defaultSettings = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            _verifier.WithMessages(scenarios =>
            {
                scenarios.Add("A disbursement status message", builder =>
                {
                    builder.WithMetadata(new { ContentType = "application/json", Encoded = false })
                           .WithContent(() => new DisbursementStatusMessage
                           {
                               Amount = 1000.0m,
                               DisbursementId = "abc-123",
                               LoanId = "IL1234",
                               Status = "Success"
                           });
                });
            }, defaultSettings)
                .WithFileSource(new FileInfo(pactFile))
                .Verify();
        }
    }
}
