
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PS.Data.Contexts;
using System.Reflection;
using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PS.ApplicationServices.Implementations;
using PS.ApplicationServices.Interfaces;
using PS.Repositories.Implementations;
using PS.Repositories.Interfaces;
using PS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
namespace PS.WebApiService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                                .Build();

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

            Log.Logger.Information("Application is starting!");


            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
            var connectionStringUsers = builder.Configuration.GetConnectionString("UsersConnectionString");
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? "wrong name";

            builder.Services.AddDbContext<PlatformDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly(assemblyName)));
            builder.Services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(connectionStringUsers, b => b.MigrationsAssembly(assemblyName)));


            // Add services to the container.

           
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{assemblyName}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            builder.Services.AddControllers();
            // Add serilog
            builder.Services.AddSerilog();


            builder.Services.AddTransient<UserManager<ApplicationUser>>();
            //For Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<UsersDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.AddAuthorization();

            // Authentication
            string tokenKey = configuration["Authentication:TokenKey"] ?? "Not working token key";
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;                
                x.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey)),
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                };
            });

            // Start SERVICE DI
            builder.Services.AddScoped<DbContext, PlatformDbContext>();
            builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();           
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IPlatformManagementService, PlatformsManagementService>();            
            builder.Services.AddScoped<IOperatingSystemManagementService, OperatingSystemManagementService>();
            builder.Services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(tokenKey));



            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddHealthChecks();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

           // app.MapHealthChecks("/healthz");
            app.MapHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.MapHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
            app.Run();
        }
    }
}
