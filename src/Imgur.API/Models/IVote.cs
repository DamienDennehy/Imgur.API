namespace Imgur.API.Models
{
    /// <summary>
    ///     The base model for a vote.
    /// </summary>
    public interface IVote
    {
        /// <summary>
        ///     Number of upvotes.
        /// </summary>
        int Ups { get; set; }

        /// <summary>
        ///     The number of downvotes.
        /// </summary>
        int Downs { get; set; }
    }
}