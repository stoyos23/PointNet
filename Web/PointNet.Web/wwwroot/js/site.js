

function isQuantityAvailable(availableQuantity) {
    var inputQuantity = document.getElementById("inputQuantity").value;

    if (inputQuantity > availableQuantity) {

            document.getElementById("popup").style.display = "block";
            document.getElementById("overlay").style.display = "block";
        }
}
// Popup Close
function popupClose() {
    document.getElementById("popup").style.display = "none";
    document.getElementById("overlay").style.display = "none";
}

