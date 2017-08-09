using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Album related actions.
    /// </summary>
    public class AlbumEndpoint : EndpointBase, IAlbumEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the AlbumEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public AlbumEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the AlbumEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal AlbumEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal AlbumRequestBuilder RequestBuilder { get; } = new AlbumRequestBuilder();

        /// <summary>
        ///     Takes a list of imageIds to add to the album. For anonymous albums, {albumId} should be the
        ///     deletehash
        ///     that is returned at creation.
        /// </summary>
        /// <param name="albumId">The id or deletehash of the album.</param>
        /// <param name="imageIds">The imageIds that you want to be added to the album.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<bool> AddAlbumImages(string albumId, IEnumerable<string> imageIds)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            if (imageIds == null)
                throw new ArgumentNullException(nameof(imageIds));

            var url = $"album/{albumId}/add";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<bool>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Create a new album.
        /// </summary>
        /// <param name="title">The title of the album.</param>
        /// <param name="description">The description of the album.</param>
        /// <param name="privacy">Sets the privacy level of the album.</param>
        /// <param name="layout">Sets the layout to display the album.</param>
        /// <param name="coverId">The Id of an image that you want to be the cover of the album.</param>
        /// <param name="imageIds">The imageIds that you want to be included in the album.</param>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<Album> CreateAlbum(string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null, string coverId = null,
            IEnumerable<string> imageIds = null)
        {
            var url = "album";

            using (var request = AlbumRequestBuilder.CreateAlbumRequest(url, title, description,
                privacy, layout, coverId, imageIds))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<Album>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Delete an album with a given Id. You are required to be logged in as the user to delete the album. For anonymous
        ///     albums, {albumId} should be the deletehash that is returned at creation.
        /// </summary>
        /// <param name="albumId">The id or deletehash of the album.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<bool> DeleteAlbum(string albumId)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"album/{albumId}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<bool>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Favorite an album with a given Id. OAuth authentication required.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public bool FavoriteAlbum(string albumId)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"album/{albumId}/favorite";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<string>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output.Data.Equals("favorited", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        ///     Get information about a specific album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<Album> GetAlbum(string albumId)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"album/{albumId}";
            
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<Album>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Get information about an image in an album.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="albumId">The album id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<Image> GetAlbumImage(string imageId, string albumId)
        {
            if (string.IsNullOrWhiteSpace(imageId))
                throw new ArgumentNullException(nameof(imageId));

            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"album/{albumId}/image/{imageId}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<Image>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Return all of the images in the album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<IEnumerable<Image>> GetAlbumImages(string albumId)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"album/{albumId}/images";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<IEnumerable<Image>>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Takes a list of imageIds and removes from the album. For anonymous albums, {albumId} should be the
        ///     deletehash that is returned at creation.
        /// </summary>
        /// <param name="albumId">The id or deletehash of the album.</param>
        /// <param name="imageIds">The imageIds that you want to be removed from the album.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<bool> RemoveAlbumImages(string albumId, IEnumerable<string> imageIds)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            if (imageIds == null)
                throw new ArgumentNullException(nameof(imageIds));

            var url = $"album/{albumId}/remove_images";

            using (var request = AlbumRequestBuilder.RemoveAlbumImagesRequest(url, imageIds))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<bool>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Sets the images for an album, removes all other images and only uses the images in this request. For anonymous
        ///     albums, {albumId} should be the deletehash that is returned at creation.
        /// </summary>
        /// <param name="albumId">The id or deletehash of the album.</param>
        /// <param name="imageIds">The imageIds that you want to be added to the album.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<bool> SetAlbumImages(string albumId, IEnumerable<string> imageIds)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            if (imageIds == null)
                throw new ArgumentNullException(nameof(imageIds));

            var url = $"album/{albumId}";

            using (var request = AlbumRequestBuilder.SetAlbumImagesRequest(url, imageIds))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<bool>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Update the information of an album. For anonymous albums, {albumId} should be the deletehash that is returned at
        ///     creation.
        /// </summary>
        /// <param name="albumId">The id or deletehash of the album.</param>
        /// <param name="title">The title of the album.</param>
        /// <param name="description">The description of the album.</param>
        /// <param name="privacy">Sets the privacy level of the album.</param>
        /// <param name="layout">Sets the layout to display the album.</param>
        /// <param name="coverId">The Id of an image that you want to be the cover of the album.</param>
        /// <param name="imageIds">The imageIds that you want to be included in the album.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<bool> UpdateAlbum(string albumId, string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null, string coverId = null,
            IEnumerable<string> imageIds = null)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"album/{albumId}";

            using (var request = AlbumRequestBuilder.UpdateAlbumRequest(url, title, description,
                privacy, layout, coverId, imageIds))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<bool>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }
    }
}