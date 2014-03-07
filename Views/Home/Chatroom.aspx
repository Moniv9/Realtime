<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head runat="server">
   <meta http-equiv="content-type" content="text/html; charset=utf-8" />
   <meta name="viewport" content="user-scalable=no,initial-scale=1.0,maximum-scale=1.0,width=device-width" />
   <title>Welcome to Chatroom</title>
   <link href="../../Content/style1.css" rel="stylesheet" type="text/css" />
   <script src="../../Content/source/ajax.js" type="text/javascript"></script>
   <script src="http://code.jquery.com/jquery-1.8.3.min.js" type="text/javascript"></script>
   <script src="../../Content/source/ui.js" type="text/javascript"></script>
   <script src="http://malsup.github.com/jquery.form.js"></script>
   <script src="../../Scripts/jquery.signalR-1.1.3.min.js" type="text/javascript"></script>
   <script src="/signalr/hubs" type="text/javascript"></script>
   <script src="../../Content/chat.js" type="text/javascript"></script>
</head>
<body class="body">
   <div class="container">
      <div id="discuss">
         <%for(int i = Model.Count - 1; i >= 0; i--) { %>
         <p class='chatline'>
            <b>
               <%=Model[i].username%></b> :
            <%=Model[i].chattext%>
         </p>
         <%} %>
      </div>
      <div class="info">
         <input type="hidden" id="displayname" value="<%=ViewBag.username %>" />
         <input type="hidden" id="userID" value="<%=ViewBag.userID %>" />
      </div>
      <!--message box-->
      <div id="msgbox">
         <div id="status">
         </div>
         <textarea id="message" placeholder="Enter text" required></textarea>
         <br />
         <!--tools-->
         <form id="form1" action="/home/Upload" method="post" enctype="multipart/form-data">
         <input id="upload-button" type="button" value="Add File" />
         <input id="submit-button" type="submit" value="Submit" />
         <label id="file-status">
         </label>
         <input id="fileupload" type="file" name="file" />
         </form>
      </div>
   </div>
   <div id="online-box">
      <b>People Online</b><br />
      <br />
      <div id="online-people">
      </div>
      <input id="join" type="button" value="Join" />
   </div>
</body>
</html>
