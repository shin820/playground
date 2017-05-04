using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveChat.Hubs
{
    public class ChatHub : Hub<IChat>
    {
        public void Send(string sender, string message)
        {
            Clients.All.NewMessage(sender, message);
        }

        public void Join(string name)
        {
            Clients.All.Join(name);
        }
    }

    public interface IChat
    {
        void NewMessage(string sender, string message);
        void Join(string name);
    }
}