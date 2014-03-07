using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chatroom.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace chatroom.Controllers
{
   public class HomeController : Controller
   {
      //
      // GET: /Home/

      public ActionResult Index()
      {
         CookiesPresent();
         return View();
      }



      public string GetMessages( string group )
      {
         return (new ParseMessage(group , null , null,null)).GetStoredMessages();
      }



      public ViewResult Room()
      {
         if(Request.Cookies["group"] != null)
            ViewBag.GroupName = Request.Cookies["group"].Value;
         else {
            HttpCookie cookie = new HttpCookie("group");
            cookie.Expires = DateTime.Now.AddHours(5);
            cookie.Value = "All";
            Response.Cookies.Add(cookie);
            ViewBag.GroupName = "All";
         }

         return View();
      }



      public void Login( string email , string name , string userimage )
      {
         UserData userdata = new UserData();

         using(Data db = new Data()) {
            userdata = db.userdatas.FirstOrDefault(x => x.emailID.Contains(email));
            if(userdata == null) {
               userdata = new UserData() {
                  emailID = email ,
                  username = name ,
                  userimage = userimage ,
                  usersummary = ""
               };

               db.userdatas.Add(userdata);
               db.SaveChanges();
            }

            FormCookie(ref userdata);
         }
      }



      private void FormCookie( ref UserData userdata )
      {
         UserInfo info = new UserInfo() {
            emailID =  userdata.emailID ,
            username = userdata.username ,
            userimage = userdata.userimage
         };

         HttpCookie cookie = new HttpCookie("userinfo");
         cookie.Expires = DateTime.Now.AddHours(5);
         cookie.Value = JsonConvert.SerializeObject(info);
         Response.Cookies.Add(cookie);
         Response.Redirect("/home/room" , true);
      }



      private void CookiesPresent()
      {
         if(Request.Cookies["userinfo"] != null)
            Response.Redirect("/home/room");
      }



      public string Upload( HttpPostedFileBase file )
      {
         return (new FileSetup(ref file , file.FileName)).FileUpload();
      }  

   }
}
