using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Endpoints;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace Imgur.API.Tests.Integration.EndpointTests.ImageEndPointTests
{
    public class AlbumEndpointClientKeyTests : IAssemblyFixture<ApiClientAssemblyFixture>
    {
        private readonly ApiClientAssemblyFixture _fixture;


        public AlbumEndpointClientKeyTests(ApiClientAssemblyFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AlbumEndpoint_AllMethods_ShouldExecute()
        {
            var apiClient = _fixture.GetApiClientWithKey();
            var httpClient = new HttpClient();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "banana.jpg");
            using var fileStream = File.OpenRead(filePath);

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var albumEndpoint = new AlbumEndpoint(apiClient, httpClient);

            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream);
            var album = await albumEndpoint.GetAlbumAsync("Pqgf62I");
            //await imageEndpoint.UpdateImageAsync(imageUpload.DeleteHash, title: "updated");

            //var image = await imageEndpoint.GetImageAsync(imageUpload.Id);
            //var imageDeleted = await imageEndpoint.DeleteImageAsync(imageUpload.DeleteHash);

            Assert.NotNull(imageUpload);
            //Assert.Equal("uploaded", imageUpload.Title);
            //Assert.Equal("updated", image.Title);
            //Assert.True(imageDeleted);
        }
    }
}
