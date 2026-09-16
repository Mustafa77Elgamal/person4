namespace person_4.Models
{
    public class MacroCalculation
    {
        public int Id { get; set; }

        public int TraineeId { get; set; }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public ActivityLevel ActivityLevel { get; set; }
        public decimal BMR { get; set; }

        public decimal TDEE { get; set; }

        public decimal ProteinGrams { get; set; }

        public decimal CarbsGrams { get; set; }

        public decimal FatGrams { get; set; }

        public DateTime CalculatedAt { get; set; }
    }
}
