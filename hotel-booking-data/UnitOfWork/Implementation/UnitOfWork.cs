﻿using hotel_booking_data.Contexts;
using hotel_booking_data.Repositories.Abstractions;
using hotel_booking_data.Repositories.Implementations;
using hotel_booking_data.UnitOfWork.Abstraction;
using System;
using System.Threading.Tasks;

namespace hotel_booking_data.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAmenityRepository _amenities;
        private ICustomerRepository _customers;
        private IHotelRepository _hotels;
        private IManagerRepository _managers;
        private IPaymentRepository _payments;
        private IRoomRepository _rooms;
        private IWishListRepository _wishLists;
        private IRoomTypeRepository _roomType;
        private readonly HbaDbContext _context;

        public UnitOfWork(HbaDbContext context)
        {
            _context = context;
        }
        public IAmenityRepository Amenities => _amenities ??= new AmenityRepository(_context);

        public ICustomerRepository Customers => _customers ??= new CustomerRepository(_context);

        public IHotelRepository Hotels => _hotels ??= new HotelRepository(_context);

        public IManagerRepository Managers => _managers ??= new ManagerRepository(_context);

        public IPaymentRepository Payments => _payments ??= new PaymentRepository(_context);

        public IRoomRepository Rooms => _rooms ??= new RoomRepository(_context);

        public IWishListRepository WishLists => _wishLists ??= new WishListRepository(_context);

        public IRoomTypeRepository RoomType => _roomType ??= new RoomTypeRepository(_context);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
