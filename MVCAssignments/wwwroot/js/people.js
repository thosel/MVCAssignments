"use strict"

class People {
    constructor(containerElement) {
        this.containerElement = containerElement;
        this.action;
        this.response;
        this.status;
        this.statusMessage;
    }

    searchPeople(searchString, caseSensitive) {
        this.action = "searchPeople";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/PeopleAjax/GetPeople?searchString=${searchString}&caseSensitive=${caseSensitive}`,
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

    createPerson(name, phone, city) {
        this.action = "createPerson";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/PeopleAjax/CreatePerson?name=${name}&phone=${phone}&city=${city}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    this.response = response;
                    if (response.statusMessage) {
                        this.status = "failure";
                        this.statusMessage = "Failed to create a new person!";
                        throw new Error();
                    } else {
                        this.status = "success";
                        this.statusMessage = "A new person was created successfully!";
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

    getPersonDetails(id) {
        this.action = "getPersonDetails";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/PeopleAjax/GetPersonDetails?id=${id}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    this.response = response;
                    if (response.errorMessage) {
                        this.status = "failure";
                        this.statusMessage = "Failed to find the person!";
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

    deletePerson(id) {
        this.action = "deletePerson";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/PeopleAjax/DeletePerson?id=${id}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    if (response.status != 200) {
                        this.status = "failure";
                        this.statusMessage = "Failed to delete the person!";
                        throw new Error();
                    } else {
                        this.status = "success";
                        this.statusMessage = "The person was deleted successfully!";
                        resolve();
                    }
                })
                .catch(() => {
                    this.updateView(undefined);
                });
        });
    }

    deletePersonLanguage(personId, languageId) {
        this.action = "deletePersonLanguage";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/PeopleAjax/DeletePersonLanguage?personId=${personId}&languageId=${languageId}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    this.response = response;
                    if (response.errorMessage) {
                        this.status = "failure";
                        this.statusMessage = "Failed to delete the person language!";
                        throw new Error();
                    } else {
                        this.status = "success";
                        this.statusMessage = "The person language was deleted successfully!";
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

    addPersonLanguage() {
        event.preventDefault();

        let personId = document.querySelector("#AddPersonLanguageViewModel_PersonId").value;
        let languageId = document.querySelector("#AddPersonLanguageViewModel_LanguageId").value;

        this.action = "addPersonLanguage";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/PeopleAjax/AddPersonLanguage?personId=${personId}&languageId=${languageId}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    this.response = response;
                    if (response.errorMessage) {
                        this.status = "failure";
                        this.statusMessage = "Failed to add the person language!";
                        throw new Error();
                    } else {
                        this.status = "success";
                        this.statusMessage = "The person language was added successfully!";
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

    updateCreatePersonForm() {
        let nameTextInput = document.querySelector("#CreatePersonViewModel_Name");
        let phoneTextInput = document.querySelector("#CreatePersonViewModel_Phone");
        let cityTextInput = document.querySelector("#CreatePersonViewModel_City");
        let nameValidationMessageSpan = document.querySelector("#name-validation-message");
        let phoneValidationMessageSpan = document.querySelector("#phone-validation-message");
        let cityValidationMessageSpan = document.querySelector("#city-validation-message");

        if (this.action === "createPerson") {
            if (this.response.nameValidationMessage !== "") {
                nameValidationMessageSpan.textContent = this.response.nameValidationMessage;
            } else {
                nameValidationMessageSpan.textContent = "";
            }

            if (this.response.phoneValidationMessage !== "") {
                phoneValidationMessageSpan.textContent = this.response.phoneValidationMessage;
            } else {
                phoneValidationMessageSpan.textContent = "";
            }

            if (this.response.cityValidationMessage !== "") {
                cityValidationMessageSpan.textContent = this.response.cityValidationMessage;
            } else {
                cityValidationMessageSpan.textContent = "";
            }

            if (this.status === "success") {
                nameTextInput.value = "";
                phoneTextInput.value = "";
                cityTextInput.value = "";
            }

        } else {
            nameValidationMessageSpan.textContent = "";
            phoneValidationMessageSpan.textContent = "";
            cityValidationMessageSpan.textContent = "";
            nameTextInput.value = "";
            phoneTextInput.value = "";
            cityTextInput.value = "";
        }
    }

    updateGetPeopleForm() {
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
            document.querySelector("#noscript-people-partial").classList.add("d-none");
            this.containerElement.textContent = "";
        }

        this.updateGetPeopleForm();

        this.updateCreatePersonForm();

        document.querySelector("#id-number").value = "";

        this.updateAlertMessage();

        if (htmlDocument !== undefined) {
            let body = htmlDocument.querySelector("body");
            this.containerElement.innerHTML = body.innerHTML;
        }

        if (document.querySelectorAll("#delete-person-language-link-div").length > 0) {
            document.querySelectorAll("#delete-person-language-link-div").forEach((link) => {
                link.classList.remove("d-none");
            });
        }
    }
}