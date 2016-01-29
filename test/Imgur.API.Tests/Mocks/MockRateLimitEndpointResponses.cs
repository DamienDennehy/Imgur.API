namespace Imgur.API.Tests.Mocks
{
    public class MockRateLimitEndpointResponses
    {
        public const string GetRateLimit =
            "{\"data\":{ \"UserLimit\":412, \"UserRemaining\":382, \"UserReset\":1439945895, \"ClientLimit\":10500, \"ClientRemaining\":9500 }, \"success\":true, \"status\":200 }";
    }
}