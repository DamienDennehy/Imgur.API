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
        public async Task<bool> AddAlbumImagesAsync(string albumId, IEnumerable<string> imageIds)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            if (imageIds == null)
                throw new ArgumentNullException(nameof(imageIds));

            var url = $"album/{albumId}/add";

            using (var request = AlbumRequestBuilder.AddAlbumImagesRequest(url, imageIds))
            {
                var added = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return added;
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
        public async Task<IAlbum> CreateAlbumAsync(string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null, string coverId = null,
            IEnumerable<string> imageIds = null)
        {
            var url = "album";

            using (var request = AlbumRequestBuilder.CreateAlbumRequest(url, title, description,
                privacy, layout, coverId, imageIds))
            {
                var album = await SendRequestAsync<Album>(request).ConfigureAwait(false);
                return album;
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
        public async Task<bool> DeleteAlbumAsync(string albumId)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"album/{albumId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Delete, url))
            {
                var deleted = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return deleted;
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
        public async Task<bool> FavoriteAlbumAsync(string albumId)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"album/{albumId}/favorite";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Post, url))
            {
                var imgurResult = await SendRequestAsync<string>(request).ConfigureAwait(false);
                return imgurResult.Equals("favorited", StringComparison.OrdinalIgnoreCase);
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
        public async Task<IAlbum> GetAlbumAsync(string albumId)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"album/{albumId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var album = await SendRequestAsync<Album>(request).ConfigureAwait(false);
                return album;
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
        public async Task<IImage> GetAlbumImageAsync(string imageId, string albumId)
        {
            if (string.IsNullOrWhiteSpace(imageId))
                throw new ArgumentNullException(nameof(imageId));

            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"album/{albumId}/image/{imageId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var returnImage = await SendRequestAsync<Image>(request).ConfigureAwait(false);
                return returnImage;
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
        public async Task<IEnumerable<IImage>> GetAlbumImagesAsync(string albumId)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"album/{albumId}/images";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var images = await SendRequestAsync<IEnumerable<Image>>(request).ConfigureAwait(false);
                return images;
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
        public async Task<bool> RemoveAlbumImagesAsync(string albumId, IEnumerable<string> imageIds)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            if (imageIds == null)
                throw new ArgumentNullException(nameof(imageIds));

            var url = $"album/{albumId}/remove_images";

            using (var request = AlbumRequestBuilder.RemoveAlbumImagesRequest(url, imageIds))
            {
                var removed = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return removed;
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
        public async Task<bool> SetAlbumImagesAsync(string albumId, IEnumerable<string> imageIds)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            if (imageIds == null)
                throw new ArgumentNullException(nameof(imageIds));

            var url = $"album/{albumId}";

            using (var request = AlbumRequestBuilder.SetAlbumImagesRequest(url, imageIds))
            {
                var set = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return set;
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
        public async Task<bool> UpdateAlbumAsync(string albumId, string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null, string coverId = null,
            IEnumerable<string> imageIds = null)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"album/{albumId}";

            using (var request = AlbumRequestBuilder.UpdateAlbumRequest(url, title, description,
                privacy, layout, coverId, imageIds))
            {
                var updated = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return updated;
            }
        }
    }
}