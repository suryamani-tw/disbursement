using PactNet.Verifier;
using Xunit.Abstractions;
using Disbursement.TestHelpers;

namespace Disbursement.ContractTests
{
    public class DisbursementApiProviderPactTests
    {

        private static readonly string DisbursementApiUrl = Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:5062";
        private static readonly string Type = "api";
        private readonly ITestOutputHelper _output;

        public DisbursementApiProviderPactTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void HonorsPactWithInstallmentConsumer()
        {
            var pactFile = Path.Combine("..", "..", "..", "..", "..", "pacts",Type, "installment-disbursement.json");

            var config = new PactVerifierConfig
            {
                Outputters = new[] { new XUnitOutput(_output) },
                LogLevel = PactNet.PactLogLevel.Debug,
            };

            new PactVerifier("disbursement")
                .WithHttpEndpoint(new Uri(DisbursementApiUrl))
                .WithFileSource(new FileInfo(pactFile))
                .Verify();
        }
    }
}
