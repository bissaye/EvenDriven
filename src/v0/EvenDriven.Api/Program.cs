using EvenDriven.Api.EndPoints;
using EvenDriven.Api.Middlewares;
using EvenDriven.Api.Services;
using EvenDriven.Api.Services.Concretes;
using EvenDriven.App;
using EvenDriven.Infra;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddHttpLogging(options => {
    options.LoggingFields = HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestPath | HttpLoggingFields.ResponseStatusCode | HttpLoggingFields.Duration;
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen(options =>
{
    // 1. Define the security scheme
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Enter your valid JWT token."
    });

    // 2. Add the security requirement using OpenApiSecuritySchemeReference
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddBusinessLayer();
builder.Services.AddScoped<UserContext>();
builder.Services.AddScoped<IUserContext>(sp => sp.GetRequiredService<UserContext>());
builder.Services.AddScoped<IUserContextAccessor>(sp => sp.GetRequiredService<UserContext>());

var app = builder.Build();

app.UseExceptionHandler();
app.UseHttpsRedirection();



app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseMiddleware<CurrentUserMiddleware>();
app.UseAuthorization();



app.MapAuthEndpoints();
app.MapUserEndpoints();
app.MapBookingEndpoints();



app.Run();
