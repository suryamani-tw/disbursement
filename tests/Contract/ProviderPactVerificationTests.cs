using PactNet.Verifier;
using Xunit.Abstractions;
using Disbursement.TestHelpers;

namespace Disbursement.ContractTests
{
    public class DisbursementProviderPactVerificationTests
    {

        private static readonly string DisbursementApiUrl = Environment.GetEnvironmentVariable("API_URL") ;
        private static readonly string Type = "api";
        private readonly ITestOutputHelper _output;

        public DisbursementProviderPactVerificationTests(ITestOutputHelper output)
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

            new PactVerifier(config)
                 .ServiceProvider("disbursement", new Uri(DisbursementApiUrl))
                 .WithFileSource(new FileInfo(pactFile))
                 .Verify();
        }
    }
}
