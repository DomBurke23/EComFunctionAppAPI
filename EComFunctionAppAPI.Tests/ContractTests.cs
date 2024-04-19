using Microsoft.Extensions.Hosting;

namespace EComFunctionAppAPI.Tests
{
    public class ContractTests : IClassFixture<HostFixture>
    {
        private readonly IHost _host;

        public ContractTests(HostFixture testServerFixture)
        {
            _host = testServerFixture.Host;
        }

        [Fact]
        public void Test1()
        {

        }
    }
}