using FoodDelivery.DTO.Review;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseReviewDTO>>> GetReview()
        {
            var reviewList = _service.GetReviewList();
            return Ok(reviewList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseReviewDTO>> GetReviewById(int id)
        {
            var existingReview = await _service.GetReviewById(id);
            return this.ToActionResult(existingReview);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseReviewDTO>> PostReview(RequestReviewDTO requestReview)
        {
            var createdReview = await _service.PostReview(requestReview);
            return this.ToCreatedAtActionResult(createdReview, nameof(GetReviewById), new { id = createdReview.Value?.ReviewId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, RequestReviewDTO requestReview)
        {
            var success = await _service.PutOrder(id, requestReview);
            return this.ToActionResult(success);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var success = await _service.DeleteReview(id);
            return this.ToActionResult(success);
        }
    }
}
