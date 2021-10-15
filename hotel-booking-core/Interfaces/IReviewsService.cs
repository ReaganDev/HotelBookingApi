﻿using hotel_booking_dto;
using hotel_booking_dto.commons;
using hotel_booking_dto.ReviewDtos;
using hotel_booking_utilities.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hotel_booking_core.Interfaces
{
    public interface IReviewsService
    {

        Task<Response<Paginator.PageResult<IEnumerable<ReviewToReturnDto>>>> GetAllReviewsByHotelAsync(PagingDto paging,
            string hotelId);

        Task<Response<ReviewToReturnDto>> AddReviewAsync(AddReviewDto model, string customerId);
    }
}