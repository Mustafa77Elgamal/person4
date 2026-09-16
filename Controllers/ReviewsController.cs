using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using person_4.Data;
using person_4.DTOs.Reviews;
using person_4.Models;

namespace person_4.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReviewsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewDto dto)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
            {
                return BadRequest("Rating must be between 1 and 5.");
            }

            var existingReview = await _context.Reviews.FirstOrDefaultAsync(x => x.SubscriptionId == dto.SubscriptionId);

            if (existingReview != null)
            {
                return Conflict("This subscription already has a review.");
            }

            var review = new Review
            {
                SubscriptionId = dto.SubscriptionId,

                Rating = dto.Rating,

                Comment = dto.Comment,

                CreatedAt = DateTime.Now
            };

            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();

            var result = _mapper.Map<ReviewDto>(review);

            return Created($"api/reviews/{review.Id}", result);
        }

        [HttpGet("/api/coaches/{coachId}/reviews")]
        public async Task<IActionResult> GetForCoach(int coachId)
        {
            var reviews = await _context.Reviews.Where(x => x.CoachId == coachId).OrderByDescending(x => x.CreatedAt).ToListAsync();

            var result = _mapper.Map<List<ReviewDto>>(reviews);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (review == null)
            {
                return NotFound("Review not found.");
            }

            _context.Reviews.Remove(review);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

