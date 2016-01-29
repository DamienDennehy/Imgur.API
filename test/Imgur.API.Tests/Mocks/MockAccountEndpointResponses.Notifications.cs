namespace Imgur.API.Tests.Mocks
{
    public partial class MockAccountEndpointResponses
    {
        public const string GetCommentNotification =
            "{\"album_cover\": null,\"author\": \"jasdev\",\"author_id\": 3698510,\"children\": [],\"comment\": \"Reply test\",\"datetime\": 1406070774,\"deleted\": false,\"downs\": 0,\"id\": 3616,\"image_id\": \"VK9VqcM\",\"on_album\": false,\"parent_id\": 3615,\"points\": 1,\"ups\": 1,\"vote\": null }";

        public const string GetMessageNotification =
            "{\"id\":\"76767\",\"account_id\":\"89898\",\"with_account\":\"3434\",\"spam\":\"0\",\"message_num\":\"2\",\"last_message\":\"Test33\",\"from\":\"Bob\",\"datetime\":1444617103}";

        public const string GetNotifications =
            "{\"data\": {\"replies\": [{ \"id\": 4511, \"account_id\": 384077, \"viewed\": false, \"content\": {\"album_cover\": null,\"author\": \"jasdev\",\"author_id\": 3698510,\"children\": [],\"comment\": \"Reply test\",\"datetime\": 1406070774,\"deleted\": false,\"downs\": 0,\"id\": 3616,\"image_id\": \"VK9VqcM\",\"on_album\": false,\"parent_id\": 3615,\"points\": 1,\"ups\": 1,\"vote\": null }}],\"messages\": [{ \"id\": 4523, \"account_id\": 384077, \"viewed\": false, \"content\": {\"id\": \"620\",\"from\": \"jasdev\",\"account_id\": \"384077\",\"with_account\": \"3698510\",\"last_message\": \"wow. such message.\",\"message_num\": \"103\",\"datetime\": 1406935917 }}]},\"success\": true,\"status\": 200 }";
    }
}