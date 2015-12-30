namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A tag vote.
    /// </summary>
    public class TagVote : ITagVote
    {
        /// <summary>
        ///     Author of the tag.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     Number of downvotes.
        /// </summary>
        public int Downs { get; set; }

        /// <summary>
        ///     Name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Number of upvotes.
        /// </summary>
        public int Ups { get; set; }
    }
}