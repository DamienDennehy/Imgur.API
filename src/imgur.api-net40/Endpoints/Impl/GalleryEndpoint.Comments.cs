using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    public partial class GalleryEndpoint
    {
        internal CommentRequestBuilder CommentRequestBuilder { get; } = new CommentRequestBuilder();

        /// <summary>
        ///     Create a comment for an item. OAuth authentication required.
        /// </summary>
        /// <param name="comment">The text of the comment.</param>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public int CreateGalleryItemComment(string comment, string galleryItemId)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"gallery/{galleryItemId}/comment";

            using (var request = RequestBuilders.CommentRequestBuilder.CreateGalleryItemCommentRequest(url, comment))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<Comment>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output.Data.Id;
            }
        }

        /// <summary>
        ///     Reply to a comment that has been created for an item. OAuth authentication required.
        /// </summary>
        /// <param name="comment">The text of the comment.</param>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="parentId">The comment id that you are replying to.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public int CreateGalleryItemCommentReply(string comment, string galleryItemId,
            string parentId)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            if (string.IsNullOrWhiteSpace(parentId))
                throw new ArgumentNullException(nameof(parentId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"gallery/{galleryItemId}/comment/{parentId}";

            using (var request = RequestBuilders.CommentRequestBuilder.CreateGalleryItemCommentRequest(url, comment))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<Comment>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output.Data.Id;
            }
        }

        /// <summary>
        ///     Get information about a specific comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<Comment> GetGalleryItemComment(int commentId, string galleryItemId)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            var url = $"gallery/{galleryItemId}/comment/{commentId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<Comment>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     The number of comments on an item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<int> GetGalleryItemCommentCount(string galleryItemId)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            var url = $"gallery/{galleryItemId}/comments/count";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<int>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     List all of the IDs for the comments on an item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<IEnumerable<int>> GetGalleryItemCommentIds(string galleryItemId)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            var url = $"gallery/{galleryItemId}/comments/ids";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<IEnumerable<int>>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }

        /// <summary>
        ///     Get all comments for a gallery item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="sort">The order that comments should be sorted by.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public Basic<IEnumerable<Comment>> GetGalleryItemComments(string galleryItemId,
            CommentSortOrder? sort = CommentSortOrder.Best)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            sort = sort ?? CommentSortOrder.Best;

            var sortValue = $"{sort}".ToLower();
            var url = $"gallery/{galleryItemId}/comments/{sortValue}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<IEnumerable<Comment>>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }
    }
}