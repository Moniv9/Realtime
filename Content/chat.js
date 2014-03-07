$(function () {
   var chat = $.connection.chathub;

   chat.client.broadcastmessage = function (name, image, message) {
      var formatmsg = "<p class='chatline'><img class='pimage' src='" + image + "' align='left'/><b>" + name + "</b><br/>" +
                      message + "</p>";
      $('#msgarea').append(formatmsg);
      $('#msgarea').animate({ scrollTop: $('#msgarea')[0].scrollHeight }, 1000);
   };


   $.connection.hub.connectionSlow(function () {
      $('#notify').html("Internet connection seems to be slow");
   });

   chat.client.groupcount = function (count) {
      $('#group-notify').html('People online in this group ' + count);
   };

   $.connection.hub.start().done(function () {

      $('.seperator').click(function () {
         
         var groupname = $(this).text().trim();
         $('#groupname').val(groupname);
         $('.seperator').css('background-color', 'white');
         $('.seperator').css('font-weight', 'normal');
         $(this).css('background-color', '#F5F5F5');
         $(this).css('font-weight', 'bold');
         chat.server.joinGroup(groupname);

         $.ajax({
            url: '/home/getmessages',
            type: 'POST',
            datatype: 'text',
            data: 'group=' + groupname,
            success: function (data) {
               
               $('#msgarea').html("");
               var formatmsg = JSON.parse(data);
               for (var i = 0; i < formatmsg.length; i++) {
                  $('#msgarea').append("<p class='chatline'><img class='pimage' src='" + formatmsg[i].userinfo.userimage + "' align='left'/><b>" + formatmsg[i].userinfo.username + "</b><br/>" +
                      formatmsg[i].message + "</p>");
               }

               $('#msgarea').animate({ scrollTop: $('#msgarea')[0].scrollHeight }, 1000);
            }
         });

      });

      $('#msgbox').bind('keypress', function (e) {

         if (e.keyCode == 13 && $('#no-enter:checked').val() == undefined) {
            var message = $('#msgbox').val();
            var groupname = $('#groupname').val();
            chat.server.send(groupname, message, filename);

            $('#msgbox').val("");
            $('#notify').hide();
            filename = null;
         }
      });

      $('#button').click(function () {
         var message = $('#msgbox').val();
         var groupname = $('#groupname').val();
         chat.server.send(groupname, message, filename);

         $('#msgbox').val("");
         $('#notify').hide();
         filename = null;

      });

      $('#form1').ajaxForm(function (data) {
         $('#notify').show();
         $('#notify').html("Image Uploaded succesfully");
         filename = data;
      });
   });

   var filename = null;


   $('.image').click(function () {
    
   });

});