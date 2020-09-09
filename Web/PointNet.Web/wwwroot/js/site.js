function isQuantityAvailable(availableQuantity) {
    var inputQuantity = document.getElementById("inputQuantity").value;

    if (inputQuantity > availableQuantity) {

            document.getElementById("popup").style.display = "block";
            document.getElementById("overlay").style.display = "block";
        }
}

function popupClose() {
    document.getElementById("popup").style.display = "none";
    document.getElementById("overlay").style.display = "none";
}

/*Background Text JS*/
$(function () {
    $('.intro').addClass('go');

    $('.reload').click(function () {
        $('.intro').removeClass('go').delay(200).queue(function (next) {
            $('.intro').addClass('go');
            next();
        });

    });
})