"use strict"

window.onload = function () {
    document.querySelector("#noscript-countries-partial").classList.remove("d-none");
};



let countriesSection = document.querySelector("#countries");
let countries = new Countries(countriesSection);


let searchButton = document.querySelector("#search-button");

searchButton.addEventListener("click", function (event) {
    event.preventDefault();

    let searchStringInput = document.querySelector("#search-text")
    let searchString = searchStringInput.value;

    let checkBoxInput = document.querySelector("#case-sensitive");
    let caseSensitive = checkBoxInput.checked;

    countries.searchCountries(searchString, caseSensitive)
        .then((htmlDocument) => {
            countries.updateView(htmlDocument);
        });
}, false);


let createButton = document.querySelector("#create-button");

createButton.addEventListener("click", function (event) {
    event.preventDefault();

    let nameInput = document.querySelector("#CreateCountryViewModel_Name")
    let name = nameInput.value;

    countries.createCountry(name)
        .then((htmlDocument) => {
            countries.updateView(htmlDocument);
        });
}, false);


let getCountriesButton = document.querySelector("#get-countries-button");

getCountriesButton.addEventListener("click", function (event) {
    event.preventDefault();

    countries.searchCountries("", "")
        .then((htmlDocument) => {
            countries.updateView(htmlDocument);
        });
}, false);

let getCountryDetailsButton = document.querySelector("#get-country-details-button");

getCountryDetailsButton.addEventListener("click", function (event) {
    event.preventDefault();

    if (isNumberInputValid()) {
        let idInput = document.querySelector("#id-number")
        let id = idInput.value;

        countries.getCountryDetails(id)
            .then((htmlDocument) => {
                countries.updateView(htmlDocument);
            });
    }

}, false);

let deleteCountryButton = document.querySelector("#delete-country-button");

deleteCountryButton.addEventListener("click", function (event) {
    event.preventDefault();

    if (isNumberInputValid()) {
        let idInput = document.querySelector("#id-number")
        let id = idInput.value;

        countries.deleteCountry(id)
            .then(() => {
                countries.searchCountries("", "")
                    .then((htmlDocument) => {
                        countries.updateView(htmlDocument);
                    });
            });
    }

}, false);

let idInput = document.querySelector('#id-number');

idInput.onkeydown = function (event) {
    if (!((event.keyCode > 95 && event.keyCode < 106)
        || (event.keyCode > 47 && event.keyCode < 58)
        || event.keyCode == 8)) {
        return false;
    }
}

let isNumberInputValid = function () {
    let idInput = document.querySelector('#id-number')

    if (idInput.value !== undefined && idInput.value > 0) {
        return true;
    } else {
        return false;
    }
}

