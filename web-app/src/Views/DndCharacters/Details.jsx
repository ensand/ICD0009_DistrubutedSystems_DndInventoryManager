import React from 'react';

import {useStoreState} from 'easy-peasy';
import {useParams} from 'react-router-dom';

import {ApiGet, ApiPut, ApiPost, ApiDelete} from '../../Utils/AccountActions';

import {Button, Grid, Paper, TextField, Typography} from '@material-ui/core';


export default function Details(props) {
    const {id} = useParams();

    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const token = useStoreState(state => state.appState.token);

    const [item, setItem] = React.useState();


    const fetchItem = async () => {
        const apiCall = await ApiGet(token, "DndCharacters", id);

        let data;
        try {
            data = await apiCall.json();
        } catch (e) {
            console.log("Error: ", e);
        }
        if (data !== undefined) {
            setItem(data);
        }
    }

    React.useEffect(() => {
        fetchItem();
    }, [userIsLoggedIn]);

    return (
        <div>
            <h1>{item ? item.name : "[charcter name]"}</h1>
            <p><Button variant="contained" color="primary" onClick={() => {}}>Delete</Button></p>
            {item && <div>
                {item.comment && <Typography variant="h5">{item.comment}</Typography>}
                <hr/>
                Treasure:

                Weapons:

                Armor:

                Magical items:

                Other stuff: 
            </div>}
        </div>
    );
}