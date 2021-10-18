using hotel_booking_dto;
using hotel_booking_models.Cloudinary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hotel_booking_core.Interfaces;
using System.Threading.Tasks;
using System.Security.Claims;
using hotel_booking_dto.CustomerDtos;
using Microsoft.Extensions.Logging;
using hotel_booking_utilities;
using hotel_booking_dto.commons;

namespace hotel_booking_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }


        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Response<string>>> Update([FromBody] UpdateCustomerDto model)
        {
            var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            _logger.LogInformation($"Update Attempt for user with id = {userId}");
            var result = await _customerService.UpdateCustomer(userId, model);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPatch("update-image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateImage([FromForm] AddImageDto imageDto)
        {
            string userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            _logger.LogInformation($"Update Image Attempt for user with id = {userId}");
            var result = await _customerService.UpdatePhoto(imageDto, userId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("AllCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "")]
        public async Task<IActionResult> GetAllCustomersAsync([FromQuery] PagingDto pagenator)
        {
            var result = await _customerService.GetAllCustomersAsync(pagenator);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("wishlist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCustomerWishList([FromQuery] PagingDto paging)
        {
            string customerId = _userManager.GetUserId(User);
            _logger.Information($"Retrieving the paginated wishlist for the customer with ID {customerId}");
            var result = await _customerService.GetCustomerWishList(customerId, paging);
            _logger.Information($"Retrieved the paginated wishlist for the customer with ID {customerId}");
            return StatusCode(result.StatusCode, result);
        }
    }
}
