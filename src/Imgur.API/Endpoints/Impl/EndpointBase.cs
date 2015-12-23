using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Exceptions;
using Imgur.API.JsonConverters;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Newtonsoft.Json;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Builder class for endpoints.
    /// </summary>
    public abstract class EndpointBase : IEndpoint
    {
        internal const string OAuth2RequiredExceptionMessage = "OAuth authentication required.";

        /// <summary>
        ///     Initializes a new instance of the EndpointBase class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EndpointBase(IApiClient apiClient)
            : this(apiClient, new HttpClient())
        {
            if (apiClient == null)
                throw new ArgumentNullException(nameof(apiClient));

            ApiClient = apiClient;
        }

        /// <summary>
        ///     Initializes a new instance of the EndpointBase class.
        /// </summary>
        protected internal EndpointBase()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the EndpointBase class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected internal EndpointBase(IApiClient apiClient, HttpClient httpClient)
        {
            if (apiClient == null)
                throw new ArgumentNullException(nameof(apiClient));

            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Remove("X-Mashape-Key");
            httpClient.DefaultRequestHeaders.Accept.Clear();

            //Add OAuth Authentication header
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                "Authorization",
                apiClient.OAuth2Token != null
                    ? $"Bearer {apiClient.OAuth2Token.AccessToken}"
                    : $"Client-ID {apiClient.ClientId}");

            //Add Mashape Authentication header
            var mashapeClient = apiClient as IMashapeClient;
            if (mashapeClient != null)
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                    "X-Mashape-Key", mashapeClient.MashapeKey);
            }

            // ReSharper disable once ExceptionNotDocumented
            httpClient.BaseAddress = new Uri(apiClient.EndpointUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ApiClient = apiClient;
            HttpClient = httpClient;
        }

        /// <summary>
        ///     The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.
        /// </summary>
        public virtual HttpClient HttpClient { get; }

        /// <summary>
        ///     Interface for all API authentication types.
        /// </summary>
        public virtual IApiClient ApiClient { get; private set; }

        /// <summary>
        ///     Switch from one client type to another.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void SwitchClient(IApiClient apiClient)
        {
            if (apiClient == null)
                throw new ArgumentNullException(nameof(apiClient));

            ApiClient = apiClient;

            HttpClient.DefaultRequestHeaders.Remove("Authorization");
            HttpClient.DefaultRequestHeaders.Remove("X-Mashape-Key");
            HttpClient.DefaultRequestHeaders.Accept.Clear();

            //Add OAuth Authentication header
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                "Authorization",
                apiClient.OAuth2Token != null
                    ? $"Bearer {apiClient.OAuth2Token.AccessToken}"
                    : $"Client-ID {apiClient.ClientId}");

            //Add Mashape Authentication header
            var mashapeClient = apiClient as IMashapeClient;
            if (mashapeClient != null)
            {
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                    "X-Mashape-Key", mashapeClient.MashapeKey);
            }

            // ReSharper disable once ExceptionNotDocumented
            HttpClient.BaseAddress = new Uri(apiClient.EndpointUrl);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        ///     Parses the string stringResponse from the endpoint into an expected type T.
        /// </summary>
        /// <typeparam name="T">The expected output type, Image, bool, etc.</typeparam>
        /// <param name="stringResponse">The string response from the endpoint.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <returns></returns>
        internal virtual T ProcessEndpointResponse<T>(string stringResponse)
        {
            //If no result is found, then we can't proceed
            if (string.IsNullOrWhiteSpace(stringResponse))
                throw new ImgurException("The response from the endpoint is empty.");

            //If the result isn't a json response, then we can't proceed
            if (stringResponse.StartsWith("<"))
                throw new ImgurException("The response from the endpoint is invalid.");

            //If the authentication method is Mashape, then an error response
            //is different to that of Imgur's stringResponse.
            if (ApiClient is IMashapeClient && stringResponse.Contains("{\"message\":"))
            {
                var maShapeError = JsonConvert.DeserializeObject<MashapeError>(stringResponse);
                throw new MashapeException(maShapeError.Message);
            }

            //If an error occurs, throw an exception
            if (stringResponse.Contains("{\"data\":{\"error\":"))
            {
                var apiError = JsonConvert.DeserializeObject<Basic<ImgurError>>(stringResponse);
                throw new ImgurException(apiError.Data.Error);
            }

            //If the type being requested is an oAuthToken
            //Deserialize it immediately and return
            if (typeof (T) == typeof (IOAuth2Token) || typeof (T) == typeof (OAuth2Token))
            {
                var oAuth2Response = JsonConvert.DeserializeObject<T>(stringResponse);
                return oAuth2Response;
            }

            //Deserialize the stringResponse into a generic Basic<object> type
            var basicResponse = JsonConvert.DeserializeObject<Basic<object>>(stringResponse);

            //If the request was not a success, then the basicResponse type is Basic<ImgurError>
            //and should be handled as such.
            if (!basicResponse.Success)
            {
                var apiError = JsonConvert.DeserializeObject<Basic<ImgurError>>(stringResponse);
                throw new ImgurException(apiError.Data.Error);
            }

            var converters = new JsonConverter[1];
            converters[0] = new GalleryItemConverter();

            //Deserialize the stringResponse to the correct Type
            var finalResponse = JsonConvert.DeserializeObject<Basic<T>>(stringResponse, converters);

            return finalResponse.Data;
        }

        /// <summary>
        ///     Send requests to the service.
        /// </summary>
        /// <param name="message">The HttpRequestMessage that should be sent.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        internal virtual async Task<T> SendRequestAsync<T>(HttpRequestMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var httpResponse = await HttpClient.SendAsync(message);

            UpdateRateLimit(httpResponse.Headers);

            string stringResponse = null;

            if (httpResponse.Content != null)
                stringResponse = await httpResponse.Content.ReadAsStringAsync();

            //On rare occasions Imgur will not return any response (example: Too Many Requests StatusCode 429).
            //In this case, if the reason phrase is not null then we should throw an ImgurException.
            if (string.IsNullOrWhiteSpace(stringResponse) 
                && !httpResponse.IsSuccessStatusCode
                && !string.IsNullOrWhiteSpace(httpResponse.ReasonPhrase))
                throw new ImgurException(httpResponse.ReasonPhrase);

            return ProcessEndpointResponse<T>(stringResponse);
        }

        /// <summary>
        ///     Updates the ApiClient's RateLimit
        ///     with the values from the last stringResponse from the Api.
        /// </summary>
        /// <param name="headers">The stringResponse headers from the last request to the endpoint.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        internal virtual void UpdateRateLimit(HttpResponseHeaders headers)
        {
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            if (ApiClient is IImgurClient
                && headers.Any(x => x.Key.Equals("X-RateLimit-ClientLimit")))
            {
                var clientLimit = headers.GetValues("X-RateLimit-ClientLimit").FirstOrDefault();
                var clientRemaining = headers.GetValues("X-RateLimit-ClientRemaining").FirstOrDefault();

                ApiClient.RateLimit.ClientLimit = Convert.ToInt32(clientLimit);
                ApiClient.RateLimit.ClientRemaining = Convert.ToInt32(clientRemaining);
            }

            if (ApiClient is IMashapeClient
                && headers.Any(x => x.Key.Equals("X-RateLimit-Requests-Limit")))
            {
                var requestsLimit = headers.GetValues("X-RateLimit-Requests-Limit").FirstOrDefault();
                var requestsRemaining = headers.GetValues("X-RateLimit-Requests-Remaining").FirstOrDefault();

                ApiClient.RateLimit.ClientLimit = Convert.ToInt32(requestsLimit);
                ApiClient.RateLimit.ClientRemaining = Convert.ToInt32(requestsRemaining);
            }
        }
    }
}