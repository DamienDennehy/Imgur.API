# Conversation Endpoint

##BlockSenderAsync
Block the user from sending messages to the user that is logged in. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new ConversationEndpoint(client);
		var blocked = await endpoint.BlockSenderAsync("SENDER");
			
##CreateConversationAsync
Create a new message. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new ConversationEndpoint(client);
		var created = await endpoint.CreateConversationAsync("RECIPIENT", "MESSAGE");

##DeleteConversationAsync
Delete a conversation by the given id. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new ConversationEndpoint(client);
		var deleted = await endpoint.DeleteConversationAsync("CONVERSATION_ID");

##GetConversationAsync
Get information about a specific conversation. Includes messages.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new ConversationEndpoint(client);
		var conversation = await endpoint.GetConversationAsync("CONVERSATION_ID");

##GetConversationsAsync
Get list of all conversations for the logged in user.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new ConversationEndpoint(client);
		var conversations = await endpoint.GetConversationsAsync();

##ReportSenderAsync
Report a user for sending messages that are against the Terms of Service.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new ConversationEndpoint(client);
		var reported = await endpoint.ReportSenderAsync("SENDER");