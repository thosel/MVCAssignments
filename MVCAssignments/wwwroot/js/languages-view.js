"use strict"

window.onload = function () {
    document.querySelector("#noscript-languages-partial").classList.remove("d-none");
};



let languagesSection = document.querySelector("#languages");
let languages = new Languages(languagesSection);


let searchButton = document.querySelector("#search-button");

searchButton.addEventListener("click", function (event) {
    event.preventDefault();

    let searchStringInput = document.querySelector("#search-text")
    let searchString = searchStringInput.value;

    let checkBoxInput = document.querySelector("#case-sensitive");
    let caseSensitive = checkBoxInput.checked;

    languages.searchLanguages(searchString, caseSensitive)
        .then((htmlDocument) => {
            languages.updateView(htmlDocument);
        });
}, false);


let createButton = document.querySelector("#create-button");

createButton.addEventListener("click", function (event) {
    event.preventDefault();

    let nameInput = document.querySelector("#CreateLanguageViewModel_Name")
    let name = nameInput.value;

    languages.createLanguage(name)
        .then((htmlDocument) => {
            languages.updateView(htmlDocument);
        });
}, false);


let getLanguagesButton = document.querySelector("#get-languages-button");

getLanguagesButton.addEventListener("click", function (event) {
    event.preventDefault();

    languages.searchLanguages("", "")
        .then((htmlDocument) => {
            languages.updateView(htmlDocument);
        });
}, false);

let getLanguageDetailsButton = document.querySelector("#get-language-details-button");

getLanguageDetailsButton.addEventListener("click", function (event) {
    event.preventDefault();

    if (isNumberInputValid()) {
        let idInput = document.querySelector("#id-number")
        let id = idInput.value;

        languages.getLanguageDetails(id)
            .then((htmlDocument) => {
                languages.updateView(htmlDocument);
            });
    }

}, false);

let deleteLanguageButton = document.querySelector("#delete-language-button");

deleteLanguageButton.addEventListener("click", function (event) {
    event.preventDefault();

    if (isNumberInputValid()) {
        let idInput = document.querySelector("#id-number")
        let id = idInput.value;

        languages.deleteLanguage(id)
            .then(() => {
                languages.searchLanguages("", "")
                    .then((htmlDocument) => {
                        languages.updateView(htmlDocument);
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

