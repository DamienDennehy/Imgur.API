using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Endpoints;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace Imgur.API.Tests.Integration
{
    public class ImageEndpointIntegrationTests : IAssemblyFixture<ApiClientAssemblyFixture>
    {
        private readonly ApiClientAssemblyFixture _fixture;

        public ImageEndpointIntegrationTests(ApiClientAssemblyFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetImageWithClientKey_GetsImage()
        {
            var apiClient = _fixture.GetApiClientWithKey();
            var httpClient = new HttpClient();

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var image = await imageEndpoint.GetImageAsync("PdvlRWc");

            Assert.NotNull(image);
            Assert.Equal("PdvlRWc", image.Id);
        }

        [Fact]
        public async Task GetImageWithClientKeyAndSecret_GetsImage()
        {
            var apiClient = _fixture.GetApiClientWithKeyAndSecret();
            var httpClient = new HttpClient();

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var image = await imageEndpoint.GetImageAsync("PdvlRWc");

            Assert.NotNull(image);
            Assert.Equal("PdvlRWc", image.Id);
        }

        [Fact]
        public async Task GetApiClientWithKeyAndOAuthToken_GetsImage()
        {
            var apiClient = _fixture.GetApiClientWithKeyAndOAuthToken();
            var httpClient = new HttpClient();

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var image = await imageEndpoint.GetImageAsync("PdvlRWc");

            Assert.NotNull(image);
            Assert.Equal("PdvlRWc", image.Id);
        }
    }
}
