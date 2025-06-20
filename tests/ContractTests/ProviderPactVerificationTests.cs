using PactNet.Verifier;
using Xunit.Abstractions;
using Disbursement.TestHelpers;

namespace Disbursement.ContractTests
{
    public class DisbursementProviderPactVerificationTests
    {

        private const string DisbursementApiUrl = "http://localhost:5062";
        private readonly ITestOutputHelper _output;

        public DisbursementProviderPactVerificationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void HonorsPactWithInstallmentConsumer()
        {
            var pactFile = Path.Combine("..", "..", "..", "..", "..", "pacts", "installment-disbursement.json");

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
