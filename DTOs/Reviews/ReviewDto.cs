namespace person_4.DTOs.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public int SubscriptionId { get; set; }

        public int TraineeId { get; set; }

        public int CoachId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
