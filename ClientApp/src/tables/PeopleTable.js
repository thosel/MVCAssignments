import React, {useState} from 'react'

const PersonTable = (props) => {
    const [currentSort, setCurrentSort] = useState("default")
    
    const onSortChange = () => {
		let nextSort;

		if (currentSort === 'down') nextSort = 'up';
		else if (currentSort === 'up') nextSort = 'default';
		else if (currentSort === 'default') nextSort = 'down';

        setCurrentSort(nextSort)
        console.log(currentSort)
	};
    const sortTypes = {
        up: (a, b) => a.net_worth - b.net_worth,
        down: (a, b) => b.net_worth - a.net_worth,
        default: (a, b) => a
    }
    return (
        <table>
            <thead>
                <tr>
                    <th>
                        <button onClick={onSortChange}>Name</button>
                    </th>
                    <th>Phone</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>

                {props.people.length > 0 ? (
                    props.people.sort(sortTypes[currentSort]).map((person) => (
                        <tr key={person.id}>
                            <td>{person.name}</td>
                            <td>{person.phone}</td>
                            <td>
                                <button
                                    onClick={() => {
                                        props.editRow(person)
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
        </table>)

}

export default PersonTable