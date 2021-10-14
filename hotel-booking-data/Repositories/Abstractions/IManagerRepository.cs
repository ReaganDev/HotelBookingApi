﻿using hotel_booking_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel_booking_data.Repositories.Abstractions
{
    public interface IManagerRepository : IGenericRepository<Manager>
    {
        Task<Manager> GetManagerStatistics(string managerId);
        Task<Manager> GetManagerAsync(string managerId);
    }
}
