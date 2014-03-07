using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using chatroom.Models;

namespace chatroom
{
   public class Chathub : Hub
   {
      static Dictionary<string , HashSet<string>> grouplist = new Dictionary<string , HashSet<string>>();
      const string staticHTML = "<br/><img align='bottom' class='image' src='/";
      string previous_group = "";
      private UserInfo userinfo = new UserInfo();

      public void Send( string group , string message , string filepath )
      {
         RetrieveInfo();
         ParseMessage parsedmsg = new ParseMessage(group , userinfo , HttpUtility.HtmlEncode(message) , filepath);

         if(parsedmsg.FlagMessage()) {
            message = parsedmsg.GetMessage();

            Clients.Group(group).broadcastmessage(userinfo.username , userinfo.userimage , message);
            parsedmsg.MessageStack();
         }
      }


      private void RetrieveInfo()
      {
         userinfo = JsonConvert.DeserializeObject<UserInfo>(HttpContext.Current.Request.Cookies["userinfo"].Value);
      }


      private void RetrieveGroup( string current_group )
      {
         previous_group = HttpContext.Current.Request.Cookies["group"].Value;
         if(previous_group != current_group) {
            HashSet<string> old_group = new HashSet<string>();

            if(!grouplist.ContainsKey(previous_group))
               grouplist.Add(previous_group , old_group);

            if(current_group != null) {
               grouplist[previous_group].Remove(Context.ConnectionId);

               HttpCookie cookie = new HttpCookie("group");
               cookie.Expires = DateTime.Now.AddHours(5);
               cookie.Value = current_group;
               HttpContext.Current.Response.Cookies.Add(cookie);

               HashSet<string> new_group = new HashSet<string>();

               if(!grouplist.ContainsKey(current_group))
                  grouplist.Add(current_group , new_group);

               grouplist[current_group].Add(Context.ConnectionId);
            }
         }
      }


      public void JoinGroup( string current_group )
      {
         RetrieveGroup(current_group);

         if(current_group != previous_group) {
            Groups.Add(Context.ConnectionId , current_group);
            Groups.Remove(Context.ConnectionId , previous_group);

            Clients.Group(current_group).groupcount(grouplist[current_group].Count);
            Clients.Group(previous_group).groupcount(grouplist[previous_group].Count);
         }
      }


      public override System.Threading.Tasks.Task OnConnected()
      {
         HashSet<string> initial_group = new HashSet<string>();
         initial_group.Add(Context.ConnectionId);

         Groups.Add(Context.ConnectionId , "All");

         if(!grouplist.ContainsKey("All"))
            grouplist.Add("All" , initial_group);

         grouplist["All"].Add(Context.ConnectionId);

         Clients.Group("All").groupcount(grouplist["All"].Count);

         return base.OnConnected();
      }

   }
}