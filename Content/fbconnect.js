$(document).ready(function () {
   window.fbAsyncInit = function () {
      // init the FB JS SDK
      FB.init({
         appId: '437979352905407',                        // App ID from the app dashboard
         channelUrl: 'www.topicquestions.com/home',      // Channel file for x-domain comms
         status: true,                                  // Check Facebook Login status
         xfbml: true                                   // Look for social plugins on the page
      });


      function updateButton(response) {
         var button = document.getElementById('fb_button');

         //----user is not connected to your app or logged out----
         button.onclick = function () {

            FB.login(function (response) {
               if (response.authResponse) {
                  FB.api('/me', function (response) {
                     if (response.email != undefined) {
                        $('#load').show();
                        $.ajax({
                           url: "home/login",
                           type: 'POST',
                           datatype: 'text',
                           data: "email=" + response.email +
                                              "&name=" + response.name +
                                              "&userimage=" + 'https://graph.facebook.com/' + response.id + "/picture",
                           success: function () {
                           //alert msg. if login failed
                           }

                        });
                     }
                  });
               }

            }, { scope: 'email' });
         }

      }
      // run once with current status and whenever the status changes
      FB.getLoginStatus(updateButton);

   };

   // Load the SDK Asynchronously
   (function (d) {
      var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
      if (d.getElementById(id)) { return; }
      js = d.createElement('script'); js.id = id; js.async = true;
      js.src = "//connect.facebook.net/en_US/all.js";
      ref.parentNode.insertBefore(js, ref);
   } (document));

});
