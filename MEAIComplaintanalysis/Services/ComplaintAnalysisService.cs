using MEAIComplaintanalysis.Models;
using Microsoft.Extensions.AI;

namespace MEAIComplaintanalysis.Services
{
    public class ComplaintAnalysisService : IComplaintAnalysisService
    {
        private readonly IChatClient _chatClient;
        private readonly IConfiguration _configuration;

        public ComplaintAnalysisService(
            IChatClient chatClient,
            IConfiguration configuration)
        {
            _chatClient = chatClient;
            _configuration = configuration;
        }

        public async Task<CustomerComplaintAnalysis> AnalyzeAsync(
            string complaint,
            CancellationToken cancellationToken = default)
        {
            var messages = new List<ChatMessage>
        {
            new ChatMessage(
                ChatRole.System,
                """
                You are a customer complaint analysis assistant.

                Analyze the customer's complaint carefully.

                Determine:
                - sentiment
                - complaint category
                - priority
                - whether a refund may be required
                - confidence
                - short summary

                Rules:

                1. Do not invent facts.
                2. Use only information available in the complaint.
                3. Use Critical priority only for serious business-impacting
                   or urgent situations.
                4. Set RequiresRefund to true only when the complaint
                   indicates a refund may be required.
                5. Confidence must be between 0 and 1.
                6. Keep the summary short and factual.
                """
            ),

            new ChatMessage(
                ChatRole.User,
                $"Analyze this customer complaint:\n\n{complaint}"
            )
        };

            var options = new ChatOptions
            {
                Temperature = 0.2f,
                MaxOutputTokens = 500
            };

            var response =
                await _chatClient.GetResponseAsync<CustomerComplaintAnalysis>(
                    messages,
                    options,
                    cancellationToken: cancellationToken);

            return response.Result;
        }
    }

}
