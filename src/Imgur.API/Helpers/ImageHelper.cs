using System;
using System.Collections.Generic;
using System.Linq;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Newtonsoft.Json;

namespace Imgur.API.Helpers
{
    /// <summary>
    ///     Helper methods for image endpoints.
    /// </summary>
    internal class ImageHelper
    {
        /// <summary>
        ///     Converts json objects to GalleryItems.
        /// </summary>
        /// <param name="galleryObjects"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<IGalleryItem> ConvertToGalleryItems(IEnumerable<object> galleryObjects)
        {
            var list = new List<IGalleryItem>();

            foreach (var jsonString in galleryObjects.Select(item => item.ToString()))
            {
                if (jsonString.Replace(" ", "").Contains("is_album\":true"))
                {
                    var album = JsonConvert.DeserializeObject<GalleryAlbum>(jsonString);
                    list.Add(album);
                }
                else
                {
                    var image = JsonConvert.DeserializeObject<GalleryImage>(jsonString);
                    list.Add(image);
                }
            }

            return list;
        }
    }
}