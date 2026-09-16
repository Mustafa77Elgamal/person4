using person_4.Models;

namespace person_4.DTOs.Macro
{
    public class CalculateMacroDto
    {
        public int TraineeId { get; set; }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public ActivityLevel ActivityLevel { get; set; }

        public decimal? ProteinRatio { get; set; }

        public decimal? CarbsRatio { get; set; }

        public decimal? FatRatio { get; set; }
    }
}
