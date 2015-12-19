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
        internal const string OAuth2RequiredExceptionMessage = "User authentication required.";

        /// <summary>
        ///     Initializes a new instance of the EndpointBase class.
        /// </summary>
        /// <param name="apiClient"></param>
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
        /// <param name="apiClient"></param>
        /// <param name="httpClient"></param>
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

            httpClient.BaseAddress = new Uri(apiClient.EndpointUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ApiClient = apiClient;
            HttpClient = httpClient;
        }

        /// <summary>
        ///     The class for sending HTTP requests and receiving HTTP responses from the service methods.
        /// </summary>
        public virtual HttpClient HttpClient { get; }

        /// <summary>
        ///     Interface for all API authentication types.
        /// </summary>
        public virtual IApiClient ApiClient { get; private set; }

        /// <summary>
        ///     Switch from one client type to another.
        /// </summary>
        /// <param name="apiClient"></param>
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
        ///     Parses the string response from the endpoint into an expected type T.
        /// </summary>
        /// <typeparam name="T">The expected output type, Image, bool, etc.</typeparam>
        /// <param name="endpointStringResponse">The string response from the endpoint.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <returns></returns>
        internal T ProcessEndpointResponse<T>(string endpointStringResponse)
        {
            //If no result is found, then we can't proceed
            if (string.IsNullOrWhiteSpace(endpointStringResponse))
                throw new ImgurException("The response from the endpoint is empty.");

            //If the result isn't a json response, then we can't proceed
            if (endpointStringResponse.StartsWith("<"))
                throw new ImgurException("The response from the endpoint is invalid.");

            //If the authentication method is Mashape, then an error response
            //is different to that of Imgur's response.
            if (ApiClient is IMashapeClient && endpointStringResponse.Contains("{\"message\":"))
            {
                var maShapeError = JsonConvert.DeserializeObject<MashapeError>(endpointStringResponse);
                throw new MashapeException(maShapeError.Message);
            }

            //If an error occurs, throw an exception
            if (endpointStringResponse.Contains("{\"data\":{\"error\":"))
            {
                var apiError = JsonConvert.DeserializeObject<Basic<ImgurError>>(endpointStringResponse);
                throw new ImgurException(apiError.Data.Error);
            }

            //If the type being requested is an oAuthToken
            //Deserialize it immediately and return
            if (typeof (T) == typeof (IOAuth2Token) || typeof (T) == typeof (OAuth2Token))
            {
                var oAuth2Response = JsonConvert.DeserializeObject<T>(endpointStringResponse);
                return oAuth2Response;
            }

            //Deserialize the response into a generic Basic<object> type
            var response = JsonConvert.DeserializeObject<Basic<object>>(endpointStringResponse);

            //If the response was not a success, then the response type is Basic<ImgurError>
            //and should be handled as such.
            if (!response.Success)
            {
                var apiError = JsonConvert.DeserializeObject<Basic<ImgurError>>(endpointStringResponse);
                throw new ImgurException(apiError.Data.Error);
            }

            var converters = new JsonConverter[1];
            converters[0] = new GalleryItemConverter();

            //Deserialize the actual response
            var finalResponse = JsonConvert.DeserializeObject<Basic<T>>(endpointStringResponse, converters);

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
        internal async Task<T> SendRequestAsync<T>(HttpRequestMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var httpResponse = await HttpClient.SendAsync(message);

            string stringResponse = null;

            if (httpResponse.Content != null)
                stringResponse = await httpResponse.Content.ReadAsStringAsync();

            UpdateRateLimit(httpResponse.Headers);

            return ProcessEndpointResponse<T>(stringResponse);
        }

        /// <summary>
        ///     Updates the ApiClient's RateLimit
        ///     with the values from the last response from the Api.
        /// </summary>
        /// <param name="headers"></param>
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
                var userLimit = headers.GetValues("X-RateLimit-UserLimit").FirstOrDefault();
                var userRemaining = headers.GetValues("X-RateLimit-UserRemaining").FirstOrDefault();
                var userReset = headers.GetValues("X-RateLimit-UserReset").FirstOrDefault();

                ApiClient.RateLimit.ClientLimit = Convert.ToInt32(clientLimit);
                ApiClient.RateLimit.ClientRemaining = Convert.ToInt32(clientRemaining);
                ApiClient.RateLimit.UserLimit = Convert.ToInt32(userLimit);
                ApiClient.RateLimit.UserRemaining = Convert.ToInt32(userRemaining);
                ApiClient.RateLimit.UserReset =
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(Convert.ToInt64(userReset));

                ApiClient.RateLimit.MashapeUploadsLimit = null;
                ApiClient.RateLimit.MashapeUploadsRemaining = null;
            }

            if (ApiClient is IMashapeClient
                && headers.Any(x => x.Key.Equals("X-RateLimit-Requests-Limit")))
            {
                var requestsLimit = headers.GetValues("X-RateLimit-Requests-Limit").FirstOrDefault();
                var requestsRemaining = headers.GetValues("X-RateLimit-Requests-Remaining").FirstOrDefault();
                var uploadsLimit = headers.GetValues("X-RateLimit-Uploads-Limit").FirstOrDefault();
                var uploadsRemaining = headers.GetValues("X-RateLimit-Uploads-Remaining").FirstOrDefault();

                ApiClient.RateLimit.ClientLimit = Convert.ToInt32(requestsLimit);
                ApiClient.RateLimit.ClientRemaining = Convert.ToInt32(requestsRemaining);
                ApiClient.RateLimit.MashapeUploadsLimit = Convert.ToInt32(uploadsLimit);
                ApiClient.RateLimit.MashapeUploadsRemaining = Convert.ToInt32(uploadsRemaining);

                ApiClient.RateLimit.UserLimit = null;
                ApiClient.RateLimit.UserRemaining = null;
                ApiClient.RateLimit.UserReset = null;
            }
        }
    }
}