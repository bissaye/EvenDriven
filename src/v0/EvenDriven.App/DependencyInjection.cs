using EvenDriven.App.Auth.Services;
using EvenDriven.App.Auth.Services.Concretes;
using EvenDriven.App.Booking.Services;
using EvenDriven.App.Booking.Services.Concretes;
using EvenDriven.App.User.Services;
using EvenDriven.App.User.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;


namespace EvenDriven.App
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {

           /* ==================================
            *                                  *
            *           Business Services      *
            *                                  *
            * ================================== */

            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IBookingServices, BookingServices>();
            

            return services;
        }
    }
}
