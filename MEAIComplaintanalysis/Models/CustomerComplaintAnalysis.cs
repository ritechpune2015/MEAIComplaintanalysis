namespace MEAIComplaintanalysis.Models
{
    public class CustomerComplaintAnalysis
    {
        public Sentiment Sentiment { get; set; }

        public ComplaintCategory Category { get; set; }

        public ComplaintPriority Priority { get; set; }

        public bool RequiresRefund { get; set; }

        public double Confidence { get; set; }

        public string Summary { get; set; } = string.Empty;
    }
}
