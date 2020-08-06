namespace Imgur.API.Tests.Mocks
{
    public static class MockOAuth2Responses
    {
        public const string GetTokenResponse =
            "{\"access_token\":\"f55964fe893dd65ff91c44d4049475bde4560f0d\",\"expires_in\":315360000,\"token_type\":\"bearer\",\"scope\":\"\",\"refresh_token\":\"4091a39bb1c15b99c9f313743ccf22f2196856c5\",\"account_id\":135798223,\"account_username\":\"A8XTgSW8pWrNCFwR\"}";

        public const string GetTokenResponseError =
            "{\"data\":{\"error\":\"Refresh token doesn't exist or is invalid for the client\",\"request\":\"\\/oauth2\\/token\",\"method\":\"POST\"},\"success\":false,\"status\":400}";
    }
}
