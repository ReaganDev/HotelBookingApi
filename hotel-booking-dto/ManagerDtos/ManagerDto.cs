﻿using hotel_booking_models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel_booking_dto.ManagerDtos
{
    public class ManagerDto
    {
        [Key]
        public string AppUserId { get; set; }
        public string CompanyName { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }
        public string CompanyAddress { get; set; }
        public string State { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }

        public AppUser AppUser { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }
}
