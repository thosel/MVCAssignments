import React, { Component } from 'react'
import Select from 'react-select'
import axios from 'axios'

export default class LanguagesDropdown extends Component {

    constructor(props) {
        super(props)
        this.state = {
            selectOptions: [],
            value: [],
            selectedValues: []
        }
    }

    componentWillReceiveProps(props) {
        if (props.toggleSelectRefresh !== this.props.toggleSelectRefresh) {
            this.setState({ selectedValues: [] })
        }
    }

    async getOptions() {
        const res = await axios.get('https://localhost:5001/PeopleAPI/GetLanguages')
        const responseData = res.data

        const options = responseData.map(data => ({
            "value": data.LanguageId,
            "label": data.LanguageName
        }))

        this.setState({ selectOptions: options })

    }

    handleChange(event) {
        this.setState({ value: event })
        const returnIds = []
        event.forEach(language => {
            returnIds.push(language.value)
        });
        this.props.updateLanguageIds(returnIds)
        this.setState({ selectedValues: event })
    }

    componentDidMount() {
        this.getOptions()
        this.props.updateLanguageIds([])
    }

    render() {
        return (
            <div>
                <Select value={this.state.selectedValues} options={this.state.selectOptions} onChange={this.handleChange.bind(this)} isMulti />
            </div>
        )
    }
}