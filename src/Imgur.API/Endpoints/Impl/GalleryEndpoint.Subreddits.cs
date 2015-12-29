using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    public partial class GalleryEndpoint
    {
        /// <summary>
        ///     View gallery images for a subreddit.
        /// </summary>
        /// <param name="subreddit">A valid subreddit name. Example: pics, gaming</param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Time</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetSubredditGalleriesAsync(string subreddit,
            SubredditGallerySortOrder? sort, Window? window, int? page = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     View a single image in the subreddit.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="subreddit">A valid subreddit name. Example: pics, gaming</param>
        /// <returns></returns>
        public async Task<IGalleryImage> GetSubredditImageAsync(string imageId, string subreddit)
        {
            throw new NotImplementedException();
        }
    }
}