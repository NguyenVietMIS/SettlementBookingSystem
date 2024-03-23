using SettlementBookingSystem.Application.Bookings.Commands;
using SettlementBookingSystem.Application.Bookings.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementBookingSystem.Application.Bookings.Repositories
{
    public interface ICreateBookingRepository
    {
        public Task AddBooking(Booking Booking);
        public Task<List<Booking>> GetBookings();
        public Task<bool> BookingExists(BookingDto BookingDTO);
        public bool BookingExistsByTime(string BookingTime);
        public Task<Booking?> GetBookingByTime(string BookingTime);

    }
}
