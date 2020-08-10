using System.IO;
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
        public async Task UploadImageStreamWithClientKey_UploadsImageStream()
        {
            var apiClient = _fixture.GetApiClientWithKey();
            var httpClient = new HttpClient();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "banana.jpg");
            using var fileStream = File.OpenRead(filePath);

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream);
            var imageDownload = await imageEndpoint.GetImageAsync(imageUpload.Id);

            Assert.NotNull(imageUpload);
            Assert.NotNull(imageDownload);
            Assert.Equal(imageUpload.Id, imageDownload.Id);
        }
    }
}
