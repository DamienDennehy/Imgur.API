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
        ///     View tags for a gallery item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ITagVote>> GetGalleryItemTagsAsync(string galleryItemId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///     View images for a gallery tag.
        /// </summary>
        /// <param name="tag">The name of the tag.</param>
        /// <param name="sort">The order that the images in the gallery tag should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        public async Task<ITag> GetGalleryTagAsync(string tag, GalleryTagSortOrder? sort, Window? window,
            int? page = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     View a single image in a gallery tag.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="tag">The name of the tag.</param>
        /// <returns></returns>
        public async Task<IGalleryImage> GetGalleryTagImageAsync(string imageId, string tag)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///     Vote for a tag. Send the same value again to undo a vote. OAuth authentication required.
        /// </summary>
        /// <param name="tag">Name of the tag (implicitly created, if doesn't exist).</param>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="vote">The vote.</param>
        /// <returns></returns>
        public async Task<bool> VoteGalleryTagAsync(string tag, string galleryItemId, Vote vote)
        {
            throw new NotImplementedException();
        }
    }
}