using MediatR;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository userRepository, IEncryptionService encryptionService) : IRequestHandler<CreateUserCommand, Result>
    {

        private readonly IUserRepository _userRepository = userRepository;

        private readonly IEncryptionService _encryptionService = encryptionService;

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if(await _userRepository.EmailExists(request.Email))
            {
                return Result.Failure(UserErrors.EmailAlreadyRegistred);
            }

            var user = new User(request.Email, request.Password);

            user.UpdatePassword(_encryptionService.EncryptPassword(user));

            await _userRepository.AddAsync(user);

            return Result.Success();    
        }
    }
}
