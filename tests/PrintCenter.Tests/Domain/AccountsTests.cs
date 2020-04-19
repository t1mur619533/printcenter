using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PrintCenter.Auth.Accounts;
using PrintCenter.Data;
using PrintCenter.Data.Models;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Tests.Helpers;
using Xunit;

namespace PrintCenter.Tests.Domain
{
    public class AccountsTests : IClassFixture<DependencySetupFixture>
    {
        private readonly IServiceProvider serviceProvider;

        public AccountsTests(DependencySetupFixture dependencySetupFixture)
        {
            serviceProvider = dependencySetupFixture.ServiceProvider;
        }

        [Theory]
        [InlineData("name", "passOld", "passNew")]
        [InlineData("name", "123456", "123456")]
        [InlineData("name", "2$*&<#@$", "@#$%%45g")]
        public async Task Edit_Password_Expect_Success(string name, string passOld, string passNew)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                // Arrange
                var provider = scope.ServiceProvider;
                var passwordHasher = provider.GetRequiredService<IPasswordHasher<User>>();
                var context = provider.GetRequiredService<DataContext>();
                var mediator = provider.GetRequiredService<IMediator>();
                await context.Users.AddAsync(new User()
                {
                    Login = name,
                    PasswordHash = passwordHasher.HashPassword(new User(), passOld)
                });
                await context.SaveChangesAsync();

                var command = new EditPassword.Command()
                {
                    AccountDto = new EditPassword.AccountDto() {OldPassword = passOld, NewPassword = passNew},
                    Login = name
                };

                // Act
                var result = await mediator.Send(command);

                // Asserts
                Assert.Equal(Unit.Value, result);
            }
        }

        [Theory]
        [InlineData("name", "13", "134")]
        [InlineData("name", "13", "1567654")]
        [InlineData("name", "234423", "1554")]
        [InlineData("name", "", "123456")]
        [InlineData("name", "         ", "123456")]
        [InlineData("name", "", "")]
        [InlineData("name", "we4234", "")]
        public async Task Edit_Password_Expect_ValidationException(string name, string passOld, string passNew)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                // Arrange
                var provider = scope.ServiceProvider;
                var passwordHasher = provider.GetRequiredService<IPasswordHasher<User>>();
                var context = provider.GetRequiredService<DataContext>();
                var mediator = provider.GetRequiredService<IMediator>();
                await context.Users.AddAsync(new User()
                {
                    Login = name,
                    PasswordHash = passwordHasher.HashPassword(new User(), passOld)
                });
                await context.SaveChangesAsync();

                var command = new EditPassword.Command()
                {
                    AccountDto = new EditPassword.AccountDto() {OldPassword = passOld, NewPassword = passNew},
                    Login = name
                };

                // Act
                var result = await Assert.ThrowsAnyAsync<Exception>(async () => { await mediator.Send(command); });

                // Asserts
                Assert.IsAssignableFrom<ValidationException>(result);
            }
        }

        [Theory]
        [InlineData("name", "passOld", "passNew")]
        [InlineData("name", "123456", "123456")]
        [InlineData("name", "2$*&<#@$", "@#$%%45g")]
        public async Task Edit_Password_Null_User_Expect_AccessDeniedException(string name, string passOld, string passNew)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                // Arrange
                var provider = scope.ServiceProvider;
                var mediator = provider.GetRequiredService<IMediator>();

                var command = new EditPassword.Command()
                {
                    AccountDto = new EditPassword.AccountDto() {OldPassword = passOld, NewPassword = passNew},
                    Login = name
                };

                // Act
                var result = await Assert.ThrowsAnyAsync<Exception>(async () => { await mediator.Send(command); });

                // Asserts
                Assert.IsAssignableFrom<AccessDeniedException>(result);
            }
        }

        [Theory]
        [InlineData("name", "passOld", "passNew")]
        [InlineData("name", "123456", "123456")]
        [InlineData("name", "2$*&<#@$", "@#$%%45g")]
        public async Task Edit_Password_Invalid_loginPassword_Expect_AccessDeniedException(string name, string passOld, string passNew)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                // Arrange
                var provider = scope.ServiceProvider;
                var passwordHasher = provider.GetRequiredService<IPasswordHasher<User>>();
                var context = provider.GetRequiredService<DataContext>();
                var mediator = provider.GetRequiredService<IMediator>();
                await context.Users.AddAsync(new User()
                {
                    Login = name,
                    PasswordHash = passwordHasher.HashPassword(new User(), Guid.NewGuid().ToString())
                });
                await context.SaveChangesAsync();

                var command = new EditPassword.Command()
                {
                    AccountDto = new EditPassword.AccountDto() { OldPassword = passOld, NewPassword = passNew },
                    Login = name
                };

                // Act
                var result = await Assert.ThrowsAnyAsync<Exception>(async () => { await mediator.Send(command); });

                // Asserts
                Assert.IsAssignableFrom<AccessDeniedException>(result);
            }
        }
    }
}
