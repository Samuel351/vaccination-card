using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Authentication.Commands.Login
{
    internal class LoginCommandHandler(IUserRepository userRepository, IEncryptionService encryptionService, ITokenService tokenService) : IRequestHandler<LoginCommand, Result<TokenResponse>>
    {

        private readonly IUserRepository _userRepository = userRepository;

        private readonly ITokenService _tokenService = tokenService;

        private readonly IEncryptionService _encryptionService = encryptionService;

        public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email, cancellationToken);

            if(user == null)
            {
                return Result<TokenResponse>.Failure(AuthenticationErrors.InvalidCredentials);
            }

            if(!_encryptionService.VerifyPassword(user, request.Password))
            {
                return Result<TokenResponse>.Failure(AuthenticationErrors.InvalidCredentials);
            }

            var token = _tokenService.GenerateTokenForUser(user);

            return Result<TokenResponse>.Success(new TokenResponse(token));
        }
    }
}
