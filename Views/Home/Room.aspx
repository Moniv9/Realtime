<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head runat="server">
   <title>Room</title>
   <script src="../../Content/jquery.js" type="text/javascript"></script>
   <script src="../../Content/ajax.js" type="text/javascript"></script>
   <link href="../../Content/style1.css" rel="stylesheet" type="text/css" />
   <script src="http://malsup.github.com/jquery.form.js"></script>
   <script src="../../Scripts/jquery.signalR-1.1.3.min.js" type="text/javascript"></script>
   <script src="../../Content/chat.js" type="text/javascript"></script>
   <script src="/signalr/hubs" type="text/javascript"></script>
</head>
<body class="body">
   <div id="header">
      <div id="group-notify" align="right">
         People online in this group</div>
   </div>
   <div id="sidebar">
      <p class="seperator">
         Algorithms</p>
      <p class="seperator">
         Data Structures</p>
      <p class="seperator">
         Physics</p>
      <p class="seperator">
         Electronics</p>
      <p class="seperator">
         Aptitude</p>
      <p class="seperator">
         Probability</p>
   </div>
   <div id="container">
      <div id="notify">
      </div>
      <div id="msgarea">
      </div>
      <div id="msgpanel">
         <input type="hidden" id="groupname" value="<%=ViewBag.GroupName %>" />
         <textarea id="msgbox" placeholder="Enter Text"></textarea>
         <div id="button">
            Send</div>
      </div>
      <br />
      <input id="no-enter" type="checkbox" value="true" /><!--no enter = true-->
      Disable Enter
      <!--image upload-->
      <div id="imagebox">
         <form id="form1" action="/home/upload" method="post" enctype="multipart/form-data">
         <input type="file" name="file" title="Max. Size 500KB" />
         <input type="submit" value="Upload" title="Click to upload image to server" />
         </form>
      </div>
      <!--image upload end-->
      <br />
      <br />
   </div>
</body>
</html>
