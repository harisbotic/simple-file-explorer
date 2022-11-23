import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

import folderIcon from './icons/folder-icon.svg';
import videoIcon from './icons/video-icon.svg';


import DropdownSelect from "./DropdownSelect";
import { File } from "./App";

interface CreateFileProps {
    file: File;
    handleMove(file: File, destinationPath: string): void;
    handleNavigation(path: string): void;
    moveToOptions: Array<{ value: string, label: string }>;
}

const FileCard = (props: CreateFileProps) => {
    let navigate = useNavigate();
    const [movePathDestination, setmovePathDestination] = useState<string>(props.moveToOptions.at(0)?.value || "");

    const handleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setmovePathDestination(event.target.value);
    }

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        props.handleMove(props.file, movePathDestination);
    }

    const navigateTo = () => {
        if(props.file.type != "Folder") {
            return;
        }
        if (props.file.path == '/')
            props.file.path = '';
        const path = `${props.file.path}/${props.file.title}`;
        navigate(path);
        props.handleNavigation(path);
    }

    return (
        <div>
            <div onClick={() => navigateTo()}>
                <img src={props.file.type == "Folder" ? folderIcon : videoIcon} alt="fileIcon" />
                {props.file.title}
            </div>
            {movePathDestination && (
                <form onSubmit={(e) => handleSubmit(e)}>
                    <span>move to</span>
                    <DropdownSelect label={"Type"} name={"type"} changeHandler={handleChange} values={props.moveToOptions} currentValue={movePathDestination} />
                    <button type="submit" className="button">Move</button>
                </form>
            )}
        </div>
    );
}

export default FileCard;