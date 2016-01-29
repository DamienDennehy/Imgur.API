using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Imgur.API.Tests.Mocks
{
    /// <summary>
    ///     Mock HttpMessageHandler for Unit Testing HttpClient.
    /// </summary>
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly Dictionary<string, HttpResponseMessage> _responseMessages =
            new Dictionary<string, HttpResponseMessage>();

        public MockHttpMessageHandler()
        {
        }

        public MockHttpMessageHandler(string url, HttpResponseMessage response)
        {
            _responseMessages.Add(url, response);
        }

        public void AddResponseMessage(string url, HttpResponseMessage response)
        {
            _responseMessages.Add(url, response);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var key = request.RequestUri.ToString();
            if (_responseMessages.ContainsKey(key))
                return _responseMessages[key];

            var notFoundMessage = new HttpResponseMessage(HttpStatusCode.NotFound) {RequestMessage = request};

            return await Task.FromResult(notFoundMessage).ConfigureAwait(false);
        }
    }
}