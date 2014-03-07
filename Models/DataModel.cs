using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace chatroom.Models
{
   public class UserData
   {
      [Required , Key , Column(Order = 0)]
      public string emailID { get; set; }

      [Required]
      public string username { get; set; }

      public string userimage { get; set; }
      public string usersummary { get; set; }
   }

   public class Info
   {
      public string school { get; set; }
      public string location { get; set; }
      public string bio { get; set; }
   }

   public class UserInfo
   {
      public string emailID { get; set; }
      public string username { get; set; }
      public string userimage { get; set; }
   }

   public class ChatData
   {
      [Required , Key]
      public string groupname { get; set; }

      [Required]
      public string chatmsg { get; set; }
   }

   public class ChatObject
   {
      public UserInfo userinfo { get; set; }
      public string message { get; set; }
   }

   public class Data : DbContext
   {
      public DbSet<ChatData> chatdatas { get; set; }
      public DbSet<UserData> userdatas { get; set; }
   }



}