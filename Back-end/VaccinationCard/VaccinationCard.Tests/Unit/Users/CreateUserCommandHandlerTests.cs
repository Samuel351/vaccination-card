
using Moq;
using VaccinationCard.Application.Users.Commands.CreateUser;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Interfaces.Repositories;
using Xunit;

namespace VaccinationCard.Tests.Unit.Users
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IEncryptionService> _encryptionServiceMock;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _encryptionServiceMock = new Mock<IEncryptionService>();

            _handler = new CreateUserCommandHandler(
                _userRepositoryMock.Object,
                _encryptionServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailAlreadyExists()
        {
            // Arrange
            var command = new CreateUserCommand("test@email.com", "password123");

            _userRepositoryMock
                .Setup(r => r.EmailExists(command.Email, CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserErrors.EmailAlreadyRegistred, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldCreateUser_WhenEmailDoesNotExist()
        {
            // Arrange
            var command = new CreateUserCommand("new@email.com", "password123");

            _userRepositoryMock
                .Setup(r => r.EmailExists(command.Email, CancellationToken.None))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
