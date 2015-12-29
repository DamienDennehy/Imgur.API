using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Album related actions.
    /// </summary>
    public interface IAlbumEndpoint : IEndpoint
    {
        /// <summary>
        ///     Takes a list of imageIds to add to the album. For anonymous albums, {albumId} should be the
        ///     deletehash
        ///     that is returned at creation.
        /// </summary>
        /// <param name="albumId">The id or deletehash of the album.</param>
        /// <param name="imageIds">The imageIds that you want to be added to the album.</param>
        /// <returns></returns>
        Task<bool> AddAlbumImagesAsync(string albumId, IEnumerable<string> imageIds);

        /// <summary>
        ///     Create a new album.
        /// </summary>
        /// <param name="title">The title of the album.</param>
        /// <param name="description">The description of the album.</param>
        /// <param name="privacy">Sets the privacy level of the album.</param>
        /// <param name="layout">Sets the layout to display the album.</param>
        /// <param name="coverId">The Id of an image that you want to be the cover of the album.</param>
        /// <param name="imageIds">The imageIds that you want to be included in the album.</param>
        /// <returns></returns>
        Task<IAlbum> CreateAlbumAsync(string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null,
            string coverId = null, IEnumerable<string> imageIds = null);

        /// <summary>
        ///     Delete an album with a given Id. You are required to be logged in as the user to delete the album. For anonymous
        ///     albums, {albumId} should be the deletehash that is returned at creation.
        /// </summary>
        /// <param name="albumId">The id or deletehash of the album.</param>
        /// <returns></returns>
        Task<bool> DeleteAlbumAsync(string albumId);

        /// <summary>
        ///     Favorite an album with a given Id. OAuth authentication required.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        Task<bool> FavoriteAlbumAsync(string albumId);

        /// <summary>
        ///     Get information about a specific album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        Task<IAlbum> GetAlbumAsync(string albumId);

        /// <summary>
        ///     Get information about an image in an album.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        Task<IImage> GetAlbumImageAsync(string imageId, string albumId);

        /// <summary>
        ///     Return all of the images in the album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        Task<IEnumerable<IImage>> GetAlbumImagesAsync(string albumId);

        /// <summary>
        ///     Takes a list of imageIds and removes from the album. For anonymous albums, {albumId} should be the
        ///     deletehash that is returned at creation.
        /// </summary>
        /// <param name="albumId">The id or deletehash of the album.</param>
        /// <param name="imageIds">The imageIds that you want to be removed from the album.</param>
        /// <returns></returns>
        Task<bool> RemoveAlbumImagesAsync(string albumId, IEnumerable<string> imageIds);

        /// <summary>
        ///     Sets the images for an album, removes all other images and only uses the images in this request. For anonymous
        ///     albums, {albumId} should be the deletehash that is returned at creation.
        /// </summary>
        /// <param name="albumId">The id or deletehash of the album.</param>
        /// <param name="imageIds">The imageIds that you want to be added to the album.</param>
        /// <returns></returns>
        Task<bool> SetAlbumImagesAsync(string albumId, IEnumerable<string> imageIds);

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
        /// <returns></returns>
        Task<bool> UpdateAlbumAsync(string albumId, string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null,
            string coverId = null, IEnumerable<string> imageIds = null);
    }
}