namespace Imgur.API.Models
{
    /// <summary>
    ///     The base model for a topic.
    /// </summary>
    public interface ITopic
    {
        /// <summary>
        ///     ID of the topic.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        ///     Topic name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Description of the topic.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     CSS class used on website to style the ephemeral topic.
        /// </summary>
        string Css { get; set; }

        /// <summary>
        ///     Whether it is an ephemeral (e.g. current events) topic.
        /// </summary>
        string Ephemeral { get; set; }
    }
}