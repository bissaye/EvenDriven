using Bissaye.JwtAuth;
using EvenDriven.App.Common;
using EvenDriven.App.Common.Services;
using EvenDriven.Domain.Entities;
using EvenDriven.Domain.Primitives;
using EvenDriven.Domain.Repositories;
using EvenDriven.Infra.Persistence;
using EvenDriven.Infra.Persistence.Respositories;
using EvenDriven.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;


namespace EvenDriven.Infra
{
    public static class DependencyInjection
    {
        static DependencyInjection()
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            /* ==================================
             *                                  *
             *           Persistence Layer      *
             *                                  *
             * ================================== */
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite(connectionString);
            });
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBaseRepository<UserEntity>, UserRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();


            /* ==================================
             *                                  *
             *           Service Layer          *
             *                                  *
             * ================================== */

            services.AddScoped<IPasswordServices, PasswordServices>();
            services.AddBissayeJwtAuth(configuration);
            services.AddScoped<IJwtServices, JwtServices>();

            return services;
        }
    }
}
