# Notification Endpoint

##GetNotificationAsync
Returns the data about a specific notification. OAuth authentication required.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new ConversationEndpoint(client);
			var notification = await endpoint.GetNotificationAsync("NOTIFICATION_ID");
			
##GetNotificationsAsync
Returns all of the notifications for the user. OAuth authentication required.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new ConversationEndpoint(client);
			var notifications = await endpoint.GetNotificationsAsync();

##MarkNotificationViewedAsync
Marks a notification as viewed. OAuth authentication required.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new ConversationEndpoint(client);
			var marked = await endpoint.MarkNotificationViewedAsync("NOTIFICATION_ID");

##MarkNotificationsViewedAsync
Marks notifications as viewed. OAuth authentication required.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new ConversationEndpoint(client);
			var marked = await endpoint.MarkNotificationsViewedAsync(new List<string>{"NOTIFICATION_ID", "ANOTHER_NOTIFICATION_ID"});
