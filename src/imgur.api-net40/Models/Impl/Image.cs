using System;
using Imgur.API.Enums;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     An image.
    /// </summary>
    public class Image : IImage
    {
        /// <summary>
        ///     Is the image animated.
        /// </summary>
        public virtual bool Animated { get; set; }

        /// <summary>
        ///     Bandwidth consumed by the image in bytes.
        /// </summary>
        public virtual long Bandwidth { get; set; }

        /// <summary>
        ///     Utc timestamp of when the image was uploaded, converted from epoch time.
        /// </summary>
        [JsonConverter(typeof(EpochTimeConverter))]
        public virtual DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     OPTIONAL, the deletehash, if you're logged in as the image owner.
        /// </summary>
        public virtual string DeleteHash { get; set; }

        /// <summary>
        ///     Description of the image.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        ///     Indicates if the current user favorited the image. Defaults to false if not signed in.
        /// </summary>
        public virtual bool? Favorite { get; set; }

        /// <summary>
        ///     OPTIONAL, The .gifv link. Only available if the image is animated and type is 'image/gif'.
        /// </summary>
        public virtual string Gifv { get; set; }

        /// <summary>
        ///     The height of the image in pixels.
        /// </summary>
        public virtual int Height { get; set; }

        /// <summary>
        ///     The ID for the image.
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        ///     True if the image has been submitted to the gallery, false if otherwise.
        /// </summary>
        [JsonProperty("in_gallery")]
        public virtual bool InGallery { get; set; }

        /// <summary>
        ///     The direct link to the the image. (Note: if fetching an animated GIF that was over 20MB in original size, a .gif
        ///     thumbnail will be returned)
        /// </summary>
        public virtual string Link { get; set; }

        /// <summary>
        ///     OPTIONAL, Whether the image has a looping animation. Only available if the image is animated and type is
        ///     'image/gif'.
        /// </summary>
        public virtual bool Looping { get; set; }

        /// <summary>
        ///     OPTIONAL, The direct link to the .mp4. Only available if the image is animated and type is 'image/gif'.
        /// </summary>
        public virtual string Mp4 { get; set; }

        /// <summary>
        ///     OPTIONAL, The Content-Length of the .mp4. Only available if the image is animated and type is 'image/gif'. Note
        ///     that a zero value (0) is possible if the video has not yet been generated.
        /// </summary>
        [JsonProperty("mp4_size")]
        public virtual int? Mp4Size { get; set; }

        /// <summary>
        ///     OPTIONAL, the original filename, if you're logged in as the image owner.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Indicates if the image has been marked as nsfw or not. Defaults to null if information is not available.
        /// </summary>
        public virtual bool? Nsfw { get; set; }

        /// <summary>
        ///     If the image has been categorized then this will contain the section the image belongs in. (funny, cats,
        ///     adviceanimals, wtf, etc)
        /// </summary>
        public virtual string Section { get; set; }

        /// <summary>
        ///     The size of the image in bytes.
        /// </summary>
        public virtual int Size { get; set; }

        /// <summary>
        ///     The title of the image.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        ///     Image MIME type.
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        ///     The size of the image in bytes.
        /// </summary>
        public virtual int Views { get; set; }

        /// <summary>
        ///     The current user's vote on the album. null if not signed in, if the user hasn't voted on it, or if not submitted to
        ///     the gallery.
        /// </summary>
        public virtual VoteOption? Vote { get; set; }

        /// <summary>
        ///     The width of the image in pixels.
        /// </summary>
        public virtual int Width { get; set; }
    }
}