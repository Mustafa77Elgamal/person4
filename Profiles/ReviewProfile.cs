using AutoMapper;
using person_4.DTOs.Reviews;
using person_4.Models;

namespace person_4.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>();
        }
    }
}