using Application.Abstractions;
using Application.Services;
using Domain.Entities;
using Domain.Repository;
using FluentValidation;
using JWTAuth.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Repositories;
using Scrutor;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JWTAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddScoped<IMemberRepository, MemberRepository>();

            builder.Services.AddScoped<IPasswordService, PasswordService>();
            //can add redis here

            builder.Services.AddScoped<IPasswordHasher<Member>, PasswordHasher<Member>>();


            builder
                .Services
                .Scan(
                    selector => selector
                        .FromAssemblies(
                            Infrastructure.AssemblyReference.Assembly,
                            Persistence.AssemblyReference.Assembly)
                        .AddClasses(false)
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

            builder.Services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly,includeInternalTypes: true);



            builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



            builder.Services.AddControllers()
                .AddApplicationPart(Presentation.AssemblyReference.Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //can do like this 
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(o => o.TokenValidationParameters = new()
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer =""
            //    });
            //Alternatively
          

            builder.Services.ConfigureOptions<JwtOptionsSetup>();
            //sb injects and IOptions<JwtOptions> triggers configure method in jwtOptionsSetup  and configures the options instance and populate it with proper values

            builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer();

            builder.Services.AddHttpContextAccessor();

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

            app.Run();
        }
    }
}
