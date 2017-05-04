using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveChat.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string sender, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.receiveNewMessage(sender, message);
        }
    }
}