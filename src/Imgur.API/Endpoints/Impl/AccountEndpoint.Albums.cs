using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Exceptions;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        private const string GetAlbumsUrl = "account/{0}/albums/{1}";
        private const string GetAlbumUrl = "account/{0}/album/{1}";
        private const string GetAlbumIdsUrl = "account/{0}/albums/ids/{1}";
        private const string GetAlbumCountUrl = "account/{0}/albums/count";
        private const string DeleteAlbumUrl = "account/{0}/album/{1}";

        /// <summary>
        ///     Get all the albums associated with the account.
        ///     Must be logged in as the user to see secret and hidden albums.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IAlbum>> GetAlbumsAsync(string username = "me", int? page = null)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAlbumsUrl);
            //endpointUrl = string.Format(endpointUrl, username, page);

            //return await MakeEndpointRequestAsync<IEnumerable<Album>>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Get additional information about an album, this works the same as the Album Endpoint.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IAlbum> GetAlbumAsync(string id, string username = "me")
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(id))
            //    throw new ArgumentNullException(nameof(id));

            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAlbumUrl);
            //endpointUrl = string.Format(endpointUrl, username, id);
            //var album = await MakeEndpointRequestAsync<Album>(HttpMethod.Get, endpointUrl);
            //return album;
        }

        /// <summary>
        ///     Return an array of all of the album IDs.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetAlbumIdsAsync(string username = "me", int? page = null)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAlbumIdsUrl);
            //endpointUrl = string.Format(endpointUrl, username, page);

            //return await MakeEndpointRequestAsync<IEnumerable<string>>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Return an array of all of the album IDs.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<int> GetAlbumCountAsync(string username = "me")
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAlbumCountUrl);
            //endpointUrl = string.Format(endpointUrl, username);

            //return await MakeEndpointRequestAsync<int>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Delete an Album with a given id.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> DeleteAlbumAsync(string id)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(id))
            //    throw new ArgumentNullException(nameof(id));

            //if (ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), DeleteAlbumUrl);
            //endpointUrl = string.Format(endpointUrl, "me", id);

            //return await MakeEndpointRequestAsync<bool>(HttpMethod.Delete, endpointUrl);
        }
    }
}