using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Meme related actions.
    /// </summary>
    public interface IMemeGenEndpoint
    {
        /// <summary>
        ///     Get the list of default memes.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IImage>> GetDefaultMemesAsync();
    }
}