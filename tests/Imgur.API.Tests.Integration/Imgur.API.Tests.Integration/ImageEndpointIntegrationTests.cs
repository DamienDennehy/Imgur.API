using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Endpoints;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssemblyFixture;

namespace Imgur.API.Tests.Integration
{
    public class ImageEndpointIntegrationTests : IAssemblyFixture<ApiClientAssemblyFixture>
    {
        private readonly ApiClientAssemblyFixture _fixture;
        private readonly ITestOutputHelper _output;


        public ImageEndpointIntegrationTests(ApiClientAssemblyFixture fixture,
                                             ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
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

        [Fact]
        public async Task UploadVideoStreamWithClientKey_UploadsImageStream()
        {
            var currentProgress = 0;
            var apiClient = _fixture.GetApiClientWithKey();
            var httpClient = new HttpClient();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "banana.jpg");
            using var fileStream = File.OpenRead(filePath);

            void report(int progress)
            {
                currentProgress += progress;
                _output.WriteLine($"{currentProgress} of {fileStream.Length}");
            }

            var uploadProgress = new Progress<int>(percent => report(percent));

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream, progress: uploadProgress, bufferSize: 4096);
            var imageDownload = await imageEndpoint.GetImageAsync(imageUpload.Id);

            Assert.NotNull(imageUpload);
            Assert.NotNull(imageDownload);
            Assert.Equal(imageUpload.Id, imageDownload.Id);
        }
    }
}
