using MEAIComplaintanalysis.Models;
using MEAIComplaintanalysis.Services;
using Microsoft.AspNetCore.Mvc;

namespace MEAIComplaintanalysis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintAnalysisService _analysisService;

        public ComplaintController(
            IComplaintAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze(
            [FromBody] ComplaintRequest request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Complaint))
            {
                return BadRequest(new
                {
                    message = "Complaint is required."
                });
            }

            var result =
                await _analysisService.AnalyzeAsync(
                    request.Complaint,
                    cancellationToken);

            return Ok(result);
        }
    }
}
