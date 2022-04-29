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
        private static Dictionary<String, String> userStorage = new Dictionary<string, string>();
        public async Task SendMessage(string user, string message, string groupName)
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
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            userStorage.Remove(Context.ConnectionId);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");

        }
    }
}
