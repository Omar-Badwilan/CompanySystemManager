// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*
document.addEventListener("DOMContentLoaded", () => {

    const form = document.getElementById("searchForm");
    const searchInput = document.getElementById("searchInp");
    const employeesTable = document.getElementById("employeesTable");

    form.addEventListener("submit", function (e) {
        e.preventDefault();

        const searchValue = searchInput.value.trim();

        fetch(/Employee/Search ? search = ${ encodeURIComponent(searchValue) })
            .then(res => res.text())
            .then(html => employeesTable.innerHTML = html)
            .catch(err => console.error(err));
    });
});*/

document.addEventListener("DOMContentLoaded", () => {
    const searchInput = document.getElementById("searchInp");
    const employeesTable = document.getElementById("employeesTable");

    let timer;
    searchInput.addEventListener("input", function () {
        clearTimeout(timer);

        // add a small delay so it doesn’t fire on every keystroke instantly
        timer = setTimeout(() => {
            const searchValue = searchInput.value.trim();

            fetch(`/Employee/Index?search=${encodeURIComponent(searchValue)}`, {
                headers: { "X-Requested-With": "XMLHttpRequest" }
            })
                .then(res => res.text())
                .then(html => {
                    employeesTable.innerHTML = html;
                })
                .catch(err => console.error(err));
        }, 300); // wait 300ms after typing stops
    });
});
