"use strict"

class Cities {
    constructor(containerElement) {
        this.containerElement = containerElement;
        this.action;
        this.response;
        this.status;
        this.statusMessage;
    }

    searchCities(searchString, caseSensitive) {
        this.action = "searchCities";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/CitiesAjax/GetCities?searchString=${searchString}&caseSensitive=${caseSensitive}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    return response.text();
                })
                .then((responseString) => {
                    return this.parseHtmlDocumentFromString(responseString);
                })
                .then((htmlDocument) => {
                    resolve(htmlDocument);
                })
                .catch((error) => {
                    console.log(error);
                });
        });
    }

    createCity(name, country) {
        this.action = "createCity";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/CitiesAjax/CreateCity?name=${name}&country=${country}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    this.response = response;
                    if (response.statusMessage) {
                        this.status = "failure";
                        this.statusMessage = "Failed to create a new city!";
                        throw new Error();
                    } else {
                        this.status = "success";
                        this.statusMessage = "A new city was created successfully!";
                        return response.text();
                    }
                })
                .then((responseString) => {
                    return this.parseHtmlDocumentFromString(responseString);
                })
                .then((htmlDocument) => {
                    resolve(htmlDocument);
                })
                .catch(() => {
                    this.updateView(undefined);
                });
        });
    }

    getCityDetails(id) {
        this.action = "getCityDetails";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/CitiesAjax/GetCityDetails?id=${id}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    this.response = response;
                    if (response.errorMessage) {
                        this.status = "failure";
                        this.statusMessage = "Failed to find the city!";
                        throw new Error();
                    } else {
                        this.status = "success";
                        return response.text();
                    }
                })
                .then((responseString) => {
                    return this.parseHtmlDocumentFromString(responseString);
                })
                .then((htmlDocument) => {
                    resolve(htmlDocument);
                })
                .catch(() => {
                    this.updateView(undefined);
                });
        });
    }

    parseHtmlDocumentFromString(htmlString) {
        const parser = new DOMParser();
        return parser.parseFromString(htmlString, "text/html");
    }

    deleteCity(id) {
        this.action = "deleteCity";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/CitiesAjax/DeleteCity?id=${id}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    if (response.status != 200) {
                        this.status = "failure";
                        this.statusMessage = "Failed to delete the city!";
                        throw new Error();
                    } else {
                        this.status = "success";
                        this.statusMessage = "The city was deleted successfully!";
                        resolve();
                    }
                })
                .catch(() => {
                    this.updateView(undefined);
                });
        });
    }

    fetchRequest(config) {
        return new Promise((resolve, reject) => {
            window.fetch(config.uri, config)
                .then((response) => {
                    const contentType = response.headers.get('content-type');
                    if (contentType && contentType.indexOf('application/json') !== -1) {
                        response = response.json();
                    }
                    return response;
                })
                .then((data) => {
                    if (data.error) {
                        reject(data.error);
                    }
                    resolve(data);
                })
                .catch((error) => {
                    reject(error);
                });
        });
    }

    updateCreateCityForm() {
        let nameTextInput = document.querySelector("#CreateCityViewModel_Name");
        let countryTextInput = document.querySelector("#CreateCityViewModel_Country");
        let nameValidationMessageSpan = document.querySelector("#name-validation-message");
        let countryValidationMessageSpan = document.querySelector("#country-validation-message");

        if (this.action === "createCity") {
            if (this.response.nameValidationMessage !== "") {
                nameValidationMessageSpan.textContent = this.response.nameValidationMessage;
            } else {
                nameValidationMessageSpan.textContent = "";
            }

            if (this.response.countryValidationMessage !== "") {
                countryValidationMessageSpan.textContent = this.response.countryValidationMessage;
            } else {
                countryValidationMessageSpan.textContent = "";
            }

            if (this.status === "success") {
                nameTextInput.value = "";
                countryTextInput.value = "";
            }

        } else {
            nameValidationMessageSpan.textContent = "";
            countryValidationMessageSpan.textContent = "";
            nameTextInput.value = "";
            countryTextInput.value = "";
        }
    }

    updateGetCitiesForm() {
        let searchStringInput = document.querySelector("#search-text")
        searchStringInput.value = "";

        let checkBoxInput = document.querySelector("#case-sensitive");
        checkBoxInput.checked = false;
    }

    updateAlertMessage() {
        let statusDiv = document.querySelector("#status-div");
        let statusMessage = document.querySelector("#status-message");

        if (this.status === "success" && this.statusMessage !== undefined) {

            if (statusDiv.classList.contains("d-none")) {
                statusDiv.classList.remove("d-none");
            }

            if (statusDiv.classList.contains("alert-danger")) {
                statusDiv.classList.remove("alert-danger");
            }

            if (!statusDiv.classList.contains("alert")) {
                statusDiv.classList.add("alert");
            }

            if (!statusDiv.classList.contains("alert-success")) {
                statusDiv.classList.add("alert-success");
            }

            statusMessage.textContent = this.statusMessage;
        } else if (this.status === "failure" && this.statusMessage !== undefined) {
            if (statusDiv.classList.contains("d-none")) {
                statusDiv.classList.remove("d-none");
            }

            if (statusDiv.classList.contains("alert-success")) {
                statusDiv.classList.remove("alert-success");
            }

            if (!statusDiv.classList.contains("alert")) {
                statusDiv.classList.add("alert");
            }

            if (!statusDiv.classList.contains("alert-danger")) {
                statusDiv.classList.add("alert-danger");
            }

            statusMessage.textContent = this.statusMessage;
        } else {
            if (statusDiv.classList.contains("alert")) {
                statusDiv.classList.remove("alert");
            }

            if (statusDiv.classList.contains("alert-success")) {
                statusDiv.classList.remove("alert-success");
            }

            if (statusDiv.classList.contains("alert-danger")) {
                statusDiv.classList.remove("alert-danger");
            }

            if (!statusDiv.classList.contains("d-none")) {
                statusDiv.classList.add("d-none");
            }
        }

        this.status = undefined;
        this.statusMessage = undefined;
    }

    updateView(htmlDocument) {
        if (htmlDocument !== undefined) {
            document.querySelector("#noscript-cities-partial").classList.add("d-none");
            this.containerElement.textContent = "";
        }

        this.updateGetCitiesForm();

        this.updateCreateCityForm();

        document.querySelector("#id-number").value = "";

        this.updateAlertMessage();

        if (htmlDocument !== undefined) {
            let body = htmlDocument.querySelector("body");
            this.containerElement.innerHTML = body.innerHTML;
        }
    }
}