"use strict"

window.onload = function () {
    if (citySelect.value > 0) {
        citySelectSelectedUpdate();
    }

    if (countrySelect.value > 0) {
        countrySelectSelectedUpdate();
    }

    languageSelectSelectedValues = Array.from(languagesSelect.querySelectorAll("option:checked"), element => element.value);
};


let people = new People();
let languageSelectSelectedValues;


let createPersonForm = document.querySelector("#update-person-form");

createPersonForm.addEventListener("reset", function () {
    setTimeout(() => {
        if (citySelect.value > 0) {
            citySelectSelectedUpdate();
        }

        if (countrySelect.value > 0) {
            countrySelectSelectedUpdate();
        }

        languagesSelect.querySelectorAll("option").forEach((option) => {

            if (languageSelectSelectedValues.indexOf(option.value) !== -1) {
                option.setAttribute('selected', 'selected');
            }
            else {
                option.removeAttribute('selected');
            }
        });

    }, 150);
}, false);


let citySelect = document.querySelector("#cities");

citySelect.addEventListener("change", function () {
    if (this.value == 0) {
        resetSelects();
    }
    if (this.value > 0) {
        citySelectSelectedUpdate();
    }
}, false);

let countrySelect = document.querySelector("#countries");

countrySelect.addEventListener("change", function () {
    if (this.value == 0) {
        resetSelects();
    }
    if (this.value > 0) {
        countrySelectSelectedUpdate();
    }
}, false);

let languagesSelect = document.querySelector("#languages");

languagesSelect.addEventListener("mousedown", function (event) {
    event.preventDefault();

    if (event.target.hasAttribute("selected")) {
        event.target.removeAttribute("selected");
    } else {
        event.target.setAttribute("selected", "");
    }
}, false);

let resetSelects = function () {
    countrySelect.querySelectorAll("option").forEach((option) => {
        if (option.classList.contains("d-none")) {
            option.classList.remove("d-none");
        }
    });
    citySelect.querySelectorAll("option").forEach((option) => {
        if (option.classList.contains("d-none")) {
            option.classList.remove("d-none");
        }
    });
    countrySelect.value = -1;
    citySelect.value = -1;
}

let citySelectSelectedUpdate = function () {
    people.getCityCountryId(citySelect.value)
        .then((cityCountryId) => {
            countrySelect.querySelectorAll("option").forEach((option) => {
                if (option.value > 0) {
                    if (option.value == cityCountryId) {
                        if (option.classList.contains("d-none")) {
                            option.classList.remove("d-none");
                        }
                    } else {
                        if (!option.classList.contains("d-none")) {
                            option.classList.add("d-none");
                        }
                    }
                    countrySelect.value = cityCountryId;
                }
            });
        });
    citySelect.querySelectorAll("option").forEach((option) => {
        if (option.value > 0) {
            if (option.value != citySelect.value) {
                if (!option.classList.contains("d-none")) {
                    option.classList.add("d-none");
                }
            }
        }
    });
}

let countrySelectSelectedUpdate = function () {
    people.getCountryCityIds(countrySelect.value)
        .then((countryCityIds) => {
            citySelect.querySelectorAll("option").forEach((option) => {
                countryCityIds.forEach((countryCityId) => {
                    if (option.value > 0) {
                        if (option.value == countryCityId) {
                            if (option.classList.contains("d-none")) {
                                option.classList.remove("d-none");
                            }
                        } else {
                            if (!option.classList.contains("d-none")) {
                                option.classList.add("d-none");
                            }
                        }
                    }
                });
                if (countryCityIds.length == 0 && option.value > 0) {
                    if (!option.classList.contains("d-none")) {
                        option.classList.add("d-none");
                    }
                }
            });
            countrySelect.querySelectorAll("option").forEach((option) => {
                if (option.value > 0) {
                    if (option.value != countrySelect.value) {
                        if (!option.classList.contains("d-none")) {
                            option.classList.add("d-none");
                        }
                    }
                }
            });
        });
}
