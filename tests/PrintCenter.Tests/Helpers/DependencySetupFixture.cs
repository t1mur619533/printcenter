using System;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PrintCenter.Api;
using PrintCenter.Data;
using PrintCenter.Domain.Users;
using PrintCenter.Infrastructure.Security;

namespace PrintCenter.Tests.Helpers
{
    public class DependencySetupFixture
    {
        public IServiceProvider ServiceProvider { get; }

        public DependencySetupFixture()
        {
            var services = new ServiceCollection();
            //fake db
            services.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DataContext>());
            services.AddScoped<ITransaction>(provider => provider.GetRequiredService<DataContext>());
            //fake configs
            services.AddScoped(provider =>
                {
                    var config = Substitute.For<IConfiguration>();
                    config["Secret"].Returns("testtesttesttesttesttesttesttesttesttesttesttestte");
                    return config;
                }
            );
            //services
            services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddMediatR();
            services.AddAutoMapper();
            services.AddValidatorsFromAssemblyContaining(typeof(Edit.Command));
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}