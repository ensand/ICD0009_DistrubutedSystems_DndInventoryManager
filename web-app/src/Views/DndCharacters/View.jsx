import React from 'react';

import {useStoreState} from 'easy-peasy';
import {useHistory} from 'react-router-dom';

import {ApiGet, ApiPost, ApiDelete} from '../../Utils/AccountActions';

import {Button, Grid, Paper, Typography} from '@material-ui/core';
import CharacterModal from '../../Components/CharacterModal/CharacterModal.jsx';

function View() {
    const history = useHistory();

    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const token = useStoreState(state => state.appState.token);

    const [items, setItems] = React.useState([]);
    const [modalOpen, toggleModal] = React.useState(false);

    const fetchItems = async () => {
        const apiCall = await ApiGet(token, "DndCharacters");

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
        const apiCall = await ApiDelete(token, "DndCharacters", id);

        if (apiCall.status === "200" || apiCall.status === 200) {
            await fetchItems();
        }
    }

    const redirectToDetails = (id) => {
        history.push(`/Characters/${id}`);
    }

    const handleModalClose = () => {
        toggleModal(false);
    }

    const createChar = async (body) => {
        const apiCall = await ApiPost(token, "DndCharacters", body);
        
        if (apiCall.status === "200" || apiCall.status === 200) {
            await fetchItems();
        }
    }

    React.useEffect(() => {
        fetchItems();
    }, [userIsLoggedIn]);

    return (
        <div>
            <h1>Your sacrifices to the DM</h1>
            <p><Button variant="contained" color="primary" onClick={() => toggleModal(true)}>Create new</Button></p>
            <Grid container spacing={3}>
                {items.map((item) => {
                    return <Grid item key={item.id} style={{minHeight: "10vh", width: "20rem", height: "fit-content"}}>
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
                            <hr/>
                            <div style={{display: "flex", justifyContent: "space-between"}}>
                                <Button variant="outlined" color="primary" size="small" title="View and change all details" onClick={() => redirectToDetails(item.id)}>Details</Button>
                                <Button variant="outlined" color="secondary" size="small" title="There is no return from here" onClick={() => deleteItem(item.id)}>Delete</Button>
                            </div>
                        </Paper>
                    </Grid>
                })}
            </Grid>

            {modalOpen && <CharacterModal closeModal={handleModalClose} onSave={(body) => createChar(body)}/>}

        </div>
    );
}

export default View;