import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';

import CreateFileForm from './CreateFileForm';
import FileCard from './FileCard';

import './App.css'
import BackButton from './BackButton';

export type File = {
  title: string;
  type: "Folder" | "Video";
  path: string;
}

function App() {
  const location = useLocation();

  const [path, setPath] = useState<string>(location.pathname || "/");

  const [files, setFiles] = useState<File[]>([]);

  useEffect(() => {
    async function fetchFiles() {
      try {
        const url = `http://localhost:5225/files?path=${path}`;
        const response = await fetch(url);
        setFiles(await response.json());
      } catch (error) {
        alert(`Invalid path ${path} - it doesn't exist`);
      }
    }

    fetchFiles();
  }, [path]);

  const createFile = (file: File) => {
    //can only contain word characters (a-z, 0-9, underscore), dash and dots
    const regex = /^[\w-.]*$/;

    if (file.title.replaceAll(' ', '').match(regex) == null) {
      alert("Invalid title name. It can only contain word characters (a-z, 0-9, underscore), dash and dot")
      return;
    }

    if (files.filter(x => x.title == file.title && x.type == file.type).length > 0) {
      alert("A file with same title and type already exists here");
      return;
    }

    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(file)
    };
    fetch('http://localhost:5225/files', requestOptions)
      .then(response => {
        if (!response.ok) {
          const error = response.status;
          alert("API call error " + error);
          return Promise.reject(error);
        }

        setFiles([...files, file])
      })
      .catch(error => {
        alert('There was an error! ' + error);
      });
  };

  const moveFile = (file: File, destinationPath: string) => {
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ ...file, destinationPath })
    };
    fetch('http://localhost:5225/files/move_to', requestOptions)
      .then(response => {
        if (!response.ok) {
          const error = response.status;
          alert("API call error " + error);
          return Promise.reject(error);
        }
        const newFiles = files.filter(f => f != file);
        setFiles(newFiles);
      })
      .catch(error => {
        alert('There was an error! ' + error);
      });
  }

  const getMoveToOptions = (file: File, allFiles: File[]) => {
    const foldersAvailable = allFiles.filter((f) => f.type == "Folder" && (file.type != "Folder" || f.title != file.title));

    const moveOptions = foldersAvailable.map(x => {
      return { value: `${x.path}/${x.title}`, label: x.title };
    });
    if (location.pathname.split('/').filter(n => n).length > 0) {
      moveOptions.unshift({ value: path.substring(0, path.lastIndexOf('/')) || "/", label: "...Parent" })
    }
    return moveOptions;
  }

  const isAtRoot = path == '/';
  return (
    <div style={{ marginLeft: "2px" }}>
      <CreateFileForm path={path} handleSubmit={createFile}></CreateFileForm>
      <div className="fileListcontainer">
        <h5 className="title">Files in .{path}</h5>
        {!isAtRoot && <BackButton handleNavigation={setPath} />}
        {files && files.map((file, index) =>
          <FileCard key={`${index}-${files.length}`} file={file} moveToOptions={getMoveToOptions(file, files)} handleMove={moveFile} handleNavigation={setPath}></FileCard>)}
      </div>
    </div>
  );
}

export default App;