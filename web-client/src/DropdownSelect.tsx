import React from "react";

type DropdownSelectProps = {
    label: string,
    name: string,
    changeHandler: (event: React.ChangeEvent<HTMLSelectElement>) => void,
    values: Array<{ value: string, label: string }>,
    currentValue: string
}

const DropdownSelect = (props: DropdownSelectProps) => {
    return (
        <select
            name={props.name}
            onChange={props.changeHandler}
            value={props.currentValue}
        >
            {props.values.map((option, i) => (
                <option key={option.value + i} value={option.value}>{option.label}</option>
            ))}
        </select>
    );
}

export default DropdownSelect