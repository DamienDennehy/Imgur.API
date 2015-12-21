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
        ///     Takes parameter, ids[], as an array of ids to add to the album. For anonymous albums, {album} should be the
        ///     deletehash
        ///     that is returned at creation.
        /// </summary>
        /// <param name="album">The id or deletehash of the album.</param>
        /// <param name="ids">The image ids that you want to be added to the album.</param>
        /// <returns></returns>
        Task<bool> AddAlbumImagesAsync(string album, IEnumerable<string> ids);

        /// <summary>
        ///     Create a new album.
        /// </summary>
        /// <param name="title">The title of the album.</param>
        /// <param name="description">The description of the album.</param>
        /// <param name="privacy">Sets the privacy level of the album.</param>
        /// <param name="layout">Sets the layout to display the album.</param>
        /// <param name="cover">The Id of an image that you want to be the cover of the album.</param>
        /// <param name="ids">The image ids that you want to be included in the album.</param>
        /// <returns></returns>
        Task<IAlbum> CreateAlbumAsync(string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null,
            string cover = null, IEnumerable<string> ids = null);

        /// <summary>
        ///     Delete an album with a given Id. You are required to be logged in as the user to delete the album. For anonymous
        ///     albums, {album} should be the deletehash that is returned at creation.
        /// </summary>
        /// <param name="album">The id or deletehash of the album.</param>
        /// <returns></returns>
        Task<bool> DeleteAlbumAsync(string album);

        /// <summary>
        ///     Favorite an album with a given Id. OAuth authentication required.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns></returns>
        Task<bool> FavoriteAlbumAsync(string id);

        /// <summary>
        ///     Get information about a specific album.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns></returns>
        Task<IAlbum> GetAlbumAsync(string id);

        /// <summary>
        ///     Get information about an image in an album.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <param name="image">The image id.</param>
        /// <returns></returns>
        Task<IImage> GetAlbumImageAsync(string id, string image);

        /// <summary>
        ///     Return all of the images in the album.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns></returns>
        Task<IEnumerable<IImage>> GetAlbumImagesAsync(string id);

        /// <summary>
        ///     Takes parameter, ids[], as an array of ids and removes from the album. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </summary>
        /// <param name="album">The id or deletehash of the album.</param>
        /// <param name="ids">The image ids that you want to be removed from the album.</param>
        /// <returns></returns>
        Task<bool> RemoveAlbumImagesAsync(string album, IEnumerable<string> ids);

        /// <summary>
        ///     Sets the images for an album, removes all other images and only uses the images in this request. For anonymous
        ///     albums, {album} should be the deletehash that is returned at creation.
        /// </summary>
        /// <param name="album">The id or deletehash of the album.</param>
        /// <param name="ids">The image ids that you want to be added to the album.</param>
        /// <returns></returns>
        Task<bool> SetAlbumImagesAsync(string album, IEnumerable<string> ids);

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
        /// <returns></returns>
        Task<bool> UpdateAlbumAsync(string album, string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null,
            string cover = null, IEnumerable<string> ids = null);
    }
}