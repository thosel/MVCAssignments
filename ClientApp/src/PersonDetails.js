import React, { useState, useEffect } from 'react'

const PersonDetails = (props) => {
  const [person, setPerson] = useState(props.personToView)

  useEffect(() => {
    setPerson(props.personToView)
  }, [props])

  return (    
    <div>
      <h4>{person.name}</h4>
      <div><strong>Phone: </strong>{person.phone}</div>
      <div><strong>City: </strong>{person.city}</div>
      <div><strong>Country: </strong>{person.country}</div>
      <div><strong>Languages:</strong>
        {
          person.languages.map((language) => <div>{language}</div>)
        }
      </div>
      <button
        onClick={() => props.deletePerson(person.id)}
        className="button muted-button"
      >
        Delete
      </button>
      <button
        onClick={() => props.setArePersonDetailsVisible(false)}
        className="button muted-button"
      >
        Go back
      </button>
    </div>
  )
}

export default PersonDetails