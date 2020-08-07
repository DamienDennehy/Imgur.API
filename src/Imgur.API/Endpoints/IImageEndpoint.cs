using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Imgur.API.Endpoints
{
    public interface IImageEndpoint
    {
        /// <summary>
        /// Get information about an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task GetImageAsync(string imageId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="albumId"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UploadImageAsync(Stream image, string albumId = null, string name = null, string title = null, string description = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="albumId"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UploadImageAsync(string image, string albumId = null, string name = null, string title = null, string description = null, CancellationToken cancellationToken = default);


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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification = "<Pending>")]
        Task UploadVideoAsync(Stream video, string albumId = null, string type = null, string name = null, string title = null, string description = null, bool disableAudio = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteImageAsync(string imageId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the title or description of an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateImageAsync(string imageId, string title = null, string description = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Favorite an image with the given ID. The user is required to be logged in to favorite the image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task FavoriteImageAsync(string imageId, CancellationToken cancellationToken = default);
    }
}
