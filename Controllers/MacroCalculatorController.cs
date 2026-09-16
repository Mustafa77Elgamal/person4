using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using person_4.Data;
using person_4.DTOs.Macro;
using person_4.Models;

namespace person_4.Controllers
{
    [ApiController]
    [Route("api/macro-calculator")]
    public class MacroCalculatorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MacroCalculatorController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate(CalculateMacroDto dto)
        {
            if (dto.Weight <= 0)
            {
                return BadRequest("Weight must be greater than zero.");
            }

            if (dto.Height <= 0)
            {
                return BadRequest("Height must be greater than zero.");
            }

            if (dto.Age <= 0)
            {
                return BadRequest("Age must be greater than zero.");
            }

            decimal bmr;

            if (dto.Gender == Gender.Male)
            {
                bmr = (10 * dto.Weight) + (6.25m * dto.Height) - (5 * dto.Age) + 5;
            }
            else
            {
                bmr = (10 * dto.Weight) + (6.25m * dto.Height) - (5 * dto.Age) - 161;
            }

            decimal activityMultiplier;

            switch (dto.ActivityLevel)
            {
                case ActivityLevel.Sedentary:
                    activityMultiplier = 1.2m;
                    break;

                case ActivityLevel.Light:
                    activityMultiplier = 1.375m;
                    break;

                case ActivityLevel.Moderate:
                    activityMultiplier = 1.55m;
                    break;

                case ActivityLevel.Active:
                    activityMultiplier = 1.725m;
                    break;

                case ActivityLevel.VeryActive:
                    activityMultiplier = 1.9m;
                    break;

                default:
                    return BadRequest("Invalid activity level.");
            }

            decimal tdee = bmr * activityMultiplier;

            decimal proteinRatio = dto.ProteinRatio ?? 30;

            decimal carbsRatio = dto.CarbsRatio ?? 40;

            decimal fatRatio = dto.FatRatio ?? 30;

            if (proteinRatio + carbsRatio + fatRatio != 100)
            {
                return BadRequest("Ratios must equal 100.");
            }

            decimal proteinCalories = tdee * proteinRatio / 100;

            decimal carbsCalories = tdee * carbsRatio / 100;

            decimal fatCalories = tdee * fatRatio / 100;

            decimal proteinGrams = proteinCalories / 4;

            decimal carbsGrams = carbsCalories / 4;

            decimal fatGrams = fatCalories / 9;

            var calculation = new MacroCalculation
            {
                TraineeId = dto.TraineeId,

                Weight = dto.Weight,

                Height = dto.Height,

                Age = dto.Age,

                Gender = dto.Gender,

                ActivityLevel = dto.ActivityLevel,

                BMR = bmr,

                TDEE = tdee,

                ProteinGrams = proteinGrams,

                CarbsGrams = carbsGrams,

                FatGrams = fatGrams,

                CalculatedAt = DateTime.Now
            };

            _context.MacroCalculations.Add(calculation);

            await _context.SaveChangesAsync();

            var result = _mapper.Map<MacroResultDto>(calculation);

            return Ok(result);
        }


        [HttpGet("trainee/{traineeId}/latest")]
        public async Task<IActionResult> GetLatest(int traineeId)
        {
            var calculation = await _context.MacroCalculations.Where(x => x.TraineeId == traineeId).OrderByDescending(x => x.CalculatedAt).FirstOrDefaultAsync();

            if (calculation == null)
            {
                return NotFound("No calculation found.");
            }

            var result = _mapper.Map<MacroResultDto>(calculation);

            return Ok(result);
        }
    }
}

