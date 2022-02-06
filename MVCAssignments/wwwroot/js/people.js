"use strict"

class People {
    constructor(containerElement) {
        this.containerElement = containerElement;
        this.status;
        this.statusMessage;
        this.response;
    }

    searchPeople(searchString, caseSensitive) {
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
                    const parser = new DOMParser();
                    return parser.parseFromString(responseString, "text/html");
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
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/PeopleAjax/CreatePerson?name=${name}&phone=${phone}&city=${city}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    if (response.errorMessage) {
                        this.response = response;
                        this.status = "failure";
                        this.statusMessage = "Failed to create a new person!";
                        throw new Error(response.errorMessage);
                    } else {
                        this.status = "success";
                        this.statusMessage = "A new person was created successfully!";
                        this.response = undefined;
                        return response.text();
                    }
                })
                .then((responseString) => {
                    const parser = new DOMParser();
                    return parser.parseFromString(responseString, "text/html");
                })
                .then((htmlDocument) => {
                    resolve(htmlDocument);
                })
                .catch((error) => {
                    console.log(error);
                    this.updateView(undefined);
                });
        });
    }

    getPersonDetails(id) {
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/PeopleAjax/GetPersonDetails?id=${id}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    if (response.errorMessage) {
                        this.response = response;
                        this.status = "failure";
                        this.statusMessage = "Failed to find the person!";
                        throw new Error(response.errorMessage);
                    } else {
                        this.status = "success";
                        this.response = undefined;
                        return response.text();
                    }
                })
                .then((responseString) => {
                    const parser = new DOMParser();
                    return parser.parseFromString(responseString, "text/html");
                })
                .then((htmlDocument) => {
                    resolve(htmlDocument);
                })
                .catch((error) => {
                    console.log(error);
                    this.updateView(undefined);
                });
        });
    }

    deletePerson(id) {
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
                        throw new Error(response.statusText);
                    } else {
                        resolve();
                    }
                })
                .catch((error) => {
                    console.log(error);
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

    updateView(htmlDocument) {
        if (htmlDocument !== undefined) {
            document.querySelector("#noscript-people-partial").classList.add("d-none");
            this.containerElement.textContent = "";
            document.querySelector("#id-number").value = "";
        }

        if (this.response !== undefined) {
            if (this.response.nameValidationMessage !== "") {
                document.querySelector("#name-validation-message").textContent = this.response.nameValidationMessage;
            } else {
                document.querySelector("#name-validation-message").textContent = "";
            }

            if (this.response.phoneValidationMessage !== "") {
                document.querySelector("#phone-validation-message").textContent = this.response.phoneValidationMessage;
            } else {
                document.querySelector("#phone-validation-message").textContent = "";
            }

            if (this.response.cityValidationMessage !== "") {
                document.querySelector("#city-validation-message").textContent = this.response.cityValidationMessage;
            } else {
                document.querySelector("#city-validation-message").textContent = "";
            }

            this.responseStatusMessage = "";
        }

        let statusDiv = document.querySelector("#status-div");
        let statusMessage = document.querySelector("#status-message");

        if (this.status === "success" && this.statusMessage !== undefined) {
            document.querySelector("#name-validation-message").textContent = "";
            document.querySelector("#phone-validation-message").textContent = "";
            document.querySelector("#city-validation-message").textContent = "";
            document.querySelector("#CreatePersonViewModel_Name").value = "";
            document.querySelector("#CreatePersonViewModel_Phone").value = "";
            document.querySelector("#CreatePersonViewModel_City").value = "";

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

            this.statusMessage = undefined;
            this.status = "";
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

            this.statusMessage = undefined;
            this.status = "";
        } else {
            document.querySelector("#name-validation-message").textContent = "";
            document.querySelector("#phone-validation-message").textContent = "";
            document.querySelector("#city-validation-message").textContent = "";
            statusDiv.classList.remove("alert");

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

        if (htmlDocument !== undefined) {
            let body = htmlDocument.querySelector("body");
            this.containerElement.innerHTML = body.innerHTML;
        }
    }
}