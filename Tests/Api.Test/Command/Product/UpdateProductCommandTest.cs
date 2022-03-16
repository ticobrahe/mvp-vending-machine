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
    public class UpdateProductCommandTest: TestBase
    {
        [Test]
        public async Task Should_Update_Product()
        {
            await CreateUsers(true);
            var users = await GetAllAsync<Domain.Entities.User>();
            var user = users.FirstOrDefault(x => x.Role == ERole.Seller.ToString());
            var products = await GetAllAsync<Domain.Entities.Product>();
            var product = products.FirstOrDefault();
            var command = new UpdateProductCommand
            {
                UserId = user.Id,
                ProductId = product.Id,
                Cost = 25,
                AmountAvailable = 4,
                ProductName = "Bag"
            };

            var result = await SendAsync(command);

            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<UpdateProductCommandResponse>();
            result.Data.AmountAvailable.Should().Be(4);
        }

        [Test]
        public async Task Shoud_Not__Update_Product_With_Invalid_Cost()
        {
            await CreateUsers(true);
            var users = await GetAllAsync<Domain.Entities.User>();
            var user = users.FirstOrDefault(x => x.Role == ERole.Seller.ToString());
            var products = await GetAllAsync<Domain.Entities.Product>();
            var product = products.FirstOrDefault();
            var command = new UpdateProductCommand
            {
                UserId = user.Id,
                ProductId = product.Id,
                Cost = 27,
                ProductName = "Bag",
                AmountAvailable = 4
            };
            var userDepositCommandValidator = new UpdateProductCommandValidator();
            var result = userDepositCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Cost);
        }
    }
}
