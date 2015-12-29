using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Topic related actions.
    /// </summary>
    public interface ITopicEndpoint
    {
        /// <summary>
        ///     Get the list of default topics.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ITopic>> GetDefaultTopicsAsync();

        /// <summary>
        ///     View a single item in a gallery topic.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="topicId">
        ///     The ID or URL-formatted name of the topic. If using a topic's name, replace its spaces with
        ///     underscores (Mother's_Day).
        /// </param>
        /// <returns></returns>
        Task<IGalleryItem> GetGalleryTopicItemAsync(string galleryItemId, string topicId);

        /// <summary>
        ///     View gallery items for a topic.
        /// </summary>
        /// <param name="topicId">
        ///     The ID or URL-formatted name of the topic. If using a topic's name, replace its spaces with
        ///     underscores (Mother's_Day).
        /// </param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetGalleryTopicItemsAsync(string topicId,
            CustomGallerySortOrder? sort = CustomGallerySortOrder.Viral, TimeWindow? window = TimeWindow.Week,
            int? page = null);
    }
}