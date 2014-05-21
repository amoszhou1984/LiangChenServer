using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AustinHarris.JsonRpc;

namespace LCServer
{
    public class LCJsonRPCServer : JsonRpcService
    {
        LCDBEntities entities = new LCDBEntities();

        [JsonRpcMethod]
        private string login(string email, string password)
        {
            foreach (User user in entities.Users)
            {
                if (user.email == email)
                {
                    if (user.password == password)
                    {
                        return "Login successful";
                    }
                }
            }
            return "Login failed";
        }

        [JsonRpcMethod]
        private string register(string email, string password)
        {
            bool userExists = false;
            foreach (User user in entities.Users)
            {
                if (user.email == email)
                {
                    userExists = true;
                    break;
                }
            }
            if (!userExists)
            {
                User newUser = new User();
                newUser.password = password;
                newUser.email = email;
                entities.Users.Add(newUser);
                entities.SaveChanges();
                return "Registration successful";
            }
            return "Registration failed";
        }

        //[JsonRpcMethod]
        //private string createEvent(string userID)
        //{
        //    //No such user
        //    if (!users.ContainsKey(userID))
        //    {
        //        return "Invalid user";
        //    }
        //    //User exists but no events
        //    if (!events.ContainsKey(userID))
        //    {
        //        events[userID] = new List<string>();
        //    }
        //    IList<string> eventIDs = events[userID];
        //    Random r = new Random();
        //    string eventID = String.Format("E{0:D6}", r.Next(1000000));
        //    eventIDs.Add(eventID);
        //    return "Event " + eventID + " created for " + userID;
        //}

        //[JsonRpcMethod]
        //private string getEvent(string userID)
        //{
        //    if (!users.ContainsKey(userID))
        //    {
        //        return "Invalid user";
        //    }

        //    if (!events.ContainsKey(userID))
        //    {
        //        return "No event found for this user";
        //    }
        //    else
        //    {
        //        StringBuilder sb = new StringBuilder("All events: ");
        //        foreach(var eventID in events[userID]){
        //            sb.Append(eventID + " ");
        //        }
        //        return sb.ToString();
        //    }
        //}
    }
}
