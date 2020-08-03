using System.IO;
using System.Threading.Tasks;

namespace Imgur.API.Endpoints
{
    interface IImageEndpoint
    {
        /// <summary>
        /// Get information about an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        Task GetImageAsync(string imageId);

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="albumId"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task UploadImageAsync(Stream image, string albumId = null, string name = null, string title = null, string description = null);

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="albumId"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task UploadImageAsync(string image, string albumId = null, string name = null, string title = null, string description = null);

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="video"></param>
        /// <param name="albumId"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="disableAudio"></param>
        /// <returns></returns>
        Task UploadVideoAsync(Stream video, string albumId = null, string type = null, string name = null, string title = null, string description = null, bool disableAudio = false);

        /// <summary>
        /// Deletes an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        Task DeleteImageAsync(string imageId);

        /// <summary>
        /// Updates the title or description of an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task UpdateImageAsync(string imageId, string title = null, string description = null);

        /// <summary>
        /// Favorite an image with the given ID. The user is required to be logged in to favorite the image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        Task FavoriteImageAsync(string imageId);
    }
}
