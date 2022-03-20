import React, { Component } from 'react'
import Select from 'react-select'
import axios from 'axios'

export default class CitiesDropdown extends Component {

    constructor(props) {
        super(props)
        this.state = {
            selectOptions: [],
            id: "",
            name: ''
        }
    }

    async getOptions() {
        const res = await axios.get('https://localhost:5001/PeopleAPI/GetCities')
        const data = res.data

        const options = data.map(d => ({
            "value": d.CityId,
            "label": d.CityName,
            "optgroup": d.CountryName
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

    handleChange(e) {
        this.setState({ id: e.value, name: e.label })
        this.props.updateCityId(e.value)
        /* this.props.updateCitySelectClear(this.clear) */
    }

    componentDidMount() {
        this.getOptions()
        
    }

    /* clear() {
        //this.state.value.clear();
        console.log("Funkar")
    } */

    render() {
        return (
            <div>
                <Select options={this.state.selectOptions} onChange={this.handleChange.bind(this)} />
            </div>
        )
    }
}