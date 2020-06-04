import React from 'react';

import {useStoreState} from 'easy-peasy';

import {Button, Grid, Link, Paper, TextField, Typography} from '@material-ui/core';
import Modal from '../../Components/Modal/Modal.jsx';

import {ApiGet, ApiPost, ApiDelete} from '../../Utils/AccountActions';

function View() {
    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const token = useStoreState(state => state.appState.token);

    const [items, setItems] = React.useState([]);
    const [modalOpen, toggleModal] = React.useState(false);

    const [name, setName] = React.useState("");
    const [comment, setComment] = React.useState("");
    const [platinumPieces, setPlatinumPieces] = React.useState("");
    const [goldPieces, setGoldPieces] = React.useState("");
    const [electrumPieces, setElectrumPieces] = React.useState("");
    const [silverPieces, setSilverPieces] = React.useState("");
    const [copperPieces, setCoppperPieces] = React.useState("");

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

    }

    const redirectToEdit = (id) => {
        
    }

    const handleModalClose = () => {
        setName("");
        setComment("");
        setPlatinumPieces("");
        setGoldPieces("");
        setElectrumPieces("");
        setSilverPieces("");
        setCoppperPieces("");

        toggleModal(false);
    }

    const createChar = async () => {
        let body = {
            name,
            comment: comment === "" ? null : comment,
            platinumPieces: platinumPieces === "" ? 0 : parseInt(platinumPieces, 10), 
            goldPieces: goldPieces === "" ? 0 : parseInt(goldPieces, 10), 
            electrumPieces: electrumPieces === "" ? 0 : parseInt(electrumPieces, 10), 
            silverPieces: silverPieces === "" ? 0 : parseInt(silverPieces, 10),
            copperPieces: copperPieces === "" ? 0 : parseInt(copperPieces, 10)
        };


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
            <h1>Your slaves</h1>
            <p><Button variant="contained" color="primary" onClick={() => toggleModal(true)}>Create new</Button></p>
            <Grid container spacing={3}>
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
                            <hr/>
                            <div style={{display: "flex", justifyContent: "space-between"}}>
                                <Button variant="outlined" color="primary" size="small" title="View and change all details" onClick={() => redirectToDetails(item.id)}>Details</Button>
                                <Button variant="outlined" color="primary" size="small" title="Edit name, comment, and treasure">Edit</Button>
                                <Button variant="outlined" color="secondary" size="small" title="There is no return from here" onClick={() => deleteItem(item.id)}>Delete</Button>
                            </div>
                        </Paper>
                    </Grid>
                })}
            </Grid>

            {modalOpen && 
                <Modal onClose={() => handleModalClose()} onSave={(e) => {handleModalClose(); createChar();}} title="Create new character">
                    <div style={{display: "flex", flexDirection: "column"}}>
                        <TextField name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                        <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>

                        <TextField type="number" name="platinumPieces" label="Platinum pieces" value={platinumPieces} onChange={(e) => setPlatinumPieces(e.target.value)}/>
                        <TextField type="number" name="goldPieces" label="Gold pieces" value={goldPieces} onChange={(e) => setGoldPieces(e.target.value)}/>
                        <TextField type="number" name="electrumPieces" label="Electrum pieces" value={electrumPieces} onChange={(e) => setElectrumPieces(e.target.value)}/>
                        <TextField type="number" name="silverPieces" label="Silver pieces" value={silverPieces} onChange={(e) => setSilverPieces(e.target.value)}/>
                        <TextField type="number" name="copperPieces" label="Coppper pieces" value={copperPieces} onChange={(e) => setCoppperPieces(e.target.value)}/>
                    </div>
                </Modal>}

        </div>
    );
}

export default View;