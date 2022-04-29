using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Image_Guesser.Hubs
{
    public class GroupSingleton
    {
        public static ArrayList groups = new ArrayList();
 
    }

    public class ChatHub : Hub
    {

        public async Task SendMessage(string user, string message, string groupName)
        {   
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }
        public async Task CreateGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            GroupSingleton.groups.Add(groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", Context.ConnectionId, $"{Context.ConnectionId} has joined the group {groupName}.");
        }
        public async Task<bool> JoinGroup(string groupName)
        {
            foreach(string group in GroupSingleton.groups)
            {
                if(group.Equals(groupName))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                    // add user to group as well
                    await Clients.Group(groupName).SendAsync("ReceiveMessage", Context.ConnectionId, $"{Context.ConnectionId} has joined the group {groupName}.");
                    return true;
                }
            }
                return false;
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}
