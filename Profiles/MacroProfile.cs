using AutoMapper;
using person_4.DTOs.Macro;
using person_4.Models;

namespace person_4.Profiles
{
    public class MacroProfile : Profile
    {
        public MacroProfile()
        {
            CreateMap<MacroCalculation, MacroResultDto>();
        }
    }
}
