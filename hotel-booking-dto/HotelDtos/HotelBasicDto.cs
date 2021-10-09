﻿using hotel_booking_models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hotel_booking_dto.commons
{
    public class HotelBasicDto
    {
        [Display(Name = "ManagerId")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Thumbnails { get; set; }
        public string Ratings { get; set; }
        public ICollection<GalleryDto> Galleries { get; set; }
    }
    
}