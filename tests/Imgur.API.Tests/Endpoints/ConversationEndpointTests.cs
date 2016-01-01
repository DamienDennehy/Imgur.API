using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class ConversationEndpointTests : TestBase
    {
        [TestMethod]
        public async Task BlockSenderAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/conversations/block/Bob";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.BlockSenderAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var created = await endpoint.BlockSenderAsync("Bob").ConfigureAwait(false);

            Assert.IsTrue(created);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task BlockSenderAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.BlockSenderAsync("Recipient").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task BlockSenderAsync_WithRecipientNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.BlockSenderAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task CreateConversationAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/conversations/Bob";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.CreateConversationAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var created = await endpoint.CreateConversationAsync("Bob", "Hello World!").ConfigureAwait(false);

            Assert.IsTrue(created);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateConversationAsync_WithBodyNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.CreateConversationAsync("Recipient", null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateConversationAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.CreateConversationAsync("Recipient", "Body").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateConversationAsync_WithRecipientNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.CreateConversationAsync(null, "Body").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task DeleteConversationAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/conversations/Bob";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.DeleteConversationAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var deleted = await endpoint.DeleteConversationAsync("Bob").ConfigureAwait(false);

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteConversationAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.DeleteConversationAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteConversationAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.DeleteConversationAsync("id").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetConversationAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/conversations/1234/1/0";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.GetConversationAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var conversation = await endpoint.GetConversationAsync("1234").ConfigureAwait(false);

            Assert.IsNotNull(conversation);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1451010592),
                conversation.DateTime);
            Assert.AreEqual(false, conversation.Done);
            Assert.AreEqual(34361981, conversation.Id);
            Assert.AreEqual("Test 2", conversation.LastMessagePreview);
            Assert.AreEqual(3, conversation.Messages.Count());
            Assert.AreEqual(3, conversation.MessageCount);
            Assert.AreEqual(2, conversation.Page);
            Assert.AreEqual("Bob", conversation.WithAccount);
            Assert.AreEqual(1198054, conversation.WithAccountId);
            Assert.IsTrue(conversation.Messages.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetConversationAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.GetConversationAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetConversationAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.GetConversationAsync("1234").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetConversationsAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/conversations";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.GetConversationsAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var conversations = await endpoint.GetConversationsAsync().ConfigureAwait(false);

            Assert.IsNotNull(conversations);
            Assert.IsTrue(conversations.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetConversationsAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.GetConversationsAsync().ConfigureAwait(false);
        }

        [TestMethod]
        public async Task ReportSenderAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/conversations/report/Bob";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.ReportSenderAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var created = await endpoint.ReportSenderAsync("Bob").ConfigureAwait(false);

            Assert.IsTrue(created);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task ReportSenderAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.ReportSenderAsync("Username").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task ReportSenderAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.ReportSenderAsync(null).ConfigureAwait(false);
        }
    }
}