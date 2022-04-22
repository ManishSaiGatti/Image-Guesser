using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Image_Guesser.Hubs
{
    public class ChatHub : Hub
    {
        
        private Dictionary<String, String> userStorage;
        public ChatHub()
        {
            userStorage = new Dictionary<string, string>();
        }
        public async Task SendMessage(string user, string message)
        {
            if (userStorage.ContainsKey(Context.ConnectionId))
            {
                Console.WriteLine("sending message rn");
                await Clients.Group(userStorage.GetValueOrDefault(Context.ConnectionId)).SendAsync("ReceiveMessage", user, message);
            }
            
        }
        public async Task AddToGroup(string groupName)
        {
            if (!userStorage.ContainsKey(Context.ConnectionId))
            {
                userStorage.Add(Context.ConnectionId, groupName);
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                Console.WriteLine(userStorage.GetValueOrDefault(Context.ConnectionId));
                await Clients.Group(groupName).SendAsync("ReceiveMessage", Context.ConnectionId, $"{Context.ConnectionId} has joined the group {groupName}.");
            }
            
        }

        public async Task RemoveFromGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userStorage.GetValueOrDefault(Context.ConnectionId));
            await Clients.Group(userStorage.GetValueOrDefault(Context.ConnectionId)).SendAsync("Send", $"{Context.ConnectionId} has left the group {userStorage.GetValueOrDefault(Context.ConnectionId)}.");
            userStorage.Remove(Context.ConnectionId);

        }
    }
}
