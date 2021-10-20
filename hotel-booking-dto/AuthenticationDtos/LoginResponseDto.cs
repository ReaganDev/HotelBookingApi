﻿using System.Text.Json.Serialization;

namespace hotel_booking_dto.AuthenticationDtos
{
    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
