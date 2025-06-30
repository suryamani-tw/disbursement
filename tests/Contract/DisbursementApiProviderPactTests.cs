using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PactNet.Verifier;
using Xunit;
using Xunit.Abstractions;
using Disbursement.TestHelpers;

namespace Disbursement.ContractTests
{
    // Fixture to start and stop the API on a real TCP port
    public class DisbursementApiFixture : IDisposable
    {
        public readonly IHost Server;
        public readonly Uri ServerUri;

        public DisbursementApiFixture()
        {
            // Use a fixed port for simplicity; you can randomize if needed
            var port = 5062;
            ServerUri = new Uri($"http://localhost:{port}");
            Server = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(ServerUri.ToString());
                    webBuilder.UseStartup<Disbursement.Api.Startup>();
                })
                .Build();
            Server.Start();
        }

        public void Dispose()
        {
            Server.Dispose();
        }
    }

    public class DisbursementApiProviderPactTests : IClassFixture<DisbursementApiFixture>
    {
        private static readonly string Type = "api";
        private readonly ITestOutputHelper _output;
        private readonly DisbursementApiFixture _fixture;

        public DisbursementApiProviderPactTests(DisbursementApiFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public void HonorsPactWithInstallmentConsumer()
        {
            var pactFile = Path.Combine("..", "..", "..", "..", "..", "pacts", Type, "installment-disbursement.json");

            var config = new PactVerifierConfig
            {
                Outputters = new[] { new XUnitOutput(_output) },
                LogLevel = PactNet.PactLogLevel.Debug,
            };

            new PactVerifier("disbursement")
                .WithHttpEndpoint(_fixture.ServerUri)
                .WithFileSource(new FileInfo(pactFile))
                .Verify();
        }
    }
}
