namespace person_4.Models
{
    public class Review
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