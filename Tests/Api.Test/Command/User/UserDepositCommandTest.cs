using Application.Command.User;
using Application.Validators.UserValidator;
using Domain.Enums;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Test.Command.User
{
    using static Testing;
    [TestFixture]
    public class UserDepositCommandTest:TestBase
    {
        [Test]
        public async Task Shoud_Add_Deposit()
        {
            await CreateUsers(false);
            var users = await GetAllAsync<Domain.Entities.User>();
            var user = users.FirstOrDefault(x => x.Role == ERole.Seller.ToString());
            var amount = 20;
            var command = new UserDepositCommand
            {
                UserId = user.Id,
                Deposit = amount
            };
            var result = await SendAsync(command);

            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<UserDepositCommandResponse>();
            result.Data.Deposit.Should().Be(amount);
        }

        [Test]
        public async Task Shoud_Not__Add_Deposit()
        {
            await CreateUsers(false);
            var users = await GetAllAsync<Domain.Entities.User>();
            var user = users.FirstOrDefault(x => x.Role == ERole.Seller.ToString());
            var command = new UserDepositCommand
            {
                UserId = user.Id,
                Deposit = 8
            };
            var userDepositCommandValidator = new UserDepositCommandValidator();
            var result = userDepositCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Deposit);
        }
    }
}
