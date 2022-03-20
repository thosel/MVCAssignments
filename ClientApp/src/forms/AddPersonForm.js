import React, { useState } from "react"
import CitiesDropdown from "./dropdowns/CitiesDropdown"
import LanguagesDropdown from "./dropdowns/LanguagesDropdown"
import axios from 'axios'

const AddPersonForm = (props) => {
    const newPersonState = { id: null, name: '', phone: '', cityId: "", languageIds: [] }

    const [person, setPerson] = useState(newPersonState)
    let [cityId, setCityId] = useState("")
    let [languageIds, setLanguageIds] = useState([])
    let [validateInformation, SetValidateInformation] = useState("")
    const [toggleSelectRefresh, setToggleSelectRefresh] = useState(false)

    const handleInputChange = (event) => {
        const { name, value } = event.target
        setPerson({ ...person, [name]: value })
    }

    const updateCityId = (newCityId) => {
        setCityId(cityId = newCityId)
    }

    const updateLanguageIds = (newLanguageIds) => {
        setLanguageIds(newLanguageIds)
    }

    return (
        <form
            onSubmit={async event => {
                event.preventDefault()
                if (!person.name || cityId <= 0) {
                    if (!person.name && cityId <= 0) {
                        SetValidateInformation(validateInformation = "You must provide a name and a city!")
                    } else if (cityId <= 0) {
                        SetValidateInformation(validateInformation = "You must provide a city!")
                    } else if (!person.name) {
                        SetValidateInformation(validateInformation = "You must provide a name!")
                    }
                } else {
                    let personToPost = {
                        "name": person.name,
                        "phone": person.phone,
                        "cityid": cityId,
                        "languageids": languageIds
                    }
                    const response = await axios.post('https://localhost:5001/PeopleAPI/CreatePerson', personToPost);
                    if (response.status === 201) {
                        person.id = response.data
                        props.readPeople()
                        setPerson(newPersonState)
                        SetValidateInformation(validateInformation = "")
                        setToggleSelectRefresh(toggleSelectRefresh === true ? false : true)
                    }
                }
            }}
        >
            <label className="text-danger">{validateInformation}</label>
            <label>Name</label>
            <input
                type="text"
                name="name"
                value={person.name}
                onChange={handleInputChange} />
            <label>Phone</label>
            <input
                type="text"
                name="phone"
                value={person.phone}
                onChange={handleInputChange}
            />
            <label>City</label>
            <CitiesDropdown toggleSelectRefresh={toggleSelectRefresh} updateCityId={updateCityId} />
            <label>Languages</label>
            <LanguagesDropdown toggleSelectRefresh={toggleSelectRefresh} updateLanguageIds={updateLanguageIds} />
            <button>Add new person</button>
        </form>
    )
}

export default AddPersonForm