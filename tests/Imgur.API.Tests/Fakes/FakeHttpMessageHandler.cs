using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Imgur.API.Tests.Fakes
{
    /// <summary>
    ///     Fake HttpMessageHandler for Unit Testing HttpClient.
    /// </summary>
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly Dictionary<string, HttpResponseMessage> _fakeResponses =
            new Dictionary<string, HttpResponseMessage>();

        public FakeHttpMessageHandler()
        {
            
        }

        public FakeHttpMessageHandler(string url, HttpResponseMessage response)
        {
            _fakeResponses.Add(url, response);
        }

        public void AddFakeResponse(string url, HttpResponseMessage response)
        {
            _fakeResponses.Add(url, response);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var key = request.RequestUri.ToString();
            if (_fakeResponses.ContainsKey(key))
                return _fakeResponses[key];

            return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound) {RequestMessage = request});
        }
    }
}