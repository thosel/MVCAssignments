"use strict"

class People {
    constructor() {
    }

    getCountryCityIds(countryId) {
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/People/GetCountryCityIdsAjax?countryId=${countryId}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    if (response.status) {
                        throw new Error();
                    } else {
                        return response;
                    }
                })
                .then((responseString) => {
                    return JSON.parse(responseString);
                })
                .then((countryCityIds) => {
                    resolve(countryCityIds);
                })
                .catch(() => {
                    window.location.replace("/People/Index");
                });
        });
    }

    getCityCountryId(cityId) {
        return new Promise((resolve) => {
            const config = {
                method: 'POST',
                uri: `/People/GetCityCountryIdAjax?cityId=${cityId}`,
            };

            this.fetchRequest(config)
                .then((response) => {
                    if (response.status) {
                        throw new Error();
                    } else {
                        return response;
                    }
                })
                .then((responseString) => {
                    return JSON.parse(responseString);
                })
                .then((cityCountryId) => {
                    resolve(cityCountryId);
                })
                .catch(() => {
                    window.location.replace("/People/Index");
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
}