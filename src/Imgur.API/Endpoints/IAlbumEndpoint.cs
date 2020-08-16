using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    /// Album Endpoint.
    /// </summary>
    public interface IAlbumEndpoint : IEndpoint
    {
        /// <summary>
        /// Get information about an album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAlbum> GetAlbumAsync(string albumId,
                                   CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets information about an image in an album.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="albumId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IImage> GetAlbumImageAsync(string imageId,
                                        string albumId,
                                        CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the images in an album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<IImage>> GetAlbumImagesAsync(string albumId,
                                                      CancellationToken cancellationToken = default);

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
        Task<IAlbum> CreateAlbumAsync(string title = null,
                                      string description = null,
                                      string privacy = null,
                                      string layout = null,
                                      string coverId = null,
                                      IEnumerable<string> imageIds = null,
                                      IEnumerable<string> deleteHashes = null,
                                      CancellationToken cancellationToken = default);

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
        Task<bool> UpdateAlbumAsync(string albumId,
                                    string title = null,
                                    string description = null,
                                    string privacy = null,
                                    string layout = null,
                                    string coverId = null,
                                    IEnumerable<string> imageIds = null,
                                    IEnumerable<string> deleteHashes = null,
                                    CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAlbumAsync(string albumId,
                                    CancellationToken cancellationToken = default);

        /// <summary>
        /// Favorites an Album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> FavoriteAlbumAsync(string albumId,
                                        CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the images for an album, removes all other images and only uses the images in this request.
        /// For anonymous albums, {albumId} should be the deletehash that is returned at creation.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="imageIds"></param>
        /// <param name="deleteHashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SetAlbumImagesAsync(string albumId,
                                       IEnumerable<string> imageIds = null,
                                       IEnumerable<string> deleteHashes = null,
                                       CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the images to an album. You must specify ids[] or deletehashes[] in order to add an image to an album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="imageIds"></param>
        /// <param name="deleteHashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AddAlbumImagesAsync(string albumId,
                                       IEnumerable<string> imageIds = null,
                                       IEnumerable<string> deleteHashes = null,
                                       CancellationToken cancellationToken = default);

        /// <summary>
        /// Takes a list of imageIds and removes from the album. For anonymous albums, {albumId} should be the
        /// deletehash that is returned at creation.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="imageIds"></param>
        /// <param name="deleteHashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> RemoveAlbumImagesAsync(string albumId,
                                          IEnumerable<string> imageIds = null,
                                          IEnumerable<string> deleteHashes = null,
                                          CancellationToken cancellationToken = default);
    }
}
