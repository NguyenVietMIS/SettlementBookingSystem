using FluentAssertions;
using FluentValidation;
using Moq;
using SettlementBookingSystem.Application.Bookings;
using SettlementBookingSystem.Application.Bookings.Commands;
using SettlementBookingSystem.Application.Bookings.Dtos;
using SettlementBookingSystem.Application.Bookings.Repositories;
using SettlementBookingSystem.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SettlementBookingSystem.Application.UnitTests
{
    public class CreateBookingCommandHandlerTests
    {
        private readonly Mock<CreateBookingRepository> _repositoryMock;
        public CreateBookingCommandHandlerTests()
        {
            _repositoryMock = new Mock<CreateBookingRepository>();          
        }
        
        [Fact]
       public async Task GivenValidBookingTime_WhenNoConflictingBookings_ThenBookingIsAccepted()
        {
            var command = new CreateBookingCommand
            {
                Name = "test",
                BookingTime = "11:16",
            };

            var handler = new CreateBookingCommandHandler(_repositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.BookingId.Should().NotBeEmpty();
        }

        [Fact]
        public void GivenOutOfHoursBookingTime_WhenBooking_ThenValidationFails()
        {
            var command = new CreateBookingCommand
            {
                Name = "test",
                BookingTime = "00:00",
            };

            var handler = new CreateBookingCommandHandler(_repositoryMock.Object);

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            //var validator = new CreateBookingValidator(_repositoryMock.Object);
            //Action act = () => validator.ValidateAndThrow(command);

            //Assert.Throws<FluentValidation.ValidationException>(act);
            act.Should().Throw<FluentValidation.ValidationException>();
        }

        [Fact]
        public void GivenValidBookingTime_WhenBookingIsFull_ThenConflictThrown()
        {
            var command = new CreateBookingCommand
            {
                Name = "test",
                BookingTime = "09:15",
            };

            var handler = new CreateBookingCommandHandler(_repositoryMock.Object);

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);
            //var validator = new CreateBookingValidator(_repositoryMock.Object);
            //Action act = () => validator.ValidateAndThrow(command);

            //Assert.Throws<ConflictException>(act);
            act.Should().Throw<ConflictException>();
        }
    }
}
