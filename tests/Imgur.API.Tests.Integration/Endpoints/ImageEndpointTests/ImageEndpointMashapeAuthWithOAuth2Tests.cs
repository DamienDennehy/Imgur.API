using System.IO;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.ImageEndpointTests
{
    [TestClass]
    public class ImageEndpointMshapeAuthWithOAuth2Tests : TestBase
    {
        [TestMethod]
        public async Task UploadImageBinaryAsync_WithImage_AreEqual()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new ImageEndpoint(client);

            var file = File.ReadAllBytes("banana.gif");
            var image = await endpoint.UploadImageBinaryAsync(file, null, "binary test title!", "binary test desc!");

            Assert.IsFalse(string.IsNullOrEmpty(image.Id));
            Assert.IsFalse(string.IsNullOrEmpty(image.AccountId));
            Assert.AreEqual("binary test title!", image.Title);
            Assert.AreEqual("binary test desc!", image.Description);

            await GetImageAsync_WithImage_AreEqual(image);
            await UpdateImageAsync_WithImage_AreEqual(image);
            await FavoriteImageAsync_WithNotFavoritedImage_IsTrue(image);
            await UnfavoriteImageAsync_WithFavoritedImage_IsFalse(image);
            await DeleteImageAsync_WithImage_IsTrue(image);
        }

        [TestMethod]
        public async Task UploadImageStreamAsync_WithImage_AreEqual()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new ImageEndpoint(client);
            IImage image = null;

            using (var fs = new FileStream("banana.gif", FileMode.Open))
            {
                image = await endpoint.UploadImageStreamAsync(fs, null, "binary test title!", "binary test desc!");
            }

            Assert.IsFalse(string.IsNullOrEmpty(image.Id));
            Assert.IsFalse(string.IsNullOrEmpty(image.AccountId));
            Assert.AreEqual("binary test title!", image.Title);
            Assert.AreEqual("binary test desc!", image.Description);

            await GetImageAsync_WithImage_AreEqual(image);
            await UpdateImageAsync_WithImage_AreEqual(image);
            await DeleteImageAsync_WithImage_IsTrue(image);
        }

        [TestMethod]
        public async Task UploadImageUrlAsync_WithImage_AreEqual()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new ImageEndpoint(client);

            var image =
                await
                    endpoint.UploadImageUrlAsync("http://i.imgur.com/Eg71tvs.gif", null, "url test title!",
                        "url test desc!");

            Assert.IsFalse(string.IsNullOrEmpty(image.Id));
            Assert.IsFalse(string.IsNullOrEmpty(image.AccountId));
            Assert.AreEqual("url test title!", image.Title);
            Assert.AreEqual("url test desc!", image.Description);

            await GetImageAsync_WithImage_AreEqual(image);
            await UpdateImageAsync_WithImage_AreEqual(image);
            await FavoriteImageAsync_WithNotFavoritedImage_IsTrue(image);
            await UnfavoriteImageAsync_WithFavoritedImage_IsFalse(image);
            await DeleteImageAsync_WithImage_IsTrue(image);
        }

        public async Task GetImageAsync_WithImage_AreEqual(IImage actualImage)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new ImageEndpoint(client);

            var expectedImage = await endpoint.GetImageAsync(actualImage.Id);

            Assert.AreEqual(actualImage.Id, expectedImage.Id);
            Assert.AreEqual(actualImage.Title, expectedImage.Title);
            Assert.AreEqual(actualImage.Description, expectedImage.Description);
            Assert.AreEqual(actualImage.DateTime, expectedImage.DateTime);
            Assert.AreEqual(actualImage.Type, expectedImage.Type);
            Assert.AreEqual(actualImage.Animated, expectedImage.Animated);
            Assert.AreEqual(actualImage.Width, expectedImage.Width);
            Assert.AreEqual(actualImage.Height, expectedImage.Height);
            Assert.AreEqual(actualImage.Size, expectedImage.Size);
            Assert.AreEqual(actualImage.Link, expectedImage.Link);
            Assert.AreEqual(actualImage.Gifv, expectedImage.Gifv);
            Assert.AreEqual(actualImage.Mp4, expectedImage.Mp4);
            Assert.AreEqual(actualImage.Webm, expectedImage.Webm);
            Assert.AreEqual(actualImage.Looping, expectedImage.Looping);
            Assert.AreEqual(actualImage.Favorite, expectedImage.Favorite);
            Assert.AreEqual(actualImage.Nsfw, expectedImage.Nsfw);
        }

        public async Task UpdateImageAsync_WithImage_AreEqual(IImage actualImage)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new ImageEndpoint(client);

            var expected = await endpoint.UpdateImageAsync(actualImage.Id, "Ti", "De");

            Assert.IsTrue(expected);
        }

        public async Task FavoriteImageAsync_WithNotFavoritedImage_IsTrue(IImage actualImage)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new ImageEndpoint(client);

            var expected = await endpoint.FavoriteImageAsync(actualImage.Id);

            Assert.IsTrue(expected);
        }

        public async Task UnfavoriteImageAsync_WithFavoritedImage_IsFalse(IImage actualImage)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new ImageEndpoint(client);

            var expected = await endpoint.FavoriteImageAsync(actualImage.Id);

            Assert.IsFalse(expected);
        }

        public async Task DeleteImageAsync_WithImage_IsTrue(IImage actualImage)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new ImageEndpoint(client);

            var expected = await endpoint.DeleteImageAsync(actualImage.Id);

            Assert.IsTrue(expected);
        }
    }
}