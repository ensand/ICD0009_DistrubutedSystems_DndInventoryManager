import React from 'react';

import {useStoreState} from 'easy-peasy';
import {useHistory} from 'react-router-dom';

import {Typography} from '@material-ui/core';


function Main(props) {
    const history = useHistory();
    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);

    return (
        <div className="text-center">
            <h1 className="display-4">Dungeons and Dragons Inventory Manager</h1>

            <div style={{height: "30vh"}}>
                <img style={{height: "inherit"}} alt="dndLogo" src={require("../../Images/dndLogo.jpg")}/>
            </div>

            {userIsLoggedIn && <Typography variant="h5" onClick={() => history.push("/Characters")} style={{cursor: "pointer"}}>View or create your characters here</Typography>}
            <br/>
        </div>
    );
}

export default Main;