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
            Clients.All.receiveNewMessage(sender, message);
        }

        public void Join(string name)
        {
            Clients.All.joinChat(name);
        }
    }
}