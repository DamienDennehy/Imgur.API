namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A topic.
    /// </summary>
    public class Topic : ITopic
    {
        /// <summary>
        ///     CSS class used on website to style the ephemeral topic.
        /// </summary>
        public string Css { get; set; }

        /// <summary>
        ///     Description of the topic.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Whether it is an ephemeral (e.g. current events) topic.
        /// </summary>
        public bool Ephemeral { get; set; }

        /// <summary>
        ///     ID of the topic.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Topic name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The top image in this topic.
        /// </summary>
        public IGalleryItem TopPost { get; set; }
    }
}