$(function () {
    //Document ready -> link up remove event handler
    $('#RemoveLink').click(function () {
        //Get the id from the link
        var recordToDelete = $(this).attr("data-id");

        if (recordToDelete != '') {
            //Perform the Ajax post
            $.post("/ShoppingCart/RemoveFromCart", { "id": recordToDelete },
                function (data) {
                    //Successful request gets here
                    //Update the page element
                    if (data.ItemCount == 0) {
                        $('#row-' + data.DeletedID).fadeOut('slow');
                    } else {
                        $('#item-count' + data.DeletedID).text(data.ItemCount);
                    }

                    $('#cart-total').text(data.CartTotal);
                    $('#update-message').text(data.Message);
                    $('#cart-status').text('Cart (' + data.CartCount + ')');
                }
            );
        }
    });
});

function handleUpdate() {
    //Load and deserialize the returned Json data
    var json = context.get_data();
    var data = Sys.Serialization.JavaScriptSerializer.deserialize(json);

    //Update the page elements
    if (data.ItemCount == 0) {
        $('#row-' + data.DeletedID).fadeOut('slow');
    } else {
        $('#item-count' + data.DeletedID).text(data.ItemCount);
    }

    $('#cart-total').text(data.CartTotal);
    $('#update-message').text(data.Message);
    $('#cart-status').text('Cart (' + data.CartCount + ')');
}

$(document).ready(function () {
    $('#myCarousel').carousel({
        interval: 5000
    })
    $('.fdi-Carousel .item').each(function () {
        var next = $(this).next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }
        next.children(':first-child').clone().appendTo($(this));

        if (next.next().length > 0) {
            next.next().children(':first-child').clone().appendTo($(this));
        }
        else {
            $(this).siblings(':first').children(':first-child').clone().appendTo($(this));
        }
    });
});

$('[data-toggle="tooltip"]').tooltip();