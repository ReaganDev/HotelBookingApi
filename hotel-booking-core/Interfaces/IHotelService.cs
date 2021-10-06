﻿using hotel_booking_dto;
using hotel_booking_dto.commons;
using hotel_booking_dto.HotelDtos;
using hotel_booking_utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel_booking_core.Interfaces
{
    public interface IHotelService
    {
        Task<IList<HotelBasicDto>> GetHotelsByRatingsAsync(Paging paging);

        /// <summary>
        /// Fetches all hotels in database. Returns a List of all registered hotels.
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<Response<List<GetHotelDto>>> GetAllHotelsAsync(Paginator paging);

        /// <summary>
        /// Fetches and hotel using it's Id. Returns the hotel object and it's child entities
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Response<GetHotelDto> GetHotelById(string id);

        /// <summary>
        /// Updates an hotel asynchronously and returns update hotel response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Response<UpdateHotelDto>> UpdateHotelAsync(UpdateHotelDto model);
    }
}
