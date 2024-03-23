using FluentValidation;
using MediatR;
using SettlementBookingSystem.Application.Bookings.Dtos;
using SettlementBookingSystem.Application.Bookings.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SettlementBookingSystem.Application.Bookings.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        private readonly ICreateBookingRepository _bookingRepository;
        
        public CreateBookingCommandHandler(ICreateBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // TODO Implement CreateBookingCommandHandler.Handle() and confirm tests are passing. See InfoTrack Global Team - Tech Test.pdf for business rules.
            var validator = new CreateBookingValidator(_bookingRepository);
            validator.ValidateAndThrow(request);

            var booking = new Booking { BookingDTO = new BookingDto(), Name = request.Name, BookingTime = request.BookingTime };
            await _bookingRepository.AddBooking(booking);
            return booking.BookingDTO;
        }
    }
}
