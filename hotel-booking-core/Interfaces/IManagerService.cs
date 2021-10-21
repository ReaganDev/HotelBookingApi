﻿using hotel_booking_dto;
using hotel_booking_dto.HotelDtos;
using hotel_booking_dto.ManagerDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hotel_booking_core.Interfaces
{
    public interface IManagerService
    {
        Task<Response<IEnumerable<HotelBasicDto>>> GetAllHotelsAsync(string managerId);
        Task<Response<string>> SoftDeleteManagerAsync(string managerId);
        Task<Response<string>> AddManagerRequest(ManagerRequestDto managerRequest);
        Task<bool> SendManagerInvite(string email);
        Task<Response<bool>> CheckTokenExpiring(string email, string token);
        Task<Response<IEnumerable<ManagerRequestResponseDTo>>> GetAllManagerRequest();
        Task<Response<ManagerResponseDto>> AddManagerAsync(ManagerDto manager);
        
    }
}
