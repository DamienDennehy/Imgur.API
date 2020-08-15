using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Endpoints;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssemblyFixture;

namespace Imgur.API.Tests.Integration.EndpointTests.ImageEndPointTests
{
    public class ImageEndpointOAuth2Tests : IAssemblyFixture<ApiClientAssemblyFixture>
    {
        private readonly ApiClientAssemblyFixture _fixture;
        private readonly ITestOutputHelper _output;


        public ImageEndpointOAuth2Tests(ApiClientAssemblyFixture fixture,
                                           ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public async Task FavoriteImage_UploadsImage()
        {
            var apiClient = _fixture.GetApiClientWithKeyAndOAuthToken();
            var httpClient = new HttpClient();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "banana.jpg");
            using var fileStream = File.OpenRead(filePath);

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);

            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream);
            var favorited = await imageEndpoint.FavoriteImageAsync(imageUpload.Id);

            var imageDeleted = await imageEndpoint.DeleteImageAsync(imageUpload.Id);

            Assert.NotNull(imageUpload);
            Assert.True(imageDeleted);
            Assert.Equal("favorited", favorited);
        }

        [Fact]
        public async Task UploadImageUrl_UploadsImage()
        {
            var apiClient = _fixture.GetApiClientWithKeyAndOAuthToken();
            var httpClient = new HttpClient();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "banana.jpg");
            using var fileStream = File.OpenRead(filePath);

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);

            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream);
            var imageUrlUpload = await imageEndpoint.UploadImageAsync(imageUpload.Link);

            var imageDeleted = await imageEndpoint.DeleteImageAsync(imageUpload.Id);
            var imageUrlDeleted = await imageEndpoint.DeleteImageAsync(imageUrlUpload.Id);

            Assert.NotNull(imageUrlUpload);
            Assert.NotEqual(imageUpload.Id, imageUrlUpload.Id);
            Assert.True(imageDeleted);
            Assert.True(imageUrlDeleted);
        }

        [Fact]
        public async Task UpdateImage_UpdatesImage()
        {
            var apiClient = _fixture.GetApiClientWithKeyAndOAuthToken();
            var httpClient = new HttpClient();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "banana.jpg");
            using var fileStream = File.OpenRead(filePath);

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);

            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream, title: "uploaded");
            await imageEndpoint.UpdateImageAsync(imageUpload.Id, title: "updated");
            
            var image = await imageEndpoint.GetImageAsync(imageUpload.Id);
            var imageDeleted = await imageEndpoint.DeleteImageAsync(imageUpload.Id);

            Assert.NotNull(imageUpload);
            Assert.Equal("uploaded", imageUpload.Title);
            Assert.Equal("updated", image.Title);
            Assert.True(imageDeleted);
        }

        [Fact]
        public async Task UploadImageStream_UploadsImage()
        {
            var apiClient = _fixture.GetApiClientWithKeyAndOAuthToken();
            var httpClient = new HttpClient();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "banana.jpg");
            using var fileStream = File.OpenRead(filePath);

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);

            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream);
            var imageDeleted = await imageEndpoint.DeleteImageAsync(imageUpload.Id);

            Assert.NotNull(imageUpload);
            Assert.True(imageDeleted);
        }

        [Fact]
        public async Task UploadImageProgressStream_UploadsImage()
        {
            var totalProgress = 0;
            var apiClient = _fixture.GetApiClientWithKeyAndOAuthToken();
            var httpClient = new HttpClient();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "banana.jpg");
            using var fileStream = File.OpenRead(filePath);

            void report(int progress)
            {
                totalProgress += progress;
            }

            var uploadProgress = new Progress<int>(report);

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);

            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream, progress: uploadProgress, bufferSize: 4096);
            var imageDeleted = await imageEndpoint.DeleteImageAsync(imageUpload.Id);

            Assert.NotNull(imageUpload);
            Assert.True(imageDeleted);

            _output.WriteLine($"{totalProgress} of {fileStream.Length} reported.");
        }

        [Fact]
        public async Task UploadVideoStream_UploadsVideo()
        {
            var apiClient = _fixture.GetApiClientWithKeyAndOAuthToken();
            var httpClient = new HttpClient();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "banana.mp4");
            using var fileStream = File.OpenRead(filePath);

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);

            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream);
            var imageDeleted = await imageEndpoint.DeleteImageAsync(imageUpload.Id);

            Assert.NotNull(imageUpload);
            Assert.True(imageDeleted);
        }

        [Fact]
        public async Task UploadVideoProgressStream_UploadsVideo()
        {
            var totalProgress = 0;
            var apiClient = _fixture.GetApiClientWithKeyAndOAuthToken();
            var httpClient = new HttpClient();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "banana.mp4");
            using var fileStream = File.OpenRead(filePath);

            void report(int progress)
            {
                totalProgress += progress;
            }

            var uploadProgress = new Progress<int>(report);

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);

            var imageUpload = await imageEndpoint.UploadVideoAsync(fileStream, progress: uploadProgress, bufferSize: 4096);
            var imageDeleted = await imageEndpoint.DeleteImageAsync(imageUpload.Id);

            Assert.NotNull(imageUpload);
            Assert.True(imageDeleted);

            _output.WriteLine($"{totalProgress} of {fileStream.Length} reported.");
        }
    }
}
