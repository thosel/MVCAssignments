"use strict"

window.onload = function () {
    document.querySelector("#noscript-people-partial").classList.remove("d-none");
};



let peopleSection = document.querySelector("#people");
let people = new People(peopleSection);


let searchButton = document.querySelector("#search-button");

searchButton.addEventListener("click", function (event) {
    event.preventDefault();

    let searchStringInput = document.querySelector("#search-text")
    let searchString = searchStringInput.value;
    searchStringInput.value = "";

    let checkBoxInput = document.querySelector("#case-sensitive");
    let caseSensitive = checkBoxInput.checked;
    checkBoxInput.checked = false;

    people.searchPeople(searchString, caseSensitive)
        .then((htmlDocument) => {
            people.updateView(htmlDocument);
        });
}, false);


let createButton = document.querySelector("#create-button");

createButton.addEventListener("click", function (event) {
    event.preventDefault();

    let nameInput = document.querySelector("#CreatePersonViewModel_Name")
    let name = nameInput.value;

    let phoneInput = document.querySelector("#CreatePersonViewModel_Phone")
    let phone = phoneInput.value;

    let cityInput = document.querySelector("#CreatePersonViewModel_City")
    let city = cityInput.value;

    people.createPerson(name, phone, city)
        .then((htmlDocument) => {
            people.updateView(htmlDocument);
        });
}, false);


let getPeopleButton = document.querySelector("#get-people-button");

getPeopleButton.addEventListener("click", function (event) {
    event.preventDefault();

    people.searchPeople("", "")
        .then((htmlDocument) => {
            people.updateView(htmlDocument);
        });
}, false);

let getPersonDetailsButton = document.querySelector("#get-person-details-button");

getPersonDetailsButton.addEventListener("click", function (event) {
    event.preventDefault();

    if (isNumberInputValid()) {
        let idInput = document.querySelector("#id-number")
        let id = idInput.value;

        people.getPersonDetails(id)
            .then((htmlDocument) => {
                people.updateView(htmlDocument);
            });
    }

}, false);

let deletePersonButton = document.querySelector("#delete-person-button");

deletePersonButton.addEventListener("click", function (event) {
    event.preventDefault();

    if (isNumberInputValid()) {
        let idInput = document.querySelector("#id-number")
        let id = idInput.value;

        people.deletePerson(id)
            .then(() => {
                people.searchPeople("", "")
                    .then((htmlDocument) => {
                        people.updateView(htmlDocument);
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

