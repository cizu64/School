using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School.API.Helpers;
using School.Domain.Interfaces;
using School.Domain.Services;
using School.Infrastructure;
using School.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string allowedOrigins = "origins";

builder.Services.AddCors(option =>
{
    option.AddPolicy(allowedOrigins, p => p.WithOrigins("https://localhost:44318", "http://localhost:13369").AllowAnyHeader().AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var Secret = builder.Configuration["jwt:secret"];
var key = Encoding.ASCII.GetBytes(Secret);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["jwt:issuer"],
        ValidAudience = builder.Configuration["jwt:audience"]
    };
});

builder.Services.AddDbContext<SchoolContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionString"], options =>
    {
        //options.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);

        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
        options.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IRestClient, RestClient>();


var app = builder.Build();

//seed database
await SeedAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(allowedOrigins);

app.MapControllers();

app.Run();


async Task SeedAsync(WebApplication host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var schoolContext = services.GetRequiredService<SchoolContext>();
            await SchoolContextSeed.SeedAsync(schoolContext, loggerFactory);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}