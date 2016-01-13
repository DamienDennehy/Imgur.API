﻿namespace Imgur.API.Models
{
    /// <summary>
    ///     A topic.
    /// </summary>
    public interface ITopic
    {
        /// <summary>
        ///     CSS class used on website to style the ephemeral topic.
        /// </summary>
        string Css { get; set; }

        /// <summary>
        ///     Description of the topic.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     Whether it is an ephemeral (e.g. current events) topic.
        /// </summary>
        bool Ephemeral { get; set; }

        /// <summary>
        ///     ID of the topic.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        ///     Topic name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     The top image in this topic.
        /// </summary>
        IGalleryItem TopPost { get; set; }
    }
}