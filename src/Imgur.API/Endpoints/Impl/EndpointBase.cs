using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.JsonConverters;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        protected EndpointBase(IApiClient apiClient)
            : this(apiClient, new HttpClient())
        {
            ApiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        /// <summary>
        ///     Initializes a new instance of the EndpointBase class.
        /// </summary>
        internal EndpointBase()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the EndpointBase class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal EndpointBase(IApiClient apiClient, HttpClient httpClient)
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
            if (apiClient is IMashapeClient mashapeClient)
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                    "X-Mashape-Key", mashapeClient.MashapeKey);
            }

            httpClient.BaseAddress = new Uri(apiClient.EndpointUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ApiClient = apiClient;
            HttpClient = httpClient;
        }

        /// <summary>
        ///     Interface for all API authentication types.
        /// </summary>
        public virtual IApiClient ApiClient { get; private set; }

        /// <summary>
        ///     The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.
        /// </summary>
        public virtual HttpClient HttpClient { get; }

        /// <summary>
        ///     Switch from one client type to another.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        public virtual void SwitchClient(IApiClient apiClient)
        {
            ApiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

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
            if (apiClient is IMashapeClient mashapeClient)
            {
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                    "X-Mashape-Key", mashapeClient.MashapeKey);
            }

            HttpClient.BaseAddress = new Uri(apiClient.EndpointUrl);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        ///     Parses the string stringResponse from the endpoint into an expected type T.
        /// </summary>
        /// <typeparam name="T">The expected output type, Image, bool, etc.</typeparam>
        /// <param name="response">The response from the endpoint.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <returns></returns>
        internal virtual T ProcessEndpointResponse<T>(HttpResponseMessage response)
        {
            //If no result is found, then we can't proceed
            if (response == null)
                throw new ImgurException("The response from the endpoint is missing.");

            var stringResponse = string.Empty;

            if (response.Content != null)
            {
                stringResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult().Trim();
            }

            //If no result is found, then we can't proceed
            if (string.IsNullOrWhiteSpace(stringResponse))
                throw new ImgurException($"The response from the endpoint is missing. {(int)response.StatusCode} {response.ReasonPhrase}");

            //If the result isn't a json response, then we can't proceed
            if (stringResponse.StartsWith("<"))
            {
                throw new ImgurException($"The response from the endpoint is invalid. {(int)response.StatusCode} {response.ReasonPhrase}");
            }

            //If the authentication method is Mashape, then an error response
            //is different to that of Imgur's error response.
            if (ApiClient is IMashapeClient && stringResponse.StartsWith("{\"message\":"))
            {
                var maShapeError = JsonConvert.DeserializeObject<MashapeError>(stringResponse);
                throw new MashapeException(maShapeError.Message);
            }

            //If an error occurs, throw an exception
            if (stringResponse.StartsWith("{\"data\":{\"error\":"))
            {
                dynamic jsonResponse = JObject.Parse(stringResponse);
                if (stringResponse.Contains("{\"message\":"))
                {
                    throw new ImgurException(jsonResponse.data.error.message.ToString());
                }
                else
                {
                    throw new ImgurException(jsonResponse.data.error.ToString());
                }
            }

            //If an error occurs, throw an exception
            if (stringResponse.StartsWith("{\"data\":\"An error occurred"))
            {
                dynamic jsonResponse = JObject.Parse(stringResponse);
                throw new ImgurException(jsonResponse.data.ToString());
            }

            //If the type being requested is an OAuth2Token
            //Deserialize it immediately and return
            if (typeof(T) == typeof(IOAuth2Token) || typeof(T) == typeof(OAuth2Token))
            {
                var oAuth2Response = JsonConvert.DeserializeObject<T>(stringResponse);
                return oAuth2Response;
            }

            //Deserialize the response into a generic Basic<object> type
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

            //Deserialize the response to the correct Type
            var finalResponse = JsonConvert.DeserializeObject<Basic<T>>(stringResponse, converters);

            return finalResponse.Data;
        }

        /// <summary>
        ///     Send requests to the service.
        /// </summary>
        /// <param name="message">The HttpRequestMessage that should be sent.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <returns></returns>
        internal virtual async Task<T> SendRequestAsync<T>(HttpRequestMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var httpResponse = await HttpClient.SendAsync(message).ConfigureAwait(false);

            UpdateRateLimit(httpResponse.Headers);

            return ProcessEndpointResponse<T>(httpResponse);
        }

        /// <summary>
        ///     Updates the ApiClient's RateLimit
        ///     with the values from the last response from the Api.
        /// </summary>
        /// <param name="headers">The headers from the last request to the endpoint.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal virtual void UpdateRateLimit(HttpResponseHeaders headers)
        {
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            var clientLimitHeader = string.Empty;
            var clientRemainingHeader = string.Empty;

            if (ApiClient is IImgurClient
                && headers.Any(x => x.Key.Equals("X-RateLimit-ClientLimit")))
            {
                clientLimitHeader = headers.GetValues("X-RateLimit-ClientLimit").FirstOrDefault();
                clientRemainingHeader = headers.GetValues("X-RateLimit-ClientRemaining").FirstOrDefault();
            }

            if (ApiClient is IMashapeClient
                && headers.Any(x => x.Key.Equals("X-RateLimit-Requests-Limit")))
            {
                clientLimitHeader = headers.GetValues("X-RateLimit-Requests-Limit").FirstOrDefault();
                clientRemainingHeader = headers.GetValues("X-RateLimit-Requests-Remaining").FirstOrDefault();
            }

            //If the values can't be parsed use the previous value
            if (!int.TryParse(clientLimitHeader, out int clientLimit))
                clientLimit = ApiClient.RateLimit.ClientLimit;

            if (!int.TryParse(clientRemainingHeader, out int clientRemaining))
                clientRemaining = ApiClient.RateLimit.ClientRemaining;

            ApiClient.RateLimit.ClientLimit = clientLimit;
            ApiClient.RateLimit.ClientRemaining = clientRemaining;
        }
    }
}