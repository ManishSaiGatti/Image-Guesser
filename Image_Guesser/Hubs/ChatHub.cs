using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Image_Guesser.Hubs
{
    public class User
    {
        private string connectionId;
        private string userName;
        private bool isReady;
        public User(string connectionId, string userName)
        {
            this.connectionId = connectionId;
            this.userName = userName;
            isReady = false;
        }

        public void changeReady()
        {
            isReady = !isReady;
        }

        public string getConnectionId()
        {
            return connectionId;
        }

        public string getUserName()
        {
            return userName;
        }

        public bool getIsReady()
        {
            return isReady;
        }
    }

    public class ChatHub : Hub
    {
        // the key is group code, the ArrayList contains all users in the group
        private static Dictionary<String, ArrayList> userStorage = new Dictionary<String, ArrayList>();
        public async Task SendMessage(string user, string message, string groupName)
        {
            if (userStorage.ContainsKey(groupName))
            {
                Console.WriteLine("sending message rn");
                
                await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
            }
        }
        public async Task<bool> AddToGroup(string groupName, string userName)
        {
            if (!userStorage.ContainsKey(groupName))
            {
                userStorage.Add(groupName, new ArrayList());
                userStorage.GetValueOrDefault(groupName).Add(new User(Context.ConnectionId, userName));
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                Console.WriteLine(userStorage.GetValueOrDefault(Context.ConnectionId));
                await Clients.Group(groupName).SendAsync("ReceiveMessage", groupName, $"{groupName} has been created.");
                return true;
            }
            else
            {
                ArrayList work = userStorage.GetValueOrDefault(groupName);
                if (!work.Contains(Context.ConnectionId))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                    await Clients.Group(groupName).SendAsync("ReceiveMessage", Context.ConnectionId, $"{Context.ConnectionId} has joined the group {groupName}.");
                    work.Add(new User(Context.ConnectionId, userName));
                }
                return true;
            }

        }
        public async Task<bool> CheckGroup(string groupName)
        {
            if (!userStorage.ContainsKey(groupName))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public async Task CreateGroup(string groupName, string userName)
        {
            userStorage.Add(groupName, new ArrayList());
            userStorage.GetValueOrDefault(groupName).Add(new User(Context.ConnectionId, userName));
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine(userStorage.GetValueOrDefault(Context.ConnectionId));
            await Clients.Group(groupName).SendAsync("ReceiveMessage", groupName, $"{groupName} has been created.");
    
        }

        public async Task RemoveFromGroup(String groupName, String userName)
        {
            if (userStorage.ContainsKey(groupName))
            {
                userStorage.GetValueOrDefault(groupName).Remove(searchUsers(groupName, userName));
                await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            }
        }

        public static List<String> getUsernameList(String groupName)
        {
            List<String> temp = new List<String>();
            if (userStorage.ContainsKey(groupName))
            {

                foreach (User user in userStorage.GetValueOrDefault(groupName))
                {
                    temp.Add(user.getUserName());
                }
                return temp;
            }
            return null;
        }

        public static List<User> getUserList(String groupName)
        {
            List<User> temp = new List<User>();
            if (userStorage.ContainsKey(groupName))
            {

                userStorage.GetValueOrDefault(groupName);

            }
            return null;
        }
        public void ChangeStatus(string groupName, string userInput)
        {
            searchUsers(groupName, userInput).changeReady();
        }

        private User searchUsers(String groupName, String userName)
        {
            ArrayList temp = userStorage.GetValueOrDefault(groupName);
            foreach(User user in temp) {
                if (user.getUserName().Equals(userName))
                {
                    return user;
                }
            }
            return null;
        }
    }
    
    
}
