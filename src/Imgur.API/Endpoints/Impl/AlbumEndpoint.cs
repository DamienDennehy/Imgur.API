using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Enums;
using Imgur.API.Exceptions;
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
        /// <param name="apiClient"></param>
        public AlbumEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the AlbumEndpoint class.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="httpClient"></param>
        internal AlbumEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal AlbumRequestBuilder RequestBuilder { get; } = new AlbumRequestBuilder();

        /// <summary>
        ///     Takes parameter, ids[], as an array of ids to add to the album. For anonymous albums, {album} should be the
        ///     deletehash
        ///     that is returned at creation.
        /// </summary>
        /// <param name="album">The id or deletehash of the album.</param>
        /// <param name="ids">The image ids that you want to be added to the album.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> AddAlbumImagesAsync(string album, IEnumerable<string> ids)
        {
            if (string.IsNullOrEmpty(album))
                throw new ArgumentNullException(nameof(album));

            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var url = $"album/{album}/add";

            using (var request = RequestBuilder.AddAlbumImagesRequest(url, ids))
            {
                var added = await SendRequestAsync<bool>(request);
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
        /// <param name="cover">The Id of an image that you want to be the cover of the album.</param>
        /// <param name="ids">The image ids that you want to be included in the album.</param>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IAlbum> CreateAlbumAsync(string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null, string cover = null,
            IEnumerable<string> ids = null)
        {
            var url = "album";

            using (var request = RequestBuilder.CreateAlbumRequest(url, title, description, privacy, layout, cover, ids)
                )
            {
                var album = await SendRequestAsync<Album>(request);
                return album;
            }
        }

        /// <summary>
        ///     Delete an album with a given Id. You are required to be logged in as the user to delete the album. For anonymous
        ///     albums, {album} should be the deletehash that is returned at creation.
        /// </summary>
        /// <param name="album">The id or deletehash of the album.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> DeleteAlbumAsync(string album)
        {
            if (string.IsNullOrEmpty(album))
                throw new ArgumentNullException(nameof(album));

            var url = $"album/{album}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Delete, url))
            {
                var deleted = await SendRequestAsync<bool>(request);
                return deleted;
            }
        }

        /// <summary>
        ///     Favorite an album with a given Id. The user is required to be logged in to favorite the album.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> FavoriteAlbumAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var url = $"album/{id}/favorite";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Post, url))
            {
                //The structure of the response of favoriting an album
                //varies on if Imgur or Mashape Authentication is used
                if (ApiClient is IImgurClient)
                {
                    var imgurResult = await SendRequestAsync<string>(request);
                    return imgurResult.Equals("favorited", StringComparison.OrdinalIgnoreCase);
                }

                //If Mashape Authentication is used, the favorite is returned as an exception
                try
                {
                    await SendRequestAsync<string>(request);
                }
                catch (ImgurException imgurException)
                {
                    return imgurException.Message.Equals("f", StringComparison.OrdinalIgnoreCase);
                }
            }

            return false;
        }

        /// <summary>
        ///     Get information about a specific album.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IAlbum> GetAlbumAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var url = $"album/{id}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var album = await SendRequestAsync<Album>(request);
                return album;
            }
        }

        /// <summary>
        ///     Get information about an image in an album.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <param name="image">The image id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IImage> GetAlbumImageAsync(string id, string image)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrEmpty(image))
                throw new ArgumentNullException(nameof(image));

            var url = $"album/{id}/image/{image}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var returnImage = await SendRequestAsync<Image>(request);
                return returnImage;
            }
        }

        /// <summary>
        ///     Return all of the images in the album.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IImage>> GetAlbumImagesAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var url = $"album/{id}/images";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var images = await SendRequestAsync<IEnumerable<Image>>(request);
                return images;
            }
        }

        /// <summary>
        ///     Takes parameter, ids[], as an array of ids and removes from the album. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </summary>
        /// <param name="album">The id or deletehash of the album.</param>
        /// <param name="ids">The image ids that you want to be removed from the album.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> RemoveAlbumImagesAsync(string album, IEnumerable<string> ids)
        {
            if (string.IsNullOrEmpty(album))
                throw new ArgumentNullException(nameof(album));

            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var url = $"album/{album}/remove_images";

            using (var request = RequestBuilder.RemoveAlbumImagesRequest(url, ids))
            {
                var removed = await SendRequestAsync<bool>(request);
                return removed;
            }
        }

        /// <summary>
        ///     Sets the images for an album, removes all other images and only uses the images in this request. For anonymous
        ///     albums, {album} should be the deletehash that is returned at creation.
        /// </summary>
        /// <param name="album">The id or deletehash of the album.</param>
        /// <param name="ids">The image ids that you want to be added to the album.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> SetAlbumImagesAsync(string album, IEnumerable<string> ids)
        {
            if (string.IsNullOrEmpty(album))
                throw new ArgumentNullException(nameof(album));

            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var url = $"album/{album}";

            using (var request = RequestBuilder.SetAlbumImagesRequest(url, ids))
            {
                var set = await SendRequestAsync<bool>(request);
                return set;
            }
        }

        /// <summary>
        ///     Update the information of an album. For anonymous albums, {album} should be the deletehash that is returned at
        ///     creation.
        /// </summary>
        /// <param name="album">The id or deletehash of the album.</param>
        /// <param name="title">The title of the album.</param>
        /// <param name="description">The description of the album.</param>
        /// <param name="privacy">Sets the privacy level of the album.</param>
        /// <param name="layout">Sets the layout to display the album.</param>
        /// <param name="cover">The Id of an image that you want to be the cover of the album.</param>
        /// <param name="ids">The image ids that you want to be included in the album.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> UpdateAlbumAsync(string album, string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null, string cover = null,
            IEnumerable<string> ids = null)
        {
            if (string.IsNullOrEmpty(album))
                throw new ArgumentNullException(nameof(album));

            var url = $"album/{album}";

            using (var request = RequestBuilder.UpdateAlbumRequest(url, title, description, privacy, layout, cover, ids)
                )
            {
                var updated = await SendRequestAsync<bool>(request);
                return updated;
            }
        }
    }
}