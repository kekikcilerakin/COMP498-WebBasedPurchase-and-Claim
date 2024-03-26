$(document).ready(function () {
  $.get("http://localhost:3000/items", function (data) {
    console.log("data", data);
    // data.forEach(function (item) {
    //   $("#items-list").append(`<li>${item.name} - ${item.cost} - ${item.imageUrl}</li>`);
    // });
  });
});
