using Moq;
using VaccinationCard.Application.Users.Commands.CreateUser;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Interfaces.Repositories;
using Xunit;

namespace VaccinationCard.Tests.User
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
                .Setup(r => r.EmailExists(command.Email))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserErrors.EmailAlreadyRegistred, result.Error);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldCreateUser_WhenEmailDoesNotExist()
        {
            // Arrange
            var command = new CreateUserCommand("new@email.com", "password123");

            _userRepositoryMock
                .Setup(r => r.EmailExists(command.Email))
                .ReturnsAsync(false);

            _encryptionServiceMock
                .Setup(e => e.EncryptPassword(It.IsAny<User>()))
                .Returns("encryptedPassword");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _encryptionServiceMock.Verify(e => e.EncryptPassword(It.Is<User>(u => u.Email == command.Email)), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.Is<User>(u => u.Email == command.Email && u.Password == "encryptedPassword")), Times.Once);
        }
    }
}
