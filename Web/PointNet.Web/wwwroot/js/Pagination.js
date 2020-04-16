var el = document.getElementById('customjs').addEventListener('click', activeItem, false);;



function activeItem() {
    $(this).addClass('active').siblings().removeClass('active');
}