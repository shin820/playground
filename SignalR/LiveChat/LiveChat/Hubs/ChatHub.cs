//How to use Clients?
//Clients.Caller
//Clients.Others
//Clients.Client(Context.ConnectionId)
//Clients.AllExcept(connectionId1, connectionId2)
//Clients.Group(groupName)
//Clients.Group(groupName, connectionId1, connectionId2).addContosoChatMessageToPage(name, message)
//Clients.OthersInGroup(groupName)
//Clients.User(userid), by default userid is 'IPrincipal.Identity.Name'
//Clients.Clients(ConnectionIds)
//Clients.Groups(GroupIds)
//Clients.Client(username)
//Clients.Users(new string[] { "myUser", "myUser2" })

//How to manage group membership from the Hub class?
//Groups.Add(Context.ConnectionId, groupName);
//Groups.Remove(Context.ConnectionId, groupName);
/*You don't have to explicitly create groups. In effect a group is automatically created the first time you
 *specify its name in a call to Groups.Add, and it is deleted when you remove the last connection from membership in it.
 */
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }

    public interface IChat
    {
        void NewMessage(string sender, string message);
        void Join(string name);
    }
}