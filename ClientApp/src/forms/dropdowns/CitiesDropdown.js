import React, { Component } from 'react'
import Select from 'react-select'
import axios from 'axios'

export default class CitiesDropdown extends Component {

    constructor(props) {
        super(props)
        this.state = {
            selectOptions: [],
            id: "",
            name: "",
            selectedValue: 0
        }
    }

    componentWillReceiveProps(props) {
        if (props.toggleSelectRefresh !== this.props.toggleSelectRefresh) {
            this.setState({ selectedValue: 0 })
        }
    }

    async getOptions() {
        const response = await axios.get('https://localhost:5001/PeopleAPI/GetCities')
        const responseData = response.data

        const options = responseData.map(data => ({
            "value": data.CityId,
            "label": data.CityName,
            "optgroup": data.CountryName
        }))

        const groups = []
        options.forEach(option => {
            if (!groups.includes(option.optgroup)) {
                groups.push(option.optgroup)
            }
        })
        const groupedOptions = [];
        groups.forEach(group => {
            let groupOptions = []
            options.forEach(option => {
                if (option.optgroup === group) {
                    groupOptions.push({
                        "value": option.value,
                        "label": option.label
                    })
                }
            })

            let groupObject = {
                label: group,
                options: groupOptions
            }
            groupedOptions.push(groupObject)
        })

        options.forEach(option => {
            if (!groupedOptions[option.optgroup]) {
                groupedOptions[option.optgroup] = []
            }
            groupedOptions[option.optgroup].push({
                value: option.value,
                label: option.label
            })
        })

        this.setState({ selectOptions: groupedOptions })

    }

    handleChange(event) {
        this.setState({ id: event.value, name: event.label })
        this.setState({ selectedValue: this.state.value })
        this.props.updateCityId(event.value)
    }

    componentDidMount() {
        this.getOptions()
    }

    render() {
        return (
            <div>
                <Select value={this.state.selectedValue} options={this.state.selectOptions} onChange={this.handleChange.bind(this)} />
            </div>
        )
    }
}