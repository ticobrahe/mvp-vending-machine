using Application.Command.Product;
using Application.Validators.ProductValidator;
using Domain.Enums;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Test.Command.Product
{
    using static Testing;
    [TestFixture]
    public class AddProductCommandTest:TestBase
    {
        [Test]
        public async Task Shoud_Add_Product()
        {
            await CreateUsers(false);
            var users = await GetAllAsync<Domain.Entities.User>();
            var user = users.FirstOrDefault(x => x.Role == ERole.Seller.ToString());
            var command = new AddProductCommand
            {
               SellerId = user.Id,
               ProductName = "PlayStation",
               AmountAvailable = 2,
               Cost = 50
            };
            var result = await SendAsync(command);

            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<AddProductCommandResponse>();
            result.Data.Cost.Should().Be(50);
        }

        [Test]
        public async Task Shoud_Not__Add_Product_With_Invalid_Cost()
        {
            await CreateUsers(false);
            var users = await GetAllAsync<Domain.Entities.User>();
            var user = users.FirstOrDefault(x => x.Role == ERole.Seller.ToString());
            var command = new AddProductCommand
            {
                SellerId = user.Id,
                ProductName = "PlayStation",
                AmountAvailable = 2,
                Cost = 52
            };
            var userDepositCommandValidator = new AddProductCommandValidator();
            var result = userDepositCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Cost);
        }
    }
}
