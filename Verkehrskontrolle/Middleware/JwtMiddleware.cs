using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context, AuthDbContext dbContext)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_jwtSettings.TokenKey);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    //var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                    var email = jwtToken.Claims.First(x => x.Type == "email").Value;

                    // attach user to context on successful jwt validation
                    context.Items["User"] = await dbContext.Users.FindAsync(email); // userService.GetById(userId);
                }
                catch
                {
                    // do nothing if jwt validation fails
                    // user is not attached to context so request won't have access to secure routes
                }
            }

            await _next(context);
        }
    }
}
