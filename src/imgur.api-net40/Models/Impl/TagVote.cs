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
        public virtual string Author { get; set; }

        /// <summary>
        ///     Number of downvotes.
        /// </summary>
        public virtual int Downs { get; set; }

        /// <summary>
        ///     Name of the tag.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Number of upvotes.
        /// </summary>
        public virtual int Ups { get; set; }
    }
}