<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head runat="server">
   <meta http-equiv="content-type" content="text/html; charset=utf-8" />
   <meta name="viewport" content="user-scalable=no,initial-scale=1.0,maximum-scale=1.0,width=device-width,height=device-height" />
   <title>Welcome to Study Room</title>
   <script src="../../Content/jquery.js" type="text/javascript"></script>
   <script type="text/javascript" src="http://topicquestions.com/Content/source/ajax.js"></script>
   <script src="../../Content/fbconnect.js" type="text/javascript"></script>
   <div id="fb-root">
   </div>
   <style type="text/css">
      .body
      {
         margin: 0px;
         font-family: 'lucida grande' ,tahoma,verdana,arial,sans-serif;
         font-size: 13px;
         line-height: 23px;
         -webkit-font-smoothing: antialiased;
         min-width: 1100px;
      }
      
      .heading
      {
         font-size: 60px;
         color: #4A4A4A;
         font-variant: small-caps;
         margin-bottom: 5px;
      }
      
      .text
      {
         color: Gray;
      }
      #header
      {
         background-image: url('/AppImages/header.png');
         background-repeat: repeat-x;
         height: 30px;
         margin-bottom: 10px;
         padding: 5px 0 0 20px;
         color: White;
         font-size: 18px;
      }
      
      .footer
      {
         bottom: 0;
         height: 50px;
         padding: 10px 0 10px 41%;
         position: absolute;
      }
      
      a
      {
         text-decoration: none;
         color: Gray;
         margin-right: 10px;
      }
   </style>
</head>
<body class="body">
   <div id="header">
   </div>
   <div style="margin: 50px;" align="center">
      <label class="heading">
         Online Study Rooms</label><br />
      <label class="text">
         XYZ is a webapp for academic platform that let you connect & discuss doubts with your<br />
         peers or people with same academic background in realtime
      </label>
      <div>
         <br />
         <br />
         <img id="fb_button" src="../../appimages/fblogin.png" width="260px" />
         <br />
         <label class="text">
            Connect with facebook to create your account
         </label>
      </div>
   </div>
   <div class="footer">
      <a href="">About Us</a> <a href="">Contact Us</a> <a href="">Blog</a> <a href="">Press</a>
   </div>
</body>
</html>
