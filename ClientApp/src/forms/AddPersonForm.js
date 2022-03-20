import React, { useState } from "react"
import CitiesDropdown from "./dropdowns/CitiesDropdown"
import LanguagesDropdown from "./dropdowns/LanguagesDropdown"
import axios from 'axios'

const AddPersonForm = (props) => {
    const initialFormState = { id: null, name: '', phone: '', cityid: "", languageIds: [] }
    
    const [person, setPerson] = useState(initialFormState)
    let [cityid, setCityId] = useState("")
    let [languageIds, setLanguageIds] = useState([])
    /* let [citySelectClear, setCitySelectClear] = useState(null) */

    const handleInputChange = (event) => {
        const { name, value } = event.target

        setPerson({ ...person, [name]: value })
    }

    const updateCityId = (newCityId) => {
        setCityId(cityid = newCityId)
    }
    
    const updateLanguageIds = (newLanguageIds) => {
        setLanguageIds(languageIds = newLanguageIds)
    }
    /* const updateCitySelectClear = (clearFunction) => {
        setCitySelectClear(citySelectClear = clearFunction)
        console.log(citySelectClear)
    } */

    return (
        <form
            onSubmit={async event => {
                event.preventDefault()
                if (!person.name || !person.phone) return
                let personToPost = {
                    "name": person.name,
                    "phone": person.phone,
                    "cityid": cityid,
                    "languageids": languageIds/* [1001, 1002] */
                }
                const res = await axios.post('https://localhost:5001/PeopleAPI/CreatePerson', personToPost);
                if (res.status === 201) {
                    person.id = res.data
                    props.addPerson(person)
                    setPerson(initialFormState)
                    
                }

            }}
        >
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
            <CitiesDropdown /* updateCitySelectClear={updateCitySelectClear} */ updateCityId={updateCityId}/>
            <label>Languages</label>
            <LanguagesDropdown updateLanguageIds={updateLanguageIds} />
            <button>Add new person</button>
        </form>
    )
}

export default AddPersonForm