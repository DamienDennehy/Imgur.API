using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Meme related actions.
    /// </summary>
    public class MemeGenEndpoint : EndpointBase, IMemeGenEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the MemeGenEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public MemeGenEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the MemeGenEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal MemeGenEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal MemeGenRequestBuilder RequestBuilder { get; } = new MemeGenRequestBuilder();

        /// <summary>
        ///     Get the list of default memes.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IImage>> GetDefaultMemesAsync()
        {
            var url = "memegen/defaults";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var images = await SendRequestAsync<IEnumerable<Image>>(request).ConfigureAwait(false);
                return images;
            }
        }
    }
}