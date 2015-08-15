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
        ///     Get information about an image.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <returns></returns>
        Task<IImage> GetImageAsync(string id);

        /// <summary>
        ///     Upload a new image using a binary file.
        /// </summary>
        /// <param name="image">A binary file.</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <returns></returns>
        Task<bool> UploadImageBinaryAsync(byte[] image, string albumId, string title, string description);

        /// <summary>
        ///     Upload a new image using base64 data.
        /// </summary>
        /// <param name="image">Base64 data.</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <returns></returns>
        Task<bool> UploadImageBase64Async(string image, string albumId, string title, string description);

        /// <summary>
        ///     Upload a new image using a URL.
        /// </summary>
        /// <param name="image">The URL for the image.</param>
        /// <param name="albumId">
        ///     The id of the album you want to add the image to. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <returns></returns>
        Task<bool> UploadImageUrlAsync(string image, string albumId, string title, string description);

        /// <summary>
        ///     Deletes an image. For an anonymous image, {id} must be the image's deletehash.
        ///     If the image belongs to your account then passing the ID of the image is sufficient.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <returns></returns>
        Task<bool> DeleteImageAsync(string id);

        /// <summary>
        ///     Updates the title or description of an image.
        ///     You can only update an image you own and is associated with your account.
        ///     For an anonymous image, {id} must be the image's deletehash.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <returns></returns>
        Task<bool> UpdateImageAsync(string id, string title, string description);

        /// <summary>
        ///     Favorite an image with the given ID. The user is required to be logged in to favorite the image.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <returns></returns>
        Task<bool> FavoriteImageAsync(string id);
    }
}