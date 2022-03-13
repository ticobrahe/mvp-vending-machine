﻿using Common.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Persistance.DbContext;
using System.Text;


namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(opts =>
            {
                opts.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }


        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var jwtUserSecret = jwtSettings.GetSection("Secret").Value;

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("ValidIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("ValidAudience").Value,
                    IssuerSigningKey = new
                        SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtUserSecret))
                };
            });
        }

        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc()
                .ConfigureApiBehaviorOptions(o =>
                {
                    o.InvalidModelStateResponseFactory = context => new ValidationFailedResult(context.ModelState);
                });//.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserValidator>());
        }


        
        //public static void ConfigureSwagger(this IServiceCollection services)
        //{
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.OperationFilter<RemoveVersionFromParameter>();
        //        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //        {
        //            Name = "Authorization",
        //            Type = SecuritySchemeType.ApiKey,
        //            Scheme = "Bearer",
        //            BearerFormat = "JWT",
        //            In = ParameterLocation.Header,
        //            Description = "JWT Authorization header using the Bearer scheme."
        //        });

        //        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        //        {
        //            {
        //                  new OpenApiSecurityScheme
        //                    {
        //                        Reference = new OpenApiReference
        //                        {
        //                            Type = ReferenceType.SecurityScheme,
        //                            Id = "Bearer"
        //                        }
        //                    },
        //                    new string[] {}
        //            }
        //        });
        //    });
        //}
        
    }
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }

    public class ValidationResultModel : ErrorResponse<List<ValidationError>>
    {
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Message = "Validation Failed";
            Error = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
        }
    }

    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
