using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Gallery related actions.
    /// </summary>
    public partial class GalleryEndpoint : EndpointBase, IGalleryEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the GalleryEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public GalleryEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the GalleryEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal GalleryEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal GalleryRequestBuilder RequestBuilder { get; } = new GalleryRequestBuilder();

        /// <summary>
        ///     Returns the images in the gallery.
        /// </summary>
        /// <param name="section">The gallery section. Default: Hot</param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <param name="showViral">Show or hide viral images from the 'user' section. Default: true</param>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetGalleryAsync(GallerySection? section = GallerySection.Hot,
            GallerySortOrder? sort = GallerySortOrder.Viral, TimeWindow? window = TimeWindow.Day, int? page = null,
            bool? showViral = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns a random set of gallery images.
        /// </summary>
        /// <param name="page">	A page of random gallery images, from 0-50. Pages are regenerated every hour.</param>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetRandomGalleryItemsAsync(int? page = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Share an Album or Image to the Gallery. OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="title">The title of the image. This is required.</param>
        /// <param name="topic">The topic name.</param>
        /// <param name="bypassTerms">
        ///     If the user has not accepted the terms yet, this endpoint will return an error. To by-pass
        ///     the terms in general simply set this value to true.
        /// </param>
        /// <param name="mature">If the post is mature, set this value to true.</param>
        /// <returns></returns>
        public async Task<bool> PublishToGalleryAsync(string galleryItemId, string title, string topic = null,
            bool? bypassTerms = null,
            bool? mature = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Remove an item from the gallery. OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        public async Task<bool> RemoveFromGalleryAsync(string galleryItemId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Report an item in the gallery.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="reason">A reason why content is inappropriate.</param>
        /// <returns></returns>
        public async Task<bool> ReportGalleryItemAsync(string galleryItemId, ReportReason reason)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Search the gallery with a given query string.
        /// </summary>
        /// <param name="allWords">Search for all of these words (and).</param>
        /// <param name="anyWords">Search for any of these words (or).</param>
        /// <param name="exactWords">Search for exactly this word or phrase.</param>
        /// <param name="excludeWords">Exclude results matching this.</param>
        /// <param name="fileType">Show results for a specific file type.</param>
        /// <param name="imageSize">Show results for a specific image size.</param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Time</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> SearchGalleryAdvancedAsync(string allWords = null,
            string anyWords = null, string exactWords = null,
            string excludeWords = null, ImageFileType? fileType = null, ImageSize? imageSize = null,
            GallerySortOrder? sort = GallerySortOrder.Time, TimeWindow? window = TimeWindow.All, int? page = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Search the gallery with a given query string.
        /// </summary>
        /// <param name="query">
        ///     Query string to search by. This parameter also supports boolean operators (AND, OR, NOT) and
        ///     indices (tag: user: title: ext: subreddit: album: meme:). An example compound query would be 'title: cats AND dogs
        ///     ext: gif'
        /// </param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Time</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> SearchGalleryAsync(string query, GallerySortOrder? sort,
            TimeWindow? window, int? page = null)
        {
            throw new NotImplementedException();
        }
    }
}