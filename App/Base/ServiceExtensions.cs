using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TimeTracking.App.Base
{
	public static class ServiceExtensions
	{
		public static void ConfigureSqlContext(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<TimeTrackingContext>(options =>
				options.UseSqlServer(connectionString));
		}

		public static void ConfigureJWT(this IServiceCollection services, string secretKey)
		{
			var key = Encoding.ASCII.GetBytes(secretKey);
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});
		}

		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Description = "Please enter JWT with Bearer into field",
					Name = "Authorization",
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
				});
				options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
				{
					{
						new Microsoft.OpenApi.Models.OpenApiSecurityScheme
						{
							Reference = new Microsoft.OpenApi.Models.OpenApiReference
							{
								Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] { }
					}
				});
			});
		}
	}
}
