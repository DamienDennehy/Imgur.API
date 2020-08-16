using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints
{
    /// <summary>
    /// Album Endpoint.
    /// </summary>
    public class AlbumEndpoint : EndpointBase, IAlbumEndpoint
    {
        /// <summary>
        /// Declares a new instance of the endpoint.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="httpClient"></param>
        public AlbumEndpoint(IApiClient apiClient, HttpClient httpClient) : base(
            apiClient, httpClient)
        {
        }

        /// <summary>
        /// Get information about an album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IAlbum> GetAlbumAsync(string albumId,
                                          CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(albumId))
            {
                throw new ArgumentNullException(nameof(albumId));
            }

            return GetAlbumInternalAsync(albumId, cancellationToken);
        }

        private async Task<IAlbum> GetAlbumInternalAsync(string albumId,
                                                         CancellationToken cancellationToken = default)
        {
            var url = $"album/{albumId}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var response = await SendRequestAsync<Album>(request,
                                                             cancellationToken).ConfigureAwait(false);
                return response;
            }
        }

        /// <summary>
        /// Gets information about an image in an album.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="albumId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IImage> GetAlbumImageAsync(string imageId,
                                               string albumId,
                                               CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the images in an album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<IImage>> GetAlbumImagesAsync(string albumId,
                                                             CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new album.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="privacy"></param>
        /// <param name="layout"></param>
        /// <param name="coverId"></param>
        /// <param name="imageIds"></param>
        /// <param name="deleteHashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IAlbum> CreateAlbumAsync(string title = null,
                                             string description = null,
                                             string privacy = null,
                                             string layout = null,
                                             string coverId = null,
                                             IEnumerable<string> imageIds = null,
                                             IEnumerable<string> deleteHashes = null,
                                             CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the Album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="privacy"></param>
        /// <param name="layout"></param>
        /// <param name="coverId"></param>
        /// <param name="imageIds"></param>
        /// <param name="deleteHashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> UpdateAlbumAsync(string albumId,
                                           string title = null,
                                           string description = null,
                                           string privacy = null,
                                           string layout = null,
                                           string coverId = null,
                                           IEnumerable<string> imageIds = null,
                                           IEnumerable<string> deleteHashes = null,
                                           CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> DeleteAlbumAsync(string albumId,
                                           CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Favorites an Album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> FavoriteAlbumAsync(string albumId,
                                               CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the images for an album, removes all other images and only uses the images in this request.
        /// For anonymous albums, {albumId} should be the deletehash that is returned at creation.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="imageIds"></param>
        /// <param name="deleteHashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> SetAlbumImagesAsync(string albumId,
                                              IEnumerable<string> imageIds = null,
                                              IEnumerable<string> deleteHashes = null,
                                              CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the images to an album. You must specify ids[] or deletehashes[] in order to add an image to an album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="imageIds"></param>
        /// <param name="deleteHashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> AddAlbumImagesAsync(string albumId,
                                              IEnumerable<string> imageIds = null,
                                              IEnumerable<string> deleteHashes = null,
                                              CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Takes a list of imageIds and removes from the album. For anonymous albums, {albumId} should be the
        /// deletehash that is returned at creation.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="imageIds"></param>
        /// <param name="deleteHashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> RemoveAlbumImagesAsync(string albumId,
                                                 IEnumerable<string> imageIds = null,
                                                 IEnumerable<string> deleteHashes = null,
                                                 CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
