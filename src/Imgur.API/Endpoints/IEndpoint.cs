using Imgur.API.Authentication;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Endpoint definition.
    /// </summary>
    public interface IEndpoint
    {
        /// <summary>
        ///     Interface for all Api authentication types.
        /// </summary>
        IApiAuthentication ApiAuthentication { get; }

        /// <summary>
        ///     Gets the endpoint url based on the authentication type.
        /// </summary>
        string GetEndpointBaseUrl();

        /// <summary>
        ///     Switch from one authentication type to another.
        /// </summary>
        /// <param name="apiAuthentication"></param>
        void SwitchAuthentication(IApiAuthentication apiAuthentication);
    }
}