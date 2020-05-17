import React from 'react';

import {useStoreState} from 'easy-peasy';

import {Grid, Paper, Typography} from '@material-ui/core';

function View() {
    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const token = useStoreState(state => state.appState.token);

    const [items, setItems] = React.useState([]);

    const fetchItems = async () => {
        const apiCall = await fetch("https://localhost:5001/api/v1.0/DndCharacters", {
                method: 'GET', 
                headers: {
                    "Content-Type": "application/json",
                    "Authorization":  `bearer ${token}`
                }
            });
        let data;
        try {
            data = await apiCall.json();
        } catch (e) {
            console.log("Error: ", e);
        }
        if (data !== undefined) {
            setItems(data);
        }
    }

    const deleteItem = async (id) => {
        await fetch(`https://localhost:5001/api/DndCharacters/${id}`, {method: 'DELETE'});
        fetchItems();
    }

    React.useEffect(() => {
        fetchItems();
    }, [userIsLoggedIn]);

    return (
        <div>
            <h1>Index</h1>
            <p><button>Create New</button></p>
            <Grid container>
                {items.map((item) => {
                    return <Grid item key={item.id} style={{minHeight: "10vh", minWidth: "15vw", width: "fit-content", height: "fit-content"}}>
                        <Paper style={{backgroundColor: "#ececec", padding: "1rem"}}>
                            <Typography variant="h5">{item.name}</Typography>
                            {item.comment && <Typography variant="subtitle2"><i>{item.comment}</i></Typography>}
                            <hr/>
                            <div style={{display: "grid", gridTemplateColumns: "1fr 1fr", gridTemplateRows: "1fr 1fr 1fr", gridColumnGap: "1rem", justifyItems: "start"}}>
                                <Typography variant="body2">Weapons</Typography>
                                <Typography variant="body1" style={{justifySelf: "center"}}>{item.weaponCount}</Typography>
                                <Typography variant="body2">Armor</Typography>
                                <Typography variant="body1" style={{justifySelf: "center"}}>{item.armorCount}</Typography>
                                <Typography variant="body2">Other equipment</Typography>
                                <Typography variant="body1" style={{justifySelf: "center"}}>{item.otherEquipmentCount + item.magicalItemCount}</Typography>
                                <Typography variant="body2">Treasure in GP</Typography>
                                <Typography variant="body1" style={{justifySelf: "center"}}>{item.treasureInGp}</Typography>
                            </div>
                        </Paper>
                    </Grid>
                })}
            </Grid>
        </div>
    );
}

export default View;