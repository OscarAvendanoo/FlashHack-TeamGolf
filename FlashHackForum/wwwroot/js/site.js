// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function toggleFavorite(button) {
    let icon = button.querySelector("i");

    if (icon.classList.contains("bi-heart")) {
        icon.classList.remove("bi-heart");
        icon.classList.add("bi-heart-fill");
        button.classList.add("active"); // Keep it filled after click
    } else {
        icon.classList.remove("bi-heart-fill");
        icon.classList.add("bi-heart");
        button.classList.remove("active"); // Revert if clicked again
    }
}


