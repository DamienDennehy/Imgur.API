namespace Imgur.API.Models
{
    /// <summary>
    ///     The base model for a tag vote.
    /// </summary>
    public interface ITagVote
    {
        /// <summary>
        ///     Number of upvotes.
        /// </summary>
        int Ups { get; set; }

        /// <summary>
        ///     Number of downvotes.
        /// </summary>
        int Downs { get; set; }

        /// <summary>
        ///     Name of the tag.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Author of the tag.
        /// </summary>
        string Author { get; set; }
    }
}