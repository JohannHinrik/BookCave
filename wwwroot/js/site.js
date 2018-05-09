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


$('.cartButton').click(function() { 
    swal({
        title: "Success!",
        text: "Book has been added to your cart!",
        icon: "success",
        button: "Continue Browsing",
      });
});

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