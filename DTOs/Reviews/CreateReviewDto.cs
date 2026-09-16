namespace person_4.DTOs.Reviews
{
    public class CreateReviewDto
    {
        public int SubscriptionId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
