namespace Imgur.API.Tests.FakeResponses
{
    public partial class GalleryEndpointResponses
    {
        public const string GetGalleryItemVotesAsync =
            "{\"data\":{\"ups\":11347,\"downs\":751},\"success\":true,\"status\":200}";

        public const string VoteGalleryItemAsync =
            "{\"data\":true,\"success\":true,\"status\":200}";
    }
}