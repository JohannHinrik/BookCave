/* $('.cartButton').delegate(".bookTitle", 'click', function(){
    swal({
        title: "Book has been added to your cart!",
        text: "Do you wish to go to cart or continue shopping?",
        icon: "success",
        button: ["Continue shopping", "Go to cart"],
  }).then((value) =>{
      if(value == null)
      {
          swal("Continue shopping");
      }
      else
      {
          swal("Go to cart");
      }
  });
}); */
$('.search-button').addEventListener("keyup", function(event) {
    // 13 is the enterkey on keyboard
    if (event.keyCode === 13) {
    document.getElementById("search-button").click();
    }
  });

$('.cartButton').click(function(id) {
    AddToCart(id);
    swal({
        title: "Success!",
        text: "Book has been added to your cart!",
        icon: "success",
        button: "Continue Browsing",
      });

});

function AddToCart(id) {
    $.ajax({
      url: '@Url.Action("AddToCart", "CartController")',
      type: 'GET',
      dataType: 'json',
      cache: false,
      data: { 'id': id },
      success: function (results) {
           alert(results)
      },
      error: function () {
       alert('Error occured');
      }
      });
      }

$('.wishListButton').click(function() { 
    swal({
        title: "Success!",
        text: "Book has been added to your wishlist!",
        icon: "success",
        button: "Continue Browsing",
      });
});

$('.updateProfile').click(function() { 
    swal({
        title: "Success!",
        text: "Profile Updated!",
        icon: "success",
        button: "Continue",
      });
});

/* Deleting account */
$('.DeleteAccountButton').click(function() { 

});