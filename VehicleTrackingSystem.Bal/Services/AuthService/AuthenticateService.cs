using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleTrackingSystem.Bal.TokenProvider;
using VehicleTrackingSystem.Core.Constants;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Entities;
using VehicleTrackingSystem.Dal.Repos.RolesRepo;
using VehicleTrackingSystem.Dal.Repos.Users;

namespace VehicleTrackingSystem.Bal.Services.AuthService
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IRolesRepository rolesRepository;
        private readonly AppSettings appSettings;

        public AuthenticateService(IUsersRepository usersRepository, IRolesRepository rolesRepository, IOptions<AppSettings> appSettings)
        {
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
            this.appSettings = appSettings.Value;

        }
    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await this.usersRepository.GetUserByEmailAndPassword(model);
            if (user == null)
                throw new Exception(message: Messages.UserNotFound);

            var role = this.rolesRepository.GetRoleById(user.UsersRolesMappers.Select(x => x.RoleId).FirstOrDefault());
            if (role == null)
                throw new Exception(message: Messages.UserNotFound);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user?.EmailId),
                    new Claim(ClaimTypes.Role, role?.Result?.RoleName)
                }),
                Expires = DateTime.Now.AddMinutes(this.appSettings.BearerTokenValidityInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return MapUser(user, tokenHandler.WriteToken(token), tokenDescriptor.Expires, role?.Result?.RoleName);
        }


        private AuthenticateResponse MapUser(User user, string token, DateTime? expiryTime, string role)
        {

            return new AuthenticateResponse
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.EmailId,
                Token = token,
                TokenExpiryAt = expiryTime,
                Role = role
            };
        }
    }
}
