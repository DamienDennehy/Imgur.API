using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        internal GalleryRequestBuilder GalleryRequestBuilder { get; } = new GalleryRequestBuilder();

        /// <summary>
        ///     Returns the users favorited images. OAuth authentication required.
        /// </summary>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <param name="sort">The order that the account gallery should be sorted by. Default: Newest</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<IEnumerable<GalleryItem>> GetAccountFavorites(int? page = null,
            AccountGallerySortOrder? sort = AccountGallerySortOrder.Newest)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            sort = sort ?? AccountGallerySortOrder.Newest;

            var sortValue = $"{sort}".ToLower();
            var url = $"account/me/favorites/{page}/{sortValue}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<IEnumerable<GalleryItem>>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Return the images the user has favorited in the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <param name="sort">The order that the account gallery should be sorted by. Default: Newest</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<IEnumerable<GalleryItem>> GetAccountGalleryFavorites(string username = "me",
            int? page = null,
            AccountGallerySortOrder? sort = AccountGallerySortOrder.Newest)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            sort = sort ?? AccountGallerySortOrder.Newest;

            var sortValue = $"{sort}".ToLower();
            var url = $"account/{username}/gallery_favorites/{page}/{sortValue}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<IEnumerable<GalleryItem>>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Return the images a user has submitted to the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<IEnumerable<GalleryItem>> GetAccountSubmissions(string username = "me", int? page = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/submissions/{page}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<IEnumerable<GalleryItem>>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     The totals for a users gallery information.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<GalleryProfile> GetGalleryProfile()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/me/gallery_profile";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<GalleryProfile>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }
    }
}