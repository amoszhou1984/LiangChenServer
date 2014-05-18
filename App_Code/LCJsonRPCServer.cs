using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AustinHarris.JsonRpc;

namespace LCServer
{
    public class LCJsonRPCServer : JsonRpcService
    {
        IDictionary<string, IList<string>> events = new Dictionary<string, IList<string>>();
        IDictionary<string, string> users = new Dictionary<string, string>();

        [JsonRpcMethod]
        private string login(string username, string password)
        {
            if (users.ContainsKey(username))
            {
                string passwordStored = users[username];
                if (passwordStored == password)
                {
                    return "Hello, " + username + "!";
                }
            }
            return "Wrong username or password";
        }

        [JsonRpcMethod]
        private string register(string username, string password)
        {
            if (users.ContainsKey(username))
            {
                return "Username exists.";
            }
            else
            {
                users[username] = password;
                return "Successfully created account for " + username;
            }
        }

        [JsonRpcMethod]
        private string createEvent(string userID)
        {
            //No such user
            if (!users.ContainsKey(userID))
            {
                return "Invalid user";
            }
            //User exists but no events
            if (!events.ContainsKey(userID))
            {
                events[userID] = new List<string>();
            }
            IList<string> eventIDs = events[userID];
            Random r = new Random();
            string eventID = String.Format("E{0:D6}", r.Next(1000000));
            eventIDs.Add(eventID);
            return "Event " + eventID + " created for " + userID;
        }

        [JsonRpcMethod]
        private string getEvent(string userID)
        {
            if (!users.ContainsKey(userID))
            {
                return "Invalid user";
            }

            if (!events.ContainsKey(userID))
            {
                return "No event found for this user";
            }
            else
            {
                StringBuilder sb = new StringBuilder("All events: ");
                foreach(var eventID in events[userID]){
                    sb.Append(eventID + " ");
                }
                return sb.ToString();
            }
        }
    }
}
