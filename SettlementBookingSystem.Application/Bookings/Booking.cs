using SettlementBookingSystem.Application.Bookings.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementBookingSystem.Application.Bookings
{
    public class Booking
    {
        public BookingDto BookingDTO { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BookingTime { get; set; }
    }
}
