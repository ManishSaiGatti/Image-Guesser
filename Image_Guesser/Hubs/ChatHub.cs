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
        private static Dictionary<String, ArrayList> userStorage = new Dictionary<String, ArrayList>();
        public async Task SendMessage(string user, string message, string groupName)
        {
            if (userStorage.ContainsKey(Context.ConnectionId))
            {
                Console.WriteLine("sending message rn");
                
                await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
            }
        }
        public async Task AddToGroup(string groupName)
        {
            if (!userStorage.ContainsKey(groupName))
            {
                userStorage.Add(groupName, new ArrayList());
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                Console.WriteLine(userStorage.GetValueOrDefault(Context.ConnectionId));
                await Clients.Group(groupName).SendAsync("ReceiveMessage", groupName, $"{groupName} has been created.");

            }
            else
            {
                ArrayList work = userStorage.GetValueOrDefault(groupName);
                if (!work.Contains(Context.ConnectionId))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                    await Clients.Group(groupName).SendAsync("ReceiveMessage", Context.ConnectionId, $"{Context.ConnectionId} has joined the group {groupName}.");
                    work.Add(Context.ConnectionId);
                }
            }

        }

        public async Task RemoveFromGroup(String groupName)
        {
            if (userStorage.ContainsKey(groupName))
            {
                userStorage.GetValueOrDefault(groupName).Remove(Context.ConnectionId);
                await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            }
        }

        public static List<String> getUserList(String groupName)
        {
            if (userStorage.ContainsKey(groupName))
            {
                return userStorage.GetValueOrDefault(groupName).Cast<String>().ToList();
            }
            return null;
        }
    }
        
}
