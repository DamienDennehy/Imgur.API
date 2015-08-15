using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Gets credit limit.
    /// </summary>
    public interface IRateLimitEndpoint : IEndpoint
    {
        /// <summary>
        ///     Get RateLimit.
        /// </summary>
        /// <returns></returns>
        Task<IRateLimit> GetRateLimitAsync();
    }
}