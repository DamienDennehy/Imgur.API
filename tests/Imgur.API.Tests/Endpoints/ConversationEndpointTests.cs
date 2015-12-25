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

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class ConversationEndpointTests : TestBase
    {
        [TestMethod]
        public async Task BlockSenderAsync_IsTrue()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.BlockSenderResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/conversations/block/Bob"),
                fakeResponse);

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var created = await endpoint.BlockSenderAsync("Bob");

            Assert.IsTrue(created);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task BlockSenderAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.BlockSenderAsync("Recipient");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task BlockSenderAsync_WithRecipientNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.BlockSenderAsync(null);
        }

        [TestMethod]
        public async Task CreateConversationAsync_IsTrue()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.CreateConversationResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/conversations/Bob"), fakeResponse);

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var created = await endpoint.CreateConversationAsync("Bob", "Hello World!");

            Assert.IsTrue(created);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateConversationAsync_WithBodyNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.CreateConversationAsync("Recipient", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateConversationAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.CreateConversationAsync("Recipient", "Body");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateConversationAsync_WithRecipientNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.CreateConversationAsync(null, "Body");
        }

        [TestMethod]
        public async Task DeleteConversationAsync_IsTrue()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.DeleteConversationResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/conversations/Bob"), fakeResponse);

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var deleted = await endpoint.DeleteConversationAsync("Bob");

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteConversationAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.DeleteConversationAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteConversationAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.DeleteConversationAsync("id");
        }

        [TestMethod]
        public async Task GetConversationAsync_IsTrue()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.GetConversationResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/conversations/1234/1/0"),
                fakeResponse);

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var conversation = await endpoint.GetConversationAsync("1234");

            Assert.IsNotNull(conversation);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1451010592),
                conversation.DateTime);
            Assert.AreEqual(false, conversation.Done);
            Assert.AreEqual(34361981, conversation.Id);
            Assert.AreEqual(null, conversation.LastMessage);
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
            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new ConversationEndpoint(client);
            await endpoint.GetConversationAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetConversationAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.GetConversationAsync("1234");
        }

        [TestMethod]
        public async Task GetConversationsAsync_IsTrue()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.GetConversationsResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/conversations"), fakeResponse);

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var conversations = await endpoint.GetConversationsAsync();

            Assert.IsNotNull(conversations);
            Assert.IsTrue(conversations.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetConversationsAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.GetConversationsAsync();
        }

        [TestMethod]
        public async Task ReportSenderAsync_IsTrue()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ConversationEndpointResponses.ReportSenderResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/conversations/report/Bob"),
                fakeResponse);

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var created = await endpoint.ReportSenderAsync("Bob");

            Assert.IsTrue(created);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task ReportSenderAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            await endpoint.ReportSenderAsync("Username");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task ReportSenderAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ConversationEndpoint(client);
            await endpoint.ReportSenderAsync(null);
        }
    }
}