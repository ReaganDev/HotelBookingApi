﻿using hotel_booking_core.Interfaces;
using hotel_booking_dto;
using hotel_booking_dto.commons;
using hotel_booking_dto.HotelDtos;
using hotel_booking_models;
using hotel_booking_utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hotel_booking_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly UserManager<AppUser> _userManager;

        public HotelController(IHotelService hotelService, UserManager<AppUser> userManager)
        {
            _hotelService = hotelService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("all-hotels")]
        public async Task<IActionResult> GetAllHotels([FromQuery] Paginator paging)
        {
            var response = await _hotelService.GetAllHotelsAsync(paging);
            return StatusCode(response.StatusCode, response);
        }

        [AllowAnonymous]
        [HttpGet("{hotelId}")]
        public IActionResult GetHotelById(string hotelId)
        {
            var response = _hotelService.GetHotelById(hotelId);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{hotelId}")]
        public async Task<IActionResult> UpdateHotel(string hotelId, [FromBody] UpdateHotelDto update)
        {
            var response = await _hotelService.UpdateHotelAsync(hotelId, update);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet]
        [Route("top-hotels")]
        public async Task<IActionResult> HotelsByRatingsAsync([FromQuery] Paging paging)
        {
            var result = await _hotelService.GetHotelsByRatingsAsync(paging);
            var response = new Response<List<HotelBasicDto>>(StatusCodes.Status200OK, true, "List of Hotels by ratings", result);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("top-deals")]
        public async Task<IActionResult> TopDealsAsync([FromQuery] Paging paging)
        {
            var result = await _hotelService.GetTopDealsAsync(paging);
            var response = new Response<List<RoomInfoDTo>>(StatusCodes.Status200OK, true, "List of Top Deals", result);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelRoomsByPriceAsync([FromQuery]PriceDto pricing)
        {
            var result = await _hotelService.GetRoomByPriceAsync(pricing);
            var response = new Response<List<RoomInfoDTo>>(StatusCodes.Status200OK, true, "List of Rooms By Price", result);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("{id}/rooms")]
        public async Task<IActionResult> GetAvailableHotelRoomAsync([FromQuery] Paginator paginator, string id)
        {
            var rooms = await _hotelService.GetAvailableRoomByHotel(paginator, id);
            return Ok(rooms);
        }

        [HttpGet]
        [Route("room/{id}")]
        public IActionResult HotelRoomById(string id)
        {
            var room = _hotelService.GetHotelRooomById(id);
            return Ok(room);
        }

        [HttpGet]
        [Route("ratings/{id}")]
        public async Task<IActionResult> HotelRatingsAsync(string id)
        {
            var rating = await _hotelService.GetHotelRatings(id);
            return Ok(rating);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddHotel([FromBody] AddHotelDto hotelDto)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var result = await _hotelService.AddHotel(loggedInUser.Id, hotelDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("rooms/{hotelId}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddHotelRoom(string hotelId, [FromBody] AddRoomDto roomDto)
        {
            var result = await _hotelService.AddHotelRoom(hotelId, roomDto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
