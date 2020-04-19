using System;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PrintCenter.Auth;
using PrintCenter.Auth.Accounts;
using PrintCenter.Data;
using PrintCenter.Infrastructure.Security;

namespace PrintCenter.Tests.Helpers
{
    public class AuthDependenciesFixture
    {
        public IServiceProvider ServiceProvider { get; }

        public AuthDependenciesFixture()
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
            services.AddValidatorsFromAssemblyContaining(typeof(AccountEnvelope));
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}