$(document).ready(function () {
    function getAccounts() {
        $.ajax({
            url: "http://localhost:54567/api/values",
            type: "GET",
            dataType: "json",
        })
          .done(function (json) {
              $("<h1>").text(json.title).appendTo("body");
              $("<div class=\"content\">").html(json.html).appendTo("body");
          })
          .fail(function (xhr, status, errorThrown) {
              alert("Sorry, there was a problem!");
              console.log("Error: " + errorThrown);
              console.log("Status: " + status);
              console.dir(xhr);
          })
          .always(function (xhr, status) {
              alert("The request is complete!");
          });
    }
});