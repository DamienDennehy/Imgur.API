namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A vote.
    /// </summary>
    public class Vote : IVote
    {
        /// <summary>
        ///     The number of downvotes.
        /// </summary>
        public virtual int Downs { get; set; }

        /// <summary>
        ///     Number of upvotes.
        /// </summary>
        public virtual int Ups { get; set; }
    }
}