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
        ///     Create a comment for an item. OAuth authentication required.
        /// </summary>
        /// <param name="comment">The text of the comment.</param>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        public async Task<bool> CreateGalleryItemCommentAsync(string comment, string galleryItemId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Reply to a comment that has been created for an item. OAuth authentication required.
        /// </summary>
        /// <param name="comment">The text of the comment.</param>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="parentId">The comment id that you are replying to.</param>
        /// <returns></returns>
        public async Task<bool> CreateGalleryItemCommentReplyAsync(string comment, string galleryItemId, string parentId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Get information about a specific comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        public async Task<IComment> GetGalleryItemCommentAsync(string commentId, string galleryItemId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The number of comments on an item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        public async Task<int> GetGalleryItemCommentCountAsync(string galleryItemId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     List all of the IDs for the comments on an item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetGalleryItemCommentIdsAsync(string galleryItemId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Get all comments for a gallery item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="sort">The order that comments should be sorted by.</param>
        /// <returns></returns>
        public async Task<IEnumerable<IComment>> GetGalleryItemCommentsAsync(string galleryItemId,
            CommentSortOrder? sort)
        {
            throw new NotImplementedException();
        }
    }
}