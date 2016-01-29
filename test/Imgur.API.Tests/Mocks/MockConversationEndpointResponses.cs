namespace Imgur.API.Tests.Mocks
{
    public class MockConversationEndpointResponses
    {
        public const string BlockSender =
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string CreateConversation =
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string DeleteConversation =
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string GetConversation =
            "{\"data\":{\"id\":34361981,\"with_account\":\"Bob\",\"with_account_id\":1198054,\"last_message_preview\":\"Test 2\",\"message_count\":3,\"messages\":[{\"id\":163690409,\"from\":\"Bob\",\"account_id\":24562464,\"sender_id\":1198054,\"body\":\"Test\",\"conversation_id\":34361981,\"datetime\":1443299681},{\"id\":167384071,\"from\":\"imgurapidotnet\",\"account_id\":24562464,\"sender_id\":24562464,\"body\":\"Test33\",\"conversation_id\":34361981,\"datetime\":1444617103},{\"id\":187176581,\"from\":\"Bob\",\"account_id\":24562464,\"sender_id\":1198054,\"body\":\"Test 2\",\"conversation_id\":34361981,\"datetime\":1451010592}],\"done\":false,\"page\":2,\"datetime\":1451010592},\"success\":true,\"status\":200}";

        public const string GetConversations =
            "{\"data\":[{\"id\":34361981,\"with_account\":\"Bob\",\"with_account_id\":434343,\"last_message_preview\":\"Test33\",\"message_count\":2,\"datetime\":1444617103}],\"success\":true,\"status\":200}";

        public const string ReportSender =
            "{\"data\":true,\"success\":true,\"status\":200}";
    }
}