using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.IO;
using Newtonsoft.Json;
using chatroom.Models;

namespace chatroom
{
   public class ParseMessage
   {
      private string message = "";
      private string group = "";
      private UserInfo userinfo = new UserInfo();
      private string url = "";
      private static Dictionary<string , List<ChatObject>> dictionary = new Dictionary<string , List<ChatObject>>();
      private const string pattern = @"((mailto\:|(news|(ht|f)tp(s?))\://){1}\S+)";
      private const string staticHTML = "<br/><img align='bottom' class='image' src='/";
      private string filepath = "";

      public ParseMessage( string group , UserInfo userinfo , string message,string filepath )
      {
         this.message = message;
         this.userinfo = userinfo;
         this.group = group;
         this.filepath = filepath;
      }

      ///<summary>
      /// will get message after formating it
      ///</summary>
      public string GetMessage()
      {
         if(filepath != null)
            message = message + staticHTML + filepath + "'/>";

         IsCode();
         CheckLink();

         return message;
      }


      private void IsCode()
      {
         if(message.IndexOf(';') != -1 && message.IndexOf('{') != -1 && message.IndexOf('}') != -1)
            message = "Posted this code <pre><code>" + message.Replace("\r\n" , "<br/>") + "</code></pre>";
      }


      ///<summary>
      /// will check for link in the given message
      ///</summary>
      private void CheckLink()
      {
         MatchCollection matches = Regex.Matches(message , pattern);
         int matchcount = matches.Count;

         if(matchcount > 0)
            switch(matchcount) {

               case 1:
                  url = matches[0].Value;
                  message = message.Replace(matches[0].Value , "") + GetLink();
                  break;

               default:
                  foreach(Match n in matches)
                     message = message.Replace(n.Value , "<a href='" + n.Value + "' target='_blank'>" + ShortUrl(n.Value) + "</a>");

                  break;
            }

      }


      private string GetLink()
      {
         string webdata = null;

         HtmlDocument document = new HtmlWeb().Load(url); // get website data as document
         HtmlNode metatag = document.DocumentNode.SelectSingleNode("//meta[@name='description']");

         if(metatag != null) {
            webdata = metatag.Attributes["content"].Value;

            if(webdata != null)
               return "<label class='link-content'>\" " + webdata + " \"<br/><a href='" + url + "' target='_blank'>read more...</a></label>";
         }
         return "<a href='" + url + "' target='_blank'>[read more from site...]</a>";
      }


      private string ShortUrl( string link )
      {
         if(link.Length > 35) return link.Substring(0 , 33) + "...";

         return link;
      }


      ///<summary>
      /// check if message is relevant , should not contain abuse words
      ///</summary>
      public Boolean FlagMessage()
      {
         return true;
      }

      ///<summary>
      /// check max. number of message in stack is reached or not(i.e > 20)
      /// if reached add to database in msg table
      /// then clear dictionary[group]
      ///</summary>
      public void MessageStack()
      {
         ChatObject chatobject = new ChatObject() {
            userinfo = userinfo ,
            message = message
         };

         List<ChatObject> list = new List<ChatObject>();

         if(!dictionary.ContainsKey(group))
            dictionary.Add(group , list);

         dictionary[group].Add(chatobject);

         if(dictionary[group].Count > 50)
            using(Data db = new Data()) {
               db.chatdatas.Add(new ChatData() { groupname = group , chatmsg = JsonConvert.SerializeObject(dictionary[group]) });
               db.SaveChanges();
               dictionary[group].Clear();
            }

      }

      ///<summary>
      ///Get recent messages from dictionary[group]
      ///</summary>
      public string GetStoredMessages()
      {
         List<ChatObject> list = new List<ChatObject>();

         if(!dictionary.ContainsKey(group))
            dictionary.Add(group , list);

         return JsonConvert.SerializeObject(dictionary[group].ToList());
      }
   }



   public class FileSetup
   {
      private HttpPostedFileBase file = null;
      private string path = "";
      private string filename = "";

      public FileSetup( ref HttpPostedFileBase file , string filename )
      {
         this.file = file;
         this.filename = filename.ToLower();
      }

      ///<summary>
      /// Save file to server , file size should be less than 500KB
      ///</summary>
      public string FileUpload()
      {
         if(file.ContentLength / 1024 <= 500)
            if(ImageExtension())
               path = SaveImage();

         return path;
      }


      private Boolean ImageExtension()
      {
         return filename.EndsWith(".png") || filename.EndsWith(".jpg") ||
                filename.EndsWith(".jpeg");
      }


      private string SaveImage()
      {
         filename = new Random().Next(1 , 9999) + filename.Replace(".png" , ".jpg");
         path = Path.Combine(HttpContext.Current.Server.MapPath("/Images") , filename);
         file.SaveAs(path);

         return "Images/" + filename;
      }

   }
}