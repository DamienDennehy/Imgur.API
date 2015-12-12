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

        /// <summary>
        ///     Add a fake response with a corresponding uri.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="response"></param>
        public void AddFakeResponse(Uri uri, HttpResponseMessage response)
        {
            var key = uri.ToString();
            _fakeResponses.Add(key, response);
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