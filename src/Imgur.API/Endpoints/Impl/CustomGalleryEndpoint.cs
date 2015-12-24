using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Custom Gallery related actions.
    /// </summary>
    public class CustomGalleryEndpoint : EndpointBase, ICustomGalleryEndpoint
    {
        /// <summary>
        ///     Add tags to a user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tags">The tags that should be added.</param>
        /// <returns></returns>
        public Task<bool> AddCustomGalleryTagsAsync(IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Add tags to filter out.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tag">The tag that should be filtered out.</param>
        /// <returns></returns>
        public Task<bool> AddFilteredOutGalleryTagAsync(string tag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     View images for current user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="sort">The order that the gallery should be sorted by.</param>
        /// <param name="window">The time period that should be used in filtering requests.</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null.</param>
        /// <returns></returns>
        public Task<ICustomGallery> GetCustomGalleryAsync(CustomGallerySortOrder sort = CustomGallerySortOrder.Viral, Window window = Window.Week,
            int? page = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     View a single item in a user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="id">The gallery item id.</param>
        /// <returns></returns>
        public Task<IGalleryItem> GetCustomGalleryItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Retrieve user's filtered out gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="sort">The order that the gallery should be sorted by.</param>
        /// <param name="window">The time period that should be used in filtering requests.</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null.</param>
        /// <returns></returns>
        public Task<ICustomGallery> GetFilteredOutGalleryAsync(CustomGallerySortOrder sort = CustomGallerySortOrder.Viral, Window window = Window.Week,
            int? page = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Remove tags from a custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tags">The tags that should be removed.</param>
        /// <returns></returns>
        public Task<bool> RemoveCustomGalleryTagsAsync(IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Remove a filtered out tag.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tag">The tag that should be removed.</param>
        /// <returns></returns>
        public Task<bool> RemoveFilteredOutGalleryTagAsync(string tag)
        {
            throw new NotImplementedException();
        }
    }
}
