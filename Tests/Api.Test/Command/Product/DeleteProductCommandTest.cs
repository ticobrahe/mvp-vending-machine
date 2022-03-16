using Application.Command.Product;
using Domain.Enums;
using FluentAssertions;
using MediatR;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Command.Product
{
    using static Testing;
    [TestFixture]
    public class DeleteProductCommandTest: TestBase
    {
        [Test]
        public async Task Should_Update_Product()
        {
            await CreateUsers(true);
            var users = await GetAllAsync<Domain.Entities.User>();
            var user = users.FirstOrDefault(x => x.Role == ERole.Seller.ToString());
            var products = await GetAllAsync<Domain.Entities.Product>();
            var product = products.FirstOrDefault();
            var command = new DeleteProductCommand
            {
                UserId = user.Id,
                ProductId = product.Id
            };

            var result = await SendAsync(command);

            result.Should().Be(Unit.Value);
        }
    }
}
