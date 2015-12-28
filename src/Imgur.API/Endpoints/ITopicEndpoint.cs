using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Topic related actions.
    /// </summary>
    public interface ITopicEndpoint
    {
        /// <summary>
        /// Get the list of default topics.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetDefaultTopicsAsync();

        /// <summary>
        /// View gallery items for a topic.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetGalleryTopicItemsAsync();

        /// <summary>
        /// View a single item in a gallery topic.
        /// </summary>
        /// <param name="id">The gallery item id,</param>
        /// <param name="topicId">The topic id.</param>
        /// <returns></returns>
        Task<IGalleryItem> GetGalleryTopicItemAsync(string id, string topicId);
    }
}
