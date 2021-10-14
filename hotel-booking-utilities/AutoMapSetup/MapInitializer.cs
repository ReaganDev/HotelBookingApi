﻿using AutoMapper;
using hotel_booking_dto;
using hotel_booking_dto.AmenityDtos;
using hotel_booking_dto.AppUserDto;
using hotel_booking_dto.AuthenticationDtos;
using hotel_booking_dto.commons;
using hotel_booking_dto.CustomerDtos;
using hotel_booking_dto.HotelDtos;
using hotel_booking_dto.RoomDtos;
using hotel_booking_models;
using System.Linq;

namespace hotel_booking_utilities.AutoMapSetup
{
    public class MapInitializer : Profile
    {
        public MapInitializer()
        {
            // Authentication Maps
            CreateMap<AppUser, RegisterUserDto>().ReverseMap();
            CreateMap<AppUser, RegisterUserDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, ResetPasswordDto>().ReverseMap();
            CreateMap<AppUser, UpdatePasswordDto>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserDto>().ReverseMap();


            // Amenity Maps
            CreateMap<Amenity, UpdateAmenityDto>().ReverseMap();
            CreateMap<Amenity, AddAmenityRequestDto>().ReverseMap();
            CreateMap<Amenity, AddAmenityResponseDto>().ReverseMap();
            CreateMap<Amenity, AmenityDto>().ReverseMap();


            // Hotel Maps
            CreateMap<Hotel, HotelBasicDto>()
                .ForMember(x => x.Thumbnail, y => y.MapFrom(src => src.Galleries.FirstOrDefault(opt => opt.IsFeature).ImageUrl))
                .ForMember(x => x.Rating, y => y.MapFrom(src => src.Ratings.Sum(r => r.Ratings) / (double)src.Ratings.Count))
                .ForMember(x => x.NumberOfReviews, y => y.MapFrom(c => c.Ratings.Count));


            CreateMap<RoomType, RoomInfoDto>()
                .ForMember(x => x.HotelName, y => y.MapFrom(c => c.Hotel.Name))
                .ForMember(x => x.DiscountPrice, y => y.MapFrom(c => c.Discount));

            CreateMap<GalleryDto, Gallery>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();
            CreateMap<Hotel, AddHotelDto>().ReverseMap();
            CreateMap<Hotel, AddHotelResponseDto>().ReverseMap();


            // Room Maps
            CreateMap<Room, AddRoomDto>().ReverseMap();
            CreateMap<Room, AddRoomResponseDto>().ReverseMap();
            CreateMap<Room, RoomDTo>();


            // RoomType Maps
            CreateMap<RoomType, RoomInfoDto>().ReverseMap();
            CreateMap<RoomType, RoomTypeByHotelDTo>();

            // Rating Maps
            CreateMap<Rating, HotelRatingsDTo>();

            // Gallery Maps
            CreateMap<Gallery, GalleryDto>().ReverseMap();
           
            //Customer
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
            CreateMap<Customer, GetUsersResponseDto>()
                .ForMember(x => x.FirstName, y => y.MapFrom(u => u.AppUser.FirstName))
                .ForMember(x => x.LastName, y => y.MapFrom(u => u.AppUser.LastName))
                .ForMember(x => x.Age, y => y.MapFrom(u => u.AppUser.Age))
                .ForMember(x => x.Id, y => y.MapFrom(u => u.AppUser.Id))
                .ForMember(x => x.Email, y => y.MapFrom(u => u.AppUser.Email))
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(u => u.AppUser.PhoneNumber))
                .ForMember(x => x.UserName, y => y.MapFrom(u => u.AppUser.UserName))
                .ForMember(x => x.Age, y => y.MapFrom(u => u.AppUser.Age))
                .ForMember(x => x.State, y => y.MapFrom(u => u.State))
                .ForMember(x => x.CreatedAt, y => y.MapFrom(u => u.AppUser.CreatedAt));
        }
    }
}
