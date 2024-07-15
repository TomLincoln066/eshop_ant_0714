using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Reviews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReviewController : ControllerBase
    {
        private readonly ICustomerReviewsService _customerReviewsService;

        public CustomerReviewController(ICustomerReviewsService customerReviewsService)
        {
            _customerReviewsService = customerReviewsService;
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _customerReviewsService.GetAllReviewsAsync();
            if (!reviews.Any())
            {
                return NotFound(new { error = "No reviews found, please try later"});
            }
            return Ok(reviews);
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> CreateReview(CustomerReviewRequestModel model)
        {
            var response = new { Message = "Review is added and has pending approval!" };
            if (!ModelState.IsValid)
                // 400 status code
                return BadRequest();
            //model.Status = ReviewStatus.Pending;
            if(await _customerReviewsService.AddReviewsAsync(model) >0)
            {
                return Ok(response);
            }

            return NoContent();
        }
        
        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> UpdateReview(CustomerReviewRequestModel model)
        {
            var review = await _customerReviewsService.GetReviewsByIdAsync(model.Id);
            if (review == null)
            {
                return NotFound(new { errorMessage = "No review found for this id" });
            }
            var response = new { Message = "Review is updated" };
            if (await _customerReviewsService.UpdateReviewsAsync(model) > 0)
                return Ok(response);
            return NoContent();
        }
        
        [HttpDelete]
        [Route("delete-{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReviews(int id)
        {
            var review = await _customerReviewsService.GetReviewsByIdAsync(id);
            if (review == null)
            {
                return NotFound(new { errorMessage = "No review found for this id" });
            }
            var response = new { Message = "Review is deleted" };
            if (await _customerReviewsService.DeleteReviewsAsync(id) > 0)
                return Ok(response);
            return NoContent();
        }
        
        [HttpGet]
        [Route("{id:int}", Name = "GetReviewsDetails")]
        [Authorize]
        public async Task<IActionResult> GetReviewsDetails(int id)
        {
            var review = await _customerReviewsService.GetReviewsByIdAsync(id);
            if (review == null)
            {
                return NotFound(new { errorMessage = "No review found for this id" });
            }

            return Ok(review);
        }
        
        [HttpGet]
        [Route("user/{userId:int}")]
        [Authorize]
        public async Task<IActionResult> GetReviewsByUser(int userId)
        {
            var reviews = await _customerReviewsService.GetReviewsByUser(userId);
            if (!reviews.Any())
            {
                return NotFound(new { errorMessage = "No reviews found for this user" });
            }

            return Ok(reviews);
        }
        
        [HttpGet]
        [Route("product/{productId}")]
        [Authorize]
        public async Task<IActionResult> GetReviewsByProduct(string productId)
        {
            var reviews = await _customerReviewsService.GetReviewsByProduct(productId);
            if (!reviews.Any())
            {
                return NotFound(new { errorMessage = "No reviews found for this user" });
            }

            return Ok(reviews);
        }

        [HttpGet]
        [Route("year/{year:int}")]
        [Authorize]
        public async Task<IActionResult> GetReviewsByYear(int year)
        {
            var reviews = await _customerReviewsService.GetReviewsByYear(year);
            if (!reviews.Any())
            {
                return NotFound(new { errorMessage = "No reviews found for this year" });
            }

            return Ok(reviews);
        }
        
        [HttpPut]
        [Route("approve/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveReview(int id)
        {
            if (await _customerReviewsService.UpdateReviewStatusAsync(id, ReviewStatus.Approved))
            {
                return Ok(new { Message = "Review is approved" });
            }

            return NotFound(new { errorMessage = "No review found for this id" });
        }

        [HttpPut]
        [Route("reject/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectReview(int id)
        {
            if (await _customerReviewsService.UpdateReviewStatusAsync(id, ReviewStatus.Rejected))
            {
                return Ok(new { Message = "Review is rejected" });
            }

            return NotFound(new { errorMessage = "No review found for this id" });
        }



    }
}
