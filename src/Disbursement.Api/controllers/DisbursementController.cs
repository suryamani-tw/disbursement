
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DisbursementController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateDisbursement([FromBody] DisbursementRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.LoanId) || request.Amount <= 0)
        {
            return BadRequest("Invalid disbursement request.");
        }

        // Simulate processing disbursement
        var result = new
        {
            DisbursementId = Guid.NewGuid().ToString(),
            Message = "Success"
        };

        return Ok(result);
    }
    [HttpGet("health")]
    public IActionResult Health() => Ok("Healthy");
}
