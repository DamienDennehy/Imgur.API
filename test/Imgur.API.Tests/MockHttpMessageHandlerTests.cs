using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests
{
    public class MockHttpMessageHandlerTests
    {
        public static Dictionary<string, string> PostContent => new Dictionary<string, string>
        {
            {"Name", "bob"},
            {"Address", "Ireland"},
            {"Phone", "12345"}
        };

        [Theory]
        [InlineData("http://example.org/url", "Hello World")]
        public async Task HttpClient_GetAsync(string requestUrl, string responseContent)
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(responseContent)};

            var httpClient = new HttpClient(new MockHttpMessageHandler(requestUrl, mockResponse));
            var httpResponse = await httpClient.GetAsync(requestUrl).ConfigureAwait(false);
            var stringResponse = string.Empty;

            if (httpResponse.Content != null)
                stringResponse = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(responseContent, stringResponse);
        }

        [Theory]
        [InlineData("http://example.org/url", "accepted=true")]
        public async Task HttpClient_PostAsync(string requestUrl, string responseContent)
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(responseContent)};
            var mockPostContent = new FormUrlEncodedContent(PostContent.ToArray());
            var mockPostContentString = await mockPostContent.ReadAsStringAsync().ConfigureAwait(false);

            var httpClient = new HttpClient(new MockHttpMessageHandler(requestUrl, mockResponse));
            var httpResponse = await httpClient.PostAsync(requestUrl, mockPostContent).ConfigureAwait(false);
            var stringResponse = string.Empty;

            if (httpResponse.Content != null)
                stringResponse = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(mockPostContentString, "Name=bob&Address=Ireland&Phone=12345");
            Assert.Equal(responseContent, stringResponse);
        }
    }
}