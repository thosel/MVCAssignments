import React, { useState, useEffect } from 'react'
import PeopleTable from './tables/PeopleTable'
import AddPersonForm from './forms/AddPersonForm'
import PersonDetails from './forms/PersonDetails'
import axios from 'axios'

const App = () => {
    const [people, setPeople] = useState([])

    useEffect(() => {
        fetch("https://localhost:5001/PeopleAPI/GetPeople")
            .then(res => res.json())
            .then((people) => {
                for (let index = 0; index < people.length; index++) {
                    people[index] = Object.keys(people[index]).reduce((accumulator, key) => {
                        accumulator[key.toLowerCase()] = people[index][key];
                        return accumulator;
                    }, {});
                }

                return people
            })
            .then(
                (result) => {
                    setPeople(result)
                },
                (error) => {
                    console.log(error)
                }
            )
    }, [])

    const [editing, setViewDetails] = useState(false)
    const initialFormState = { id: null, name: '', phone: '' }
    const [currentPerson, setCurrentPerson] = useState(initialFormState)
    const editRow = (person) => {
        setViewDetails(true)

        setCurrentPerson({ id: person.id, name: person.name, phone: person.phone, city: person.city, country: person.country, languages: person.languages })
    }

    const addPerson = (person) => {
        setPeople([...people, person])
    }

    const deletePerson = async (id) => {
        const res = await axios.get(`https://localhost:5001/PeopleAPI/DeletePerson/${id}`);
                if (res.status === 204) {
                    setPeople(people.filter((person) => person.id !== id))
                    setViewDetails(false)
                }
    }

    const updatePerson = (id, updatedPerson) => {
        setViewDetails(false)

        setPeople(people.map((person) => (person.id === id ? updatedPerson : person)))
    }

    return (
        <div className="container">
            <h1>People</h1>
            <div className="flex-row">
                <div className="flex-large">
                    {editing ? (
                        <div>
                            <h2>Person details</h2>
                            <PersonDetails deletePerson={deletePerson}
                                setViewDetails={setViewDetails}
                                currentPerson={currentPerson}
                                updatePerson={updatePerson}
                            />
                        </div>
                    ) : (
                        <div>
                            <h2>Add person</h2>
                            <AddPersonForm addPerson={addPerson} />
                        </div>
                    )}
                </div>
                <div className="flex-large">
                    <h2>View people</h2>
                    <PeopleTable people={people} editRow={editRow} deletePerson={deletePerson} />
                </div>
            </div>
        </div>
    )
}

export default App