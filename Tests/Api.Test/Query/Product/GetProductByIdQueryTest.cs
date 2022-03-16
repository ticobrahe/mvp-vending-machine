using Application.Query.Product;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Query.Product
{
    using static Testing;
    [TestFixture]
    public class GetProductByIdQueryTest:TestBase
    {
        [Test]
        public async Task Should_Return_Product()
        {
            await CreateUsers(true);
            var users = await GetAllAsync<Domain.Entities.User>();
            var user = users.FirstOrDefault(x => x.Role == ERole.Seller.ToString());
            var products = await GetAllAsync<Domain.Entities.Product>();
            var product = products.FirstOrDefault();
            var command = new GetProductByIdQuery
            {
                ProductId = product.Id
            };

            var result = await SendAsync(command);

            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<GetProductByIdQueryResponse>();
        }
    }
}
