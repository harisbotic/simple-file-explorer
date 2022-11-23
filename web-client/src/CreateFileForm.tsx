import React, { useState } from "react";

import TextInput from "./TextInput";
import DropdownSelect from "./DropdownSelect";
import { File } from "./App";

const fileTypes = [
    { value: "Folder", label: "Folder" },
    { value: "Video", label: "Video" },
]

interface CreateFileProps {
    path: string;
    handleSubmit(file: File): void;
}

const CreateFileForm = (props: CreateFileProps) => {
    const [file, setFile] = useState<File>({
        title: "",
        type: "Folder",
        path: props.path,
    });

    const handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setFile({ ...file, [event.target.name]: event.target.value });
    }

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        props.handleSubmit({ ...file, path: props.path });
    }

    return (
        <div>
            <h5 className="title">Create New File</h5>
            <form onSubmit={(e) => handleSubmit(e)} className="form">
                <TextInput changeHandler={handleChange} label={"Title"} name={"title"} />
                <DropdownSelect label={"Type"} name={"type"} changeHandler={handleChange} values={fileTypes} currentValue={file.type} />
                <button type="submit" className="button">Create</button>
            </form>
        </div>
    );
}

export default CreateFileForm;