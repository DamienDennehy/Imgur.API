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
        internal AlbumRequestBuilder AlbumRequestBuilder { get; } = new AlbumRequestBuilder();

        /// <summary>
        ///     Delete an Album with a given id. OAuth authentication required.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Task<bool> DeleteAlbumAsync(string albumId, string username = "me")
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/album/{albumId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Delete, url))
            {
                var deleted = SendRequestAsync<bool>(request);
                return deleted;
            }
        }

        /// <summary>
        ///     Get additional information about an album, this works the same as the Album Endpoint.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Task<Album> GetAlbumAsync(string albumId, string username = "me")
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/album/{albumId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var album = SendRequestAsync<Album>(request);
                return album;
            }
        }

        /// <summary>
        ///     Return a list of all of the album IDs.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Task<int> GetAlbumCountAsync(string username = "me")
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/albums/count";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var count = SendRequestAsync<int>(request);
                return count;
            }
        }

        /// <summary>
        ///     Return a list of all of the album IDs.
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
        public Task<IEnumerable<string>> GetAlbumIdsAsync(string username = "me", int? page = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/albums/ids/{page}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var albums = SendRequestAsync<IEnumerable<string>>(request);
                return albums;
            }
        }

        /// <summary>
        ///     Get all the albums associated with the account.
        ///     Must be logged in as the user to see secret and hidden albums.
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
        public Task<IEnumerable<Album>> GetAlbumsAsync(string username = "me", int? page = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/albums/{page}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var albums = SendRequestAsync<IEnumerable<Album>>(request);
                return albums;
            }
        }
    }
}