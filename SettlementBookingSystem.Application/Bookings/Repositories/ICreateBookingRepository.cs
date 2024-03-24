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
        public BookingDto AddBooking(string strName, string strBookingTime);
        public List<Booking> GetBookings();
        public bool BookingExists(BookingDto BookingDTO);
        public bool BookingExistsByTime(string BookingTime);
        public Booking? GetBookingByTime(string BookingTime);

    }
}
