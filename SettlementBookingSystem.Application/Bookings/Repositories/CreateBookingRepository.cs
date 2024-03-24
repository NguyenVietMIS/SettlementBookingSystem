using MediatR;
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

        public List<Booking> GetBookings()
        {  
            return Bookings; 
        }

        public bool BookingExists (BookingDto BookingDTO)
        {
            return Bookings.Any(x => x.BookingDTO == BookingDTO);
        }

        public bool BookingExistsByTime(string BookingTime)
        {
            if (TimeSpan.TryParse(BookingTime, out var timeCompare))
            {
                var timeCompareTo = timeCompare + TimeSpan.FromMinutes(59);
                foreach (var booking in Bookings)
                {
                    if (TimeSpan.TryParse(booking.BookingTime, out var fromTime))
                    {
                            var toTime = fromTime + TimeSpan.FromMinutes(59);
                            if ( (fromTime <= timeCompare && timeCompare <= toTime) || (fromTime <= timeCompareTo && timeCompareTo <= toTime))
                            {
                                return true;
                            }                    
                    }
                    else
                    {
                        throw new ValidationException($"Invalid time {booking.BookingTime}");
                    }
                }
            }
            else
            {
                throw new ValidationException($"Invalid time {timeCompare}");
            }
            return false;
        }

        public Booking? GetBookingByTime(string BookingTime)
        {
            return Bookings.FirstOrDefault(x => x.BookingTime == BookingTime);
        }

        public BookingDto AddBooking(string strName, string strBookingTime)
        {
            var booking = new Booking { BookingDTO = new BookingDto(), Name = strName, BookingTime = strBookingTime };
            Bookings.Add(booking);
            return booking.BookingDTO;
        }
    }
}
