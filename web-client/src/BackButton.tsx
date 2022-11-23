import { useNavigate } from "react-router-dom";
import { useLocation } from 'react-router-dom';


interface CreateFileProps {
    
    handleNavigation(path:string): void;
}

const BackButton = (props: CreateFileProps) => {
    let navigate = useNavigate();
    const location = useLocation();
    
    const goBack = () => {
        const path = location.pathname.substring(0, location.pathname.lastIndexOf('/')) || "/";
        navigate(path);
        props.handleNavigation(path); 
    }

    return (
       <button style={{width:"100px", marginBottom:"20px"}} onClick={goBack}>Back</button>
    );
}

export default BackButton;