import React, { Component } from 'react'
import Select from 'react-select'
import axios from 'axios'

export default class LanguagesDropdown extends Component {

    constructor(props) {
        super(props)
        this.state = {
            selectOptions: [],
            value: []
        }
    }

    async getOptions() {
        const res = await axios.get('https://localhost:5001/PeopleAPI/GetLanguages')
        const data = res.data

        const options = data.map(d => ({
            "value": d.LanguageId,
            "label": d.LanguageName
        }))

        this.setState({ selectOptions: options })

    }

    handleChange(e) {
        this.setState({ value: e })
        const returnIds = []
        e.forEach(language => {
            returnIds.push(language.value)
        });
        this.props.updateLanguageIds(returnIds)
        console.log(returnIds)
    }

    componentDidMount() {
        this.getOptions()
        this.props.updateLanguageIds([])
    }

    render() {
        return (
            <div>
                <Select options={this.state.selectOptions} onChange={this.handleChange.bind(this)} isMulti />
            </div>
        )
    }
}