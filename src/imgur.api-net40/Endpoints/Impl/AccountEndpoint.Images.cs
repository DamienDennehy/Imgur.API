using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        internal ImageRequestBuilder ImageRequestBuilder { get; } = new ImageRequestBuilder();

        /// <summary>
        ///     Deletes an Image. You are required to be logged in as the user whom created the image.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<bool> DeleteImage(string imageId, string username = "me")
        {
            if (string.IsNullOrWhiteSpace(imageId))
                throw new ArgumentNullException(nameof(imageId));

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/image/{imageId}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<bool>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Return information about a specific image.
        /// </summary>
        /// <param name="imageId">The image's id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<Image> GetImage(string imageId, string username = "me")
        {
            if (string.IsNullOrWhiteSpace(imageId))
                throw new ArgumentNullException(nameof(imageId));

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/image/{imageId}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<Image>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Returns the total number of images associated with the account.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="username">The username. Default: me</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<int> GetImageCount(string username = "me")
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/images/count";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<int>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Returns a list of Image IDs that are associated with the account.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<IEnumerable<string>> GetImageIds(string username = "me", int? page = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/images/ids/{page}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<IEnumerable<string>>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Return all of the images associated with the account.
        ///     You can page through the images by setting the page, this defaults to 0.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<IEnumerable<Image>> GetImages(string username = "me", int? page = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/images/{page}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<IEnumerable<Image>>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }
    }
}