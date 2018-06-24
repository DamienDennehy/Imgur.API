using Imgur.API.Models;
using System.Threading.Tasks;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Gets credit limit.
    /// </summary>
    public interface IRateLimitEndpoint : IEndpoint
    {
        /// <summary>
        ///     Gets remaining credits for the application.
        /// </summary>
        /// <returns></returns>
        Task<IRateLimit> GetRateLimitAsync();
    }
}