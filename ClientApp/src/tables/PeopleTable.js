import React, { useState } from 'react'

const PersonTable = (props) => {
    const [order, setOrder] = useState("ASC")
    const sorting = (column) => {
        if (order === "ASC") {
            const sorted = [...props.people].sort((a, b) =>
                a[column].toLowerCase() > b[column].toLowerCase() ? 1 : -1
            )
            props.replacePeople(sorted)
            setOrder("DSC")
        }
        if (order === "DSC") {
            const sorted = [...props.people].sort((a, b) =>
                a[column].toLowerCase() < b[column].toLowerCase() ? 1 : -1
            )
            props.replacePeople(sorted)
            setOrder("ASC")
        }
    }

    return (
        <div className='container'>
            <table className='table table-bordered'>
                <thead>
                    <tr>
                        <th onClick={(() => sorting("name"))}>
                            Name
                        </th>
                        <th>Phone</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {props.people.length > 0 ? (
                        props.people.map((person) => (
                            <tr key={person.id}>
                                <td>{person.name}</td>
                                <td>{person.phone}</td>
                                <td>
                                    <button
                                        onClick={() => {
                                            props.viewPersonDetails(person)
                                        }}
                                        className="button muted-button"
                                    >
                                        Details
                                    </button>
                                    <button
                                        onClick={() => props.deletePerson(person.id)}
                                        className="button muted-button"
                                    >
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan={3}>No people</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    )

}

export default PersonTable