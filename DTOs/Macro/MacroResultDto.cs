namespace person_4.DTOs.Macro
{
    public class MacroResultDto
    {
        public int Id { get; set; }
        public decimal BMR { get; set; }

        public decimal TDEE { get; set; }

        public decimal ProteinGrams { get; set; }

        public decimal CarbsGrams { get; set; }

        public decimal FatGrams { get; set; }
    }
}
