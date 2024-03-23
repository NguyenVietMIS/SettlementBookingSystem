using SettlementBookingSystem.Application.Bookings;
using SettlementBookingSystem.Application.Bookings.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementBookingSystem.Application.Bookings.Repositories
{
    public class CreateBookingRepository : ICreateBookingRepository
    {
        private List<Booking> Bookings = new List<Booking>()
        {
            new Booking { BookingDTO = new BookingDto(), Name = "Viet", BookingTime="09:00" },
            new Booking { BookingDTO = new BookingDto(), Name = "Viet", BookingTime="10:15" }
        };

        public Task<List<Booking>> GetBookings()
        {  
            return Task.FromResult(Bookings); 
        }

        public Task<bool> BookingExists (BookingDto BookingDTO)
        {
            return Task.FromResult(Bookings.Any(x => x.BookingDTO == BookingDTO));
        }

        public bool BookingExistsByTime(string BookingTime)
        {
           foreach (var booking in Bookings)
            {
                if (TimeSpan.TryParse(booking.BookingTime, out var fromTime))
                {
                    if (TimeSpan.TryParse(BookingTime, out var timeCompare))
                    {
                        var toTime = fromTime + TimeSpan.FromMinutes(59);
                        if (fromTime <= timeCompare && timeCompare <= toTime)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        throw new ValidationException($"Invalid time {timeCompare}");
                    }
                }
                else
                {
                    throw new ValidationException($"Invalid time {booking.BookingTime}");
                }
            }
            return false;
        }

        public Task<Booking?> GetBookingByTime(string BookingTime)
        {
            return Task.FromResult(Bookings.FirstOrDefault(x => x.BookingTime == BookingTime));
        }

        public Task AddBooking(Booking Booking)
        {
            Bookings.Add(Booking);
            return Task.CompletedTask;
        }
    }
}
