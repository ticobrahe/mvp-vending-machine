using Application.Command.User;
using Common.General;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Command.User
{
    using static Testing;
    [TestFixture]
    public class UserBuyCommandTest: TestBase
    {

        [Test]
        public async Task Should_Throw_EXception_When_Amount_GreaterThan_Than_AmountAvailable()
        {
            await CreateUsers(true);

            var users = await GetAllAsync<Domain.Entities.User>();
            var user = users.FirstOrDefault(x => x.Role == ERole.Buyer.ToString());
            var products = await GetAllAsync<Domain.Entities.Product>();
            var product = products.FirstOrDefault();
            var command = new UserBuyCommand
            {
                UserId = user.Id,
                Amount = 5,
                ProductId = product.Id,
                
            };

            Assert.ThrowsAsync<RestException>(async () => await SendAsync(command));
        }

        [Test]
        public async Task Should_Buy_Product()
        {
            await CreateUsers(true);

            var users = await GetAllAsync<Domain.Entities.User>();
            var user = users.FirstOrDefault(x => x.Role == ERole.Buyer.ToString());
            var products = await GetAllAsync<Domain.Entities.Product>();
            var product = products.FirstOrDefault();
            var command = new UserBuyCommand
            {
                UserId = user.Id,
                Amount = 1,
                ProductId = product.Id,
            };

            var result = await SendAsync(command);

            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<UserBuyCommandResponse>();
        }
    }
}
