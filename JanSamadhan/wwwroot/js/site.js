// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // Get the modal
    var modal = document.getElementById("imageModal");

    // Get the image and insert it inside the modal
    var modalImg = document.getElementById("modalImage");

    // Use event delegation for dynamically added images
    $(document).on('click', '.reply-body img', function(){
        modal.style.display = "block";
        modalImg.src = this.src;
    });

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("image-modal-close")[0];

    // When the user clicks on <span> (x), close the modal
    if (span) {
        span.onclick = function() {
            modal.style.display = "none";
        }
    }

    // When the user clicks anywhere outside of the modal content, close it
    window.onclick = function(event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
});
