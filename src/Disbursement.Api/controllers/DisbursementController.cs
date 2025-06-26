using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Disbursement.Api.Messaging;

[ApiController]
[Route("api/[controller]")]
public class DisbursementController : ControllerBase
{
    private readonly IDisbursementMessagePublisher _publisher;
    private readonly ILogger<DisbursementController> _logger;

    public DisbursementController(IDisbursementMessagePublisher publisher, ILogger<DisbursementController> logger)
    {
        _publisher = publisher;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDisbursement([FromBody] DisbursementRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.LoanId) || request.Amount <= 0)
        {
            return BadRequest("Invalid disbursement request.");
        }

        var disbursementId = Guid.NewGuid().ToString();
        // Simulate processing disbursement
        var message = new Disbursement.Api.Messaging.DisbursementStatusMessage
        {
            Amount = request.Amount,
            DisbursementId = disbursementId,
            LoanId = request.LoanId,
            Status = "Pending"
        };
        await _publisher.PublishDisbursementAsync(message);

        var result = new
        {
            DisbursementId = disbursementId,
            Message = "Success"
        };

        return Ok(result);
    }

    [HttpGet("health")]
    public IActionResult Health() => Ok("Healthy");
}
