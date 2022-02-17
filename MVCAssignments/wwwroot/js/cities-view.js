"use strict"

window.onload = function () {
    document.querySelector("#noscript-cities-partial").classList.remove("d-none");
};



let citiesSection = document.querySelector("#cities");
let cities = new Cities(citiesSection);


let searchButton = document.querySelector("#search-button");

searchButton.addEventListener("click", function (event) {
    event.preventDefault();

    let searchStringInput = document.querySelector("#search-text")
    let searchString = searchStringInput.value;

    let checkBoxInput = document.querySelector("#case-sensitive");
    let caseSensitive = checkBoxInput.checked;

    cities.searchCities(searchString, caseSensitive)
        .then((htmlDocument) => {
            cities.updateView(htmlDocument);
        });
}, false);


let createButton = document.querySelector("#create-button");

createButton.addEventListener("click", function (event) {
    event.preventDefault();

    let nameInput = document.querySelector("#CreateCityViewModel_Name")
    let name = nameInput.value;

    let countryInput = document.querySelector("#CreateCityViewModel_Country")
    let country = countryInput.value;

    cities.createCity(name, country)
        .then((htmlDocument) => {
            cities.updateView(htmlDocument);
        });
}, false);


let getCitiesButton = document.querySelector("#get-cities-button");

getCitiesButton.addEventListener("click", function (event) {
    event.preventDefault();

    cities.searchCities("", "")
        .then((htmlDocument) => {
            cities.updateView(htmlDocument);
        });
}, false);

let getCityDetailsButton = document.querySelector("#get-city-details-button");

getCityDetailsButton.addEventListener("click", function (event) {
    event.preventDefault();

    if (isNumberInputValid()) {
        let idInput = document.querySelector("#id-number")
        let id = idInput.value;

        cities.getCityDetails(id)
            .then((htmlDocument) => {
                cities.updateView(htmlDocument);
            });
    }

}, false);

let deleteCityButton = document.querySelector("#delete-city-button");

deleteCityButton.addEventListener("click", function (event) {
    event.preventDefault();

    if (isNumberInputValid()) {
        let idInput = document.querySelector("#id-number")
        let id = idInput.value;

        cities.deleteCity(id)
            .then(() => {
                cities.searchCities("", "")
                    .then((htmlDocument) => {
                        cities.updateView(htmlDocument);
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

