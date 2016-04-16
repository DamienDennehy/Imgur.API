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
        public virtual string Css { get; set; }

        /// <summary>
        ///     Description of the topic.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        ///     Whether it is an ephemeral (e.g. current events) topic.
        /// </summary>
        public virtual bool Ephemeral { get; set; }

        /// <summary>
        ///     ID of the topic.
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        ///     Topic name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     The top image in this topic.
        /// </summary>
        public virtual IGalleryItem TopPost { get; set; }

        /// <summary>
        ///     The current 'hero' image or album chosen by the Imgur community staff.
        /// </summary>
        public virtual IGalleryItem HeroImage { get; set; }

        /// <summary>
        ///     Whether the topic's HeroImage should be used as the overall hero image.
        /// </summary>
        public virtual bool IsHero { get; set; }
    }
}