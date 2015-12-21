using System.IO;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Image related actions.
    /// </summary>
    public interface IImageEndpoint : IEndpoint
    {
        /// <summary>
        ///     Deletes an image. For an anonymous image, {id} must be the image's deletehash.
        ///     If the image belongs to your account then passing the ID of the image is sufficient.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <returns></returns>
        Task<bool> DeleteImageAsync(string id);

        /// <summary>
        ///     Favorite an image with the given ID. OAuth authentication required.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <returns></returns>
        Task<bool> FavoriteImageAsync(string id);

        /// <summary>
        ///     Get information about an image.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <returns></returns>
        Task<IImage> GetImageAsync(string id);

        /// <summary>
        ///     Updates the title or description of an image.
        ///     You can only update an image you own and is associated with your account.
        ///     For an anonymous image, {id} must be the image's deletehash.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <returns></returns>
        Task<bool> UpdateImageAsync(string id, string title = null, string description = null);

        /// <summary>
        ///     Upload a new image using a binary file.
        /// </summary>
        /// <param name="image">A binary file.</param>
        /// <param name="album">
        ///     The id of the album you want to add the image to. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <returns></returns>
        Task<IImage> UploadImageBinaryAsync(byte[] image, string album = null, string title = null,
            string description = null);

        /// <summary>
        ///     Upload a new image using a stream.
        /// </summary>
        /// <param name="image">A stream.</param>
        /// <param name="album">
        ///     The id of the album you want to add the image to. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <returns></returns>
        Task<IImage> UploadImageStreamAsync(Stream image, string album = null, string title = null,
            string description = null);

        /// <summary>
        ///     Upload a new image using a URL.
        /// </summary>
        /// <param name="image">The URL for the image.</param>
        /// <param name="album">
        ///     The id of the album you want to add the image to. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <returns></returns>
        Task<IImage> UploadImageUrlAsync(string image, string album = null, string title = null,
            string description = null);
    }
}