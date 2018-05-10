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

$('.cartButton').click(function(id, qty) {

    let oldCart = window.sessionStorage.getItem('cart');
    
    console.log(oldCart);

    let newCart = { bookId: id,
                    qty: qty }

    console.log(newCart)

    newCart = newCart + oldCart

    console.log(newCart)

    window.sessionStorage.setItem('cart', newCart)


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

/* Deleting account */
$('.DeleteAccountButton').click(function() { 

});