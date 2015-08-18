using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imgur.API.Authentication;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Builder class for endpoints.
    /// </summary>
    public abstract class EndpointBase : IEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the EndpointBase class.
        /// </summary>
        protected EndpointBase()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the EndpointBase class.
        /// </summary>
        /// <param name="authentication"></param>
        protected EndpointBase(IApiAuthentication authentication)
        {
            if (authentication == null)
                throw new ArgumentNullException(nameof(authentication));

            ApiAuthentication = authentication;
        }

        /// <summary>
        ///     Interface for all API authentication types.
        /// </summary>
        public virtual IApiAuthentication ApiAuthentication { get; private set; }

        /// <summary>
        ///     The base Endpoint Url based on the current authentication set.
        ///     Example: https://api.imgur.com/3/
        /// </summary>
        public virtual string GetEndpointUrl()
        {
            if (ApiAuthentication is IImgurAuthentication)
                return "https://api.imgur.com/3/";

            if (ApiAuthentication is IMashapeAuthentication)
                return "https://imgur-apiv3.p.mashape.com/3/";

            throw new InvalidOperationException("Authentication type not recognized.");
        }

        /// <summary>
        ///     Switch from one authentication type to another.
        /// </summary>
        /// <param name="authentication"></param>
        public virtual void SwitchAuthentication(IApiAuthentication authentication)
        {
            if (authentication == null)
                throw new ArgumentNullException(nameof(authentication));

            ApiAuthentication = authentication;
        }

        /// <summary>
        ///     Gets a new HttpClient with DefaultRequestHeaders configured based
        ///     on the current ApiAuthentication set.
        /// </summary>
        /// <returns></returns>
        public HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();

            //Add Imgur Authentication header
            var imgurAuthentication = ApiAuthentication as IImgurAuthentication;
            if (imgurAuthentication != null)
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                    "Authorization", $"Client-ID {imgurAuthentication.ClientId}");
            }

            //Add Mashape Authentication header
            var mashapeAuthentication = ApiAuthentication as IMashapeAuthentication;
            if (mashapeAuthentication != null)
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                    "X-Mashape-Key", mashapeAuthentication.MashapeKey);

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                    "Authorization", $"Client-ID {mashapeAuthentication.ClientId}");
            }

            //Add OAuth Authentication header
            if (ApiAuthentication.OAuth2Authentication?.OAuth2Token != null)
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                    "Authorization", $"Bearer {ApiAuthentication.OAuth2Authentication.OAuth2Token.AccessToken}");

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        /// <summary>
        ///     Make requests to the endpoint.
        /// </summary>
        /// <param name="endpointUrl">The endpointUrl that should be called.</param>
        /// <param name="httpMethod">The HttpMethod that should be used.</param>
        /// <param name="content">The HttpContent that should be submitted.</param>
        /// <returns></returns>
        public async Task<T> MakeEndpointRequestAsync<T>(HttpMethod httpMethod, string endpointUrl, HttpContent content)
        {
            using (var httpClient = GetHttpClient())
            {
                HttpResponseMessage httpResponse;

                //Select the right method to use
                if (httpMethod == HttpMethod.Get)
                {
                    httpResponse = await httpClient.GetAsync(endpointUrl);
                }
                else if (httpMethod == HttpMethod.Post)
                {
                    httpResponse = await httpClient.PostAsync(endpointUrl, content);
                }
                else if (httpMethod == HttpMethod.Put)
                {
                    httpResponse = await httpClient.PutAsync(endpointUrl, content);
                }
                else if (httpMethod == HttpMethod.Delete)
                {
                    httpResponse = await httpClient.DeleteAsync(endpointUrl);
                }
                else
                {
                    throw new ArgumentException("Invalid HttpMethod provided.", nameof(httpMethod));
                }

                //Get the string response
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();

                return ProcessEndpointResponse<T>(stringResponse);
            }
        }

        /// <summary>
        /// Parses the string response from the endpoint into an expected type T.
        /// </summary>
        /// <typeparam name="T">The expected output type, Image, bool, etc.</typeparam>
        /// <param name="stringResponse">The string response from the endpoint.</param>
        /// <returns></returns>
        public T ProcessEndpointResponse<T>(string stringResponse)
        {
            //If no result is found, then we can't proceed
            if (string.IsNullOrWhiteSpace(stringResponse))
                throw new HttpRequestException("The response from the endpoint is empty.");

            ////If the authentication method is Mashape, then an error response
            ////is different to that of Imgur's response.
            //if (ApiAuthentication is IMashapeAuthentication && stringResponse.Contains("{\"message\":"))
            //{
            //    var maShapeError = JsonConvert.DeserializeObject<MashapeError>(stringResponse);
            //    throw new HttpRequestException(maShapeError.Message);
            //}

            ////If an error occurs, throw an exception
            //if (stringResponse.Contains("{\"data\":{\"error\":"))
            //{
            //    var apiError = JsonConvert.DeserializeObject<Basic<ImgurError>>(stringResponse);
            //    throw new HttpRequestException(apiError.Data.ErrorMessage);
            //}

            //if (typeof(T) == typeof(IOAuth2Token))
            //{
            //    //Deserialize the actual response
            //    var response = JsonConvert.DeserializeObject<OAuth2Token>(stringResponse);
            //    return response;
            //}

            ////Deserialize the response into a generic Basic<object> type
            //response = JsonConvert.DeserializeObject<Basic<object>>(stringResponse);

            ////If the response was not a success, then the response type is Basic<Error>
            ////and should be handled as such.
            //if (!response.Success)
            //{
            //    var apiError = JsonConvert.DeserializeObject<Basic<ImgurError>>(stringResponse);
            //    throw new EndpointException(apiError.Data.ErrorMessage);
            //}

            ////Deserialize the actual response
            //var finalResponse = JsonConvert.DeserializeObject<Basic<T>>(stringResponse);

            throw new NotImplementedException();
        }
    }
}