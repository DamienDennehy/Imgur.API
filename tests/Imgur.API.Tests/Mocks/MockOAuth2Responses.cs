namespace Imgur.API.Tests.Mocks
{
    public static class MockOAuth2Responses
    {
        public const string GetTokenResponse =
            "{\"access_token\":\"6e079993b20f45fab3c22ed489a6f454\",\"expires_in\":315360000,\"token_type\":\"bearer\",\"scope\":\"\",\"refresh_token\":\"e325da142cd545298ef68868824d3b8a\",\"account_id\":135798223,\"account_username\":\"A8XTgSW8pWrNCFwR\"}";

        public const string GetTokenResponseError =
            "{\"data\":{\"error\":\"Refresh token doesn't exist or is invalid for the client\",\"request\":\"\\/oauth2\\/token\",\"method\":\"POST\"},\"success\":false,\"status\":400}";
    }
}
