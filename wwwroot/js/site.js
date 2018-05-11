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

$('.search-button').addEventListener("keyup", function(event) {
    // 13 is the enterkey on keyboard
    if (event.keyCode === 13) {
    document.getElementById("search-button").click();
    }
});

/* Deleting account */
$('.DeleteAccountButton').click(function() { 

});

$("#Image-choosing input[type='radio']:checked").attr("checked", "checked");

/*$("#Image-choosing input[type='radio']:checked").css("border", '2px solid red');*/