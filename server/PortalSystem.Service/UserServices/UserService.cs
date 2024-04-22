using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PortalSystem.Models;
using PortalSystem.Repository.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PortalSystem.Service.UserServices
{
    public class UserService : IUserService
    {
        private readonly IOptions<JwtConfiguration> _jwtConfiguration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        public UserService(IOptions<JwtConfiguration> jwtConfiguration, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtConfiguration = jwtConfiguration;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }
        //public static User user = new User();
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, user.Email), .. user.RoleList.Select(role => new Claim(ClaimTypes.Role, role))
            ];

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                GetBytes(_jwtConfiguration.Value.Token));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        public async Task<User> CreateUser(CreateUserRequest request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var User = new User()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleList = ["user"]
            };
            var existing = await _userRepository.GetByEmail(request.Email);
            if (existing != null)
            {
                throw new Exception("User Already Exist.");
            }
            var userInsertion = await _userRepository.Add(User);
            if (userInsertion) { return User; }
            throw new Exception("User Cannot saved.");
        }
        public async Task<LoginUserResponse> LoginUser(LoginUserRequest request)
        {
            var existing = await _userRepository.GetByEmail(request.Email);
            if (existing == null)
            {
                throw new Exception("User not registerd.");
            }
            if (!VerifyPasswordHash(request.Password, existing.PasswordHash, existing.PasswordSalt))
            {
                throw new Exception("Password incorrect.");
            }
            string token = CreateToken(existing);
            var refreshToken = GenerateRefreshToken();
            refreshToken.UserId = existing.Id;
            //await _refreshTokenRepository.Add(refreshToken);
            return new LoginUserResponse(token, refreshToken);
        }

        public async Task<LoginUserResponse> RefreshToken(string refreshToken)
        {
            var existingToken = await _refreshTokenRepository.GetRefreshTokenByIdAsync(refreshToken);
            if(existingToken != null)
            {
                var existingUser = await _userRepository.GetById(existingToken.UserId);
                if (existingUser != null)
                {
                    string token = CreateToken(existingUser);
                    var newRefreshToken = GenerateRefreshToken();
                    newRefreshToken.UserId = existingUser.Id;
                    //await _refreshTokenRepository.DeleteRefreshTokenByToken(existingToken.Token);
                    //await _refreshTokenRepository.Add(newRefreshToken);
                    return new LoginUserResponse(token, newRefreshToken);
                }
            }
            
            throw new Exception("Access is denied.");
            
        }

        public async Task RemoveRefreshToken(string refreshToken)
        {
            var existingToken = await _refreshTokenRepository.GetRefreshTokenByIdAsync(refreshToken);
            if(existingToken != null)
            {
                //await _refreshTokenRepository.DeleteRefreshTokenByToken(existingToken.Token);
            }
            
        }
    }
}
