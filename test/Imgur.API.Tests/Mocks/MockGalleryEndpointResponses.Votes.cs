namespace Imgur.API.Tests.Mocks
{
    public partial class MockGalleryEndpointResponses
    {
        public const string GetGalleryItemVotes =
            "{\"data\":{\"ups\":11347,\"downs\":751},\"success\":true,\"status\":200}";

        public const string VoteGalleryItem =
            "{\"data\":true,\"success\":true,\"status\":200}";
    }
}