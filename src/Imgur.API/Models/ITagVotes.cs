using System.Collections.Generic;

namespace Imgur.API.Models
{
    /// <summary>
    ///     Tag vote data.
    /// </summary>
    public interface ITagVotes
    {
        /// <summary>
        ///     The list of tags.
        /// </summary>
        IEnumerable<ITagVote> Tags { get; set; }
    }
}