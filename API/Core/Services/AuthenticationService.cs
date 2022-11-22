using API.Core.Interfaces;
using API.Models.Entities;
using API.Models.Requests;
using API.Models.Responses;
using AutoMapper;

namespace API.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository authenticationRepository;
        private readonly IPasswordHasherService passwordHasher;
        private readonly IMapper mapper;
        private readonly JwtService jwt;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public AuthenticationService(
            IAuthenticationRepository authenticationRepository,
            IPasswordHasherService passwordHasher,
            IMapper mapper,
            JwtService jwt,
            IRefreshTokenRepository refreshTokenRepository)
        {
            this.authenticationRepository = authenticationRepository;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;
            this.jwt = jwt;
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticatedResponse> Login(LoginRequest login)
        {
            // Validadr Modelo !ModelState.IsValid

            User user = await authenticationRepository.GetByUserCode(login.username);
            if (user == null)
            {
                throw new InvalidOperationException("El usuario no se encuentra registrado");
            }

            bool isCorrectPassword = passwordHasher.VerifyPassword(login.Password, user.password);
            if (!isCorrectPassword)
            {
                throw new InvalidOperationException("Lo contraseña no es correcta");
            }

            LoginResponse loginResponse = mapper.Map<LoginResponse>(user);

            return await JWTAuthenticate(loginResponse);
        }
        public async Task<AuthenticatedResponse> Refresh(RefreshRequest refreshRequest)
        {
            bool isValidRefreshToken = jwt.RefreshTokenValidate(refreshRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                throw new InvalidOperationException("Invalid refresh token.");
            }

            RefreshToken refreshTokenDto = await refreshTokenRepository.GetByToken(refreshRequest.RefreshToken);
            if (refreshTokenDto == null)
            {
                throw new InvalidOperationException("Invalid refresh token.");
            }

            await refreshTokenRepository.Delete(refreshTokenDto.username);

            LoginResponse loginResponse = await GetByUserCode(refreshTokenDto.username);

            return await JWTAuthenticate(loginResponse);
        }
        public async Task Logout(string username)
        {
            await refreshTokenRepository.Delete(username);
        }
        public async Task Register(UserRequest userRequest)
        {
            userRequest.Password = passwordHasher.SHA1(userRequest.Password);
            int register = await authenticationRepository.Register(userRequest);
            if (register != 1)
            {
                throw new InvalidOperationException("Invalid register.");
            }
        }

        private async Task<AuthenticatedResponse> JWTAuthenticate(LoginResponse user)
        {
            string accessToken = jwt.GenerateAccessToken(user);
            string refreshToken = jwt.GenerateRefreshToken();

            RefreshToken refreshTokenDto = new RefreshToken()
            {
                Token = refreshToken,
                first_name = user.first_name,
                last_name = user.last_name,
                username = user.username
            };

            await refreshTokenRepository.Create(refreshTokenDto);

            return new AuthenticatedResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        private async Task<LoginResponse> GetByUserCode(string username)
        {
            User user = await authenticationRepository.GetByUserCode(username);
            if (user == null)
            {
                throw new InvalidOperationException("El usuario no se encuentra registrado");
            }
            return mapper.Map<LoginResponse>(user);
        }

    }
}
