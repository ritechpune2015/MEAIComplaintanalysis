using MEAIComplaintanalysis.Models;

namespace MEAIComplaintanalysis.Services
{
    public interface IComplaintAnalysisService
    {
        Task<CustomerComplaintAnalysis> AnalyzeAsync(
            string complaint,
            CancellationToken cancellationToken = default);

    }
}
