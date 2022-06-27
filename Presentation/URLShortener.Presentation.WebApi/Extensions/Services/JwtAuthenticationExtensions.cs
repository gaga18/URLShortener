using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Workabroad.Presentation.Admin.Extensions.Services
{
    public static class JwtAuthenticationExtensions
    {
        /// <summary>
        /// ავთენთიფიკაციის პარამეტრების დამატება
        /// </summary>
        public static void AddJwtAuthenticationConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateActor = false,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero, // ანულებს ტოკენის სიცოცხლის ხანგრძლივობას. დეფოლტად არის 5 წუთი
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidAudience = configuration["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]))
                    };
                });
        }

        /// <summary>
        /// ავტორიზაციის პარამეტრების დამატება
        /// </summary>
        public static void AddJwtAuthorizationConfigs(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                // მისი გამოყენება მოხდება [Authorize(Policy = "EditUsersPolicy")] ატრიბუტით
                options.AddPolicy("EditPolicy", policy =>
                {
                    policy.RequireAssertion(con => con.User.HasClaim(x => x.Type == "resources" && x.Value == "user.edit"));
                });
                options.AddPolicy("DeletePolicy", policy =>
                {
                    policy.RequireAssertion(con => con.User.HasClaim(x => x.Type == "resources" && x.Value == "user.delete"));
                });
                options.AddPolicy("ViewPolicy", policy =>
                {
                    policy.RequireAssertion(con => con.User.HasClaim(x => x.Type == "resources" && x.Value == "user.view"));
                });
            });
        }
    }
}
