﻿"use strict"

class Languages {
    constructor(containerElement) {
        this.containerElement = containerElement;
        this.action;
        this.response;
        this.status;
        this.statusMessage;
    }

    searchLanguages(searchString, caseSensitive) {
        this.action = "searchLanguages";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/LanguagesAjax/GetLanguages?searchString=${searchString}&caseSensitive=${caseSensitive}`,
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

    createLanguage(name) {
        this.action = "createLanguage";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/LanguagesAjax/CreateLanguage?name=${name}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    this.response = response;
                    if (response.statusMessage) {
                        this.status = "failure";
                        this.statusMessage = "Failed to create a new language!";
                        throw new Error();
                    } else {
                        this.status = "success";
                        this.statusMessage = "A new language was created successfully!";
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

    getLanguageDetails(id) {
        this.action = "getLanguageDetails";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/LanguagesAjax/GetLanguageDetails?id=${id}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    this.response = response;
                    if (response.errorMessage) {
                        this.status = "failure";
                        this.statusMessage = "Failed to find the language!";
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

    deleteLanguage(id) {
        this.action = "deleteLanguage";
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/LanguagesAjax/DeleteLanguage?id=${id}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    if (response.status != 200) {
                        this.status = "failure";
                        this.statusMessage = "Failed to delete the language!";
                        throw new Error();
                    } else {
                        this.status = "success";
                        this.statusMessage = "The language was deleted successfully!";
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

    updateCreateLanguageForm() {
        let nameTextInput = document.querySelector("#CreateLanguageViewModel_Name");
        let nameValidationMessageSpan = document.querySelector("#name-validation-message");

        if (this.action === "createLanguage") {
            if (this.response.nameValidationMessage !== "") {
                nameValidationMessageSpan.textContent = this.response.nameValidationMessage;
            } else {
                nameValidationMessageSpan.textContent = "";
            }

            if (this.status === "success") {
                nameTextInput.value = "";
            }

        } else {
            nameValidationMessageSpan.textContent = "";
            nameTextInput.value = "";
        }
    }

    updateGetLanguagesForm() {
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
            document.querySelector("#noscript-languages-partial").classList.add("d-none");
            this.containerElement.textContent = "";
        }

        this.updateGetLanguagesForm();

        this.updateCreateLanguageForm();

        document.querySelector("#id-number").value = "";

        this.updateAlertMessage();

        if (htmlDocument !== undefined) {
            let body = htmlDocument.querySelector("body");
            this.containerElement.innerHTML = body.innerHTML;
        }
    }
}