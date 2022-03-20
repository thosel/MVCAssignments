import React, { useState, useEffect } from 'react'
import PeopleTable from './tables/PeopleTable'
import AddPersonForm from './forms/AddPersonForm'
import PersonDetails from './PersonDetails'
import axios from 'axios'

const App = () => {
    let [people, setPeople] = useState([])
    const [arePersonDetailsVisible, setArePersonDetailsVisible] = useState(false)
    const [personToView, setPersonToView] = useState({ id: null, name: '', phone: '' })
    const viewPersonDetails = (person) => {
        setArePersonDetailsVisible(true)

        setPersonToView({
            id: person.id,
            name: person.name,
            phone: person.phone,
            city: person.city,
            country: person.country,
            languages: person.languages
        })
    }

    useEffect(() => {
        readPeople()
    }, [])

    const readPeople = async () => {
        const response = await axios.get("https://localhost:5001/PeopleAPI/GetPeople")
        if(response.status === 200){
            let responseData = response.data
            for (let index = 0; index < responseData.length; index++) {
                responseData[index] = Object.keys(responseData[index]).reduce((accumulator, key) => {
                    accumulator[key.toLowerCase()] = responseData[index][key];
                    return accumulator;
                }, {});
            }
            setPeople(responseData)
        }            
    }

    const replacePeople = (newPeople) => {
        setPeople(newPeople)
    }

    const deletePerson = async (id) => {
        const response = await axios.get(`https://localhost:5001/PeopleAPI/DeletePerson/${id}`);
        if (response.status === 204) {
            setPeople(people.filter((person) => person.id !== id))
            setArePersonDetailsVisible(false)
        }
    }

    return (
        <div className="container">
            <h1>People</h1>
            <div className="flex-row">
                <div className="flex-large">
                    {arePersonDetailsVisible ? (
                        <div>
                            <h2>Person details</h2>
                            <PersonDetails
                                deletePerson={deletePerson}
                                setArePersonDetailsVisible={setArePersonDetailsVisible}
                                personToView={personToView}
                            />
                        </div>
                    ) : (
                        <div>
                            <h2>Add person</h2>
                            <AddPersonForm readPeople={readPeople} />
                        </div>
                    )}
                </div>
                <div className="flex-large">
                    <h2>View people</h2>
                    <PeopleTable people={people} viewPersonDetails={viewPersonDetails} deletePerson={deletePerson} replacePeople={replacePeople} />
                </div>
            </div>
        </div>
    )
}

export default App