using NUnit.Framework;
using System.Threading.Tasks;

namespace Api.Test
{
    using static Testing;
    public class TestBase
    {
        [SetUp]
        public async Task SetUp()
        {
            await ResetDbState();
        }
    }
}
