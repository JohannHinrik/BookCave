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


$('.wishListButton').click(function() { 
    swal({
        title: "Success!",
        text: "Book has been added to your wishlist!",
        icon: "success",
        button: "Continue Browsing",
      });
});

$('.cartButton').click(function() { 
    swal({
        title: "Success!",
        text: "Book has been added to your cart!",
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