using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Repos.Users;

namespace VehicleTrackingSystem.Bal.TokenProvider
{
    public class JwtMiddleware
    {
        private readonly AppSettings appSettings;
        private readonly IUsersRepository usersRepository;
        private readonly RequestDelegate next;


        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, IUsersRepository usersRepository)
        {
            this.next = next;
            this.appSettings = appSettings.Value;
            this.usersRepository = usersRepository;

        }
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, token);

            await this.next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id" || x.Type == "Name").Value);

                context.Items["User"] = this.usersRepository.GetUserById(userId);
            }
            catch
            {
                
            }
        }
    }
}
