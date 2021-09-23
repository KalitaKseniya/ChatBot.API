using ChatBot.Core.Interfaces;
using ChatBot.Infrastructure;
using ChatBot.Infrastructure.Models;
using ChatBot.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using ChatBot.Core.Models;

namespace ChatBot.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureLoggerService(this IServiceCollection services)
            => services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 4;
                o.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuraton)
        {
            var jwtSettings = configuraton.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey + secretKey))
                };
            });

        }
        public static void ConfigureAuthManager(this IServiceCollection services) =>
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ChatBot API",
                    Version = "v1",
                }) ;
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add jwt with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
            });
        }
    
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureAuth(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //to automate?
                //var permissions = SerializeStatic.SerializeSuperClass(typeof(Permissions));
                //var policies = SerializeStatic.SerializeSuperClass(typeof(PolicyTypes));

                //users
                options.AddPolicy(PolicyTypes.Users.View, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Users.View); });
                options.AddPolicy(PolicyTypes.Users.AddRemove, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Users.AddRemove); });
                options.AddPolicy(PolicyTypes.Users.Edit, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Users.Edit); });
                options.AddPolicy(PolicyTypes.Users.EditRoles, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Users.EditRoles); });
                options.AddPolicy(PolicyTypes.Users.ViewRoles, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Users.ViewRoles); });
                options.AddPolicy(PolicyTypes.Users.ChangePassword, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Users.ChangePassword); });

                //chats
                options.AddPolicy(PolicyTypes.Chats.AddRemove, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Chats.AddRemove); });
                options.AddPolicy(PolicyTypes.Chats.Edit, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Chats.Edit); });
                options.AddPolicy(PolicyTypes.Chats.ViewById, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Chats.ViewById); });
                
                //claims
                options.AddPolicy(PolicyTypes.Claims.View, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Claims.View); });

                //roles
                options.AddPolicy(PolicyTypes.Roles.View, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Roles.View); });
                options.AddPolicy(PolicyTypes.Roles.AddRemove, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Roles.AddRemove); });
                options.AddPolicy(PolicyTypes.Roles.ViewClaims, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Roles.ViewClaims); });
                options.AddPolicy(PolicyTypes.Roles.EditClaims, policy => { policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Roles.EditClaims); });

            });
        }
    }
}

