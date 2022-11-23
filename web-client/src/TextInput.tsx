import React from "react";

type TextInputProps = {
    label: string,
    name: string,
    changeHandler: (event: React.ChangeEvent<HTMLInputElement>) => void,
}

const TextInput = (props: TextInputProps) => {
    return (
        <label>
            {props.label}
            <input type="text"
                name={props.name}
                onChange={props.changeHandler}
            />
        </label>
    );
}

export default TextInput