namespace Imgur.API.Models
{
    /// <summary>
    ///     A vote.
    /// </summary>
    public interface IVote
    {
        /// <summary>
        ///     The number of downvotes.
        /// </summary>
        int Downs { get; set; }

        /// <summary>
        ///     Number of upvotes.
        /// </summary>
        int Ups { get; set; }
    }
}