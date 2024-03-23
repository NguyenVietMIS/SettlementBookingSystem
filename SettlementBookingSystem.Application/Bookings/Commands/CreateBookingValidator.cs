using FluentValidation;
using SettlementBookingSystem.Application.Bookings.Repositories;
using SettlementBookingSystem.Application.Exceptions;
using System;
using System.ComponentModel.Design;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SettlementBookingSystem.Application.Bookings.Commands
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
    {
        private readonly ICreateBookingRepository _createBookingRepository;
        public CreateBookingValidator(ICreateBookingRepository bookingRepository)
        {
            _createBookingRepository = bookingRepository;
            RuleFor(b => b.Name).NotEmpty();
            RuleFor(b => b.BookingTime)
                .Matches("[0-9]{1,2}:[0-9][0-9]").WithMessage("Booking Time Not Correct Format")
                .Must(BookingTime =>
                {
                    if (TimeSpan.TryParse(BookingTime,out TimeSpan timeCheck))
                    {
                        if (TimeSpan.Parse("09:00") <= timeCheck && timeCheck <= TimeSpan.Parse("16:00"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else 
                    {
                        return false;
                    }
                }
            ).WithMessage("Booking Time must between 09:00 to 16:00")
            .Must(BookingTimeMustNotExist).WithMessage("Booking Time Exist");           
        }
        private bool BookingTimeMustNotExist(string BookingTime)
        {
            var result = _createBookingRepository.BookingExistsByTime(BookingTime);
            if (result == true)
            {
                throw new ConflictException("Booking Time Exist");
            }
            return !result;
        }
    }
}
