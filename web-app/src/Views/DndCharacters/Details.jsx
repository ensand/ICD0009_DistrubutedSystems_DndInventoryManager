import React from 'react';

import {useStoreState} from 'easy-peasy';
import {useHistory, useParams} from 'react-router-dom';

import {ApiGet, ApiPut, ApiPost, ApiDelete} from '../../Utils/AccountActions';

import DisplayList from '../../Components/DisplayList/DisplayList.jsx';
import CharacterModal from '../../Components/CharacterModal/CharacterModal.jsx';

import {Button, ExpansionPanel, ExpansionPanelDetails, ExpansionPanelSummary, Grid, IconButton, Paper, TextField, Typography} from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import EditIcon from '@material-ui/icons/Edit';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';


export default function Details(props) {
    const history = useHistory();
    const {id} = useParams();

    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const token = useStoreState(state => state.appState.token);

    const [item, setItem] = React.useState();
    const [modalOpen, toggleModal] = React.useState();


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

    const deleteItem = async () => {
        const apiCall = await ApiDelete(token, "DndCharacters", item.id);

        if (apiCall.status === "200" || apiCall.status === 200) {
            history.push("/Characters");
        }
    }

    const handleModalClose = () => {
        toggleModal(false);
    }

    
    const editChar = async (body) => {
        body.id = item.id;
        const apiCall = await ApiPut(token, "DndCharacters", item.id, body);
        
        if (apiCall.status === "200" || apiCall.status === 200) {
            await fetchItem();
        }
    }

    React.useEffect(() => {
        fetchItem();
    }, [userIsLoggedIn]);


    if (!userIsLoggedIn || item === undefined || item.name === undefined)
        return <>Not found</>;

    return (
        <div>
            <div style={{display: "flex", justifyContent: "space-between"}}>
                <div style={{display: "flex", justifyContent: "space-between"}}>
                    <h1>{item.name}</h1>
                    <IconButton color="primary" title="Edit the name, comment or treasure" onClick={() => toggleModal(true)}><EditIcon/></IconButton>
                </div>
                <div style={{display: "flex", flexDirection: "column"}}>
                    <Button variant="contained" color="secondary" onClick={() => deleteItem()}>Delete</Button>
                </div>
            </div>
            {item.comment && <Typography variant="h5">{item.comment}</Typography>}
            <hr/>
            <div style={{display: "grid", gridTemplateColumns: "1fr 1fr 3fr", marginBottom: "1rem"}}>
                <Paper style={{backgroundColor: "#ececec", padding: "1rem", width: "13rem"}}>
                    <Typography variant="h6">Overview:</Typography>
                    <DisplayList
                        itemId={"overview"}
                        displayItems={[item.treasureInGp, item.allItemsValueInGp, item.allItemsWeight]} 
                        displayHeadings={["Total treasure (GP)", "All items value (GP)", "All items weight"]}/>
                </Paper>

                <Paper style={{backgroundColor: "#ececec", padding: "1rem", width: "12rem"}}>
                    <Typography variant="h6">Treasure:</Typography>
                    <DisplayList
                        itemId={"treasure"}
                        displayItems={[item.platinumPieces, item.goldPieces, item.electrumPieces, item.silverPieces, item.copperPieces]} 
                        displayHeadings={["Platinum pieces", "Gold pieces", "Electrum pieces", "Silver pieces", "Copper pieces"]}/>
                </Paper>
            </div>

            <div style={{display: "grid", gridTemplateColumns: "1fr 1fr", gridTemplateRows: "auto", columnGap: "1rem", rowGap: "1rem"}}>
                <div>
                    <div style={{display: "flex", alignItems: "center"}}>
                        <Typography variant="h6">Weapons</Typography>
                        <IconButton color="primary" title="Add a new weapon"><AddIcon/></IconButton>
                    </div>
                    {item.weapons.map((weapon) => {
                        return (
                            <ExpansionPanel key={weapon.id}>
                                <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />} id={weapon.id}>
                                    <div style={{display: "flex", flexDirection: "column"}}>
                                        <Typography variant="body1"><b>{weapon.name}</b></Typography>
                                        <Typography variant="subtitle1">Damage: {weapon.damageDice} {weapon.damageType}</Typography>
                                    </div>
                                </ExpansionPanelSummary>
                                <ExpansionPanelDetails>
                                    <DisplayList 
                                        itemId={`weapons_${weapon.id}`}
                                        displayItems={[weapon.comment, weapon.weaponType, weapon.weaponRange, weapon.properties, weapon.weight, weapon.valueInGp, weapon.quantity]} 
                                        displayHeadings={["Comment", "Type", "Range", "Properties", "Weight", "Value (GP)", "Quantity"]}/>
                                </ExpansionPanelDetails>
                            </ExpansionPanel>
                        );
                    })}
                </div>

                <div>
                    <div style={{display: "flex", alignItems: "center"}}>
                        <Typography variant="h6">Armor</Typography>
                        <IconButton color="primary" title="Add a new armor"><AddIcon/></IconButton>
                    </div>                       
                    {item.armor.map((armor) => {
                        return (
                            <ExpansionPanel key={armor.id}>
                                <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />} id={armor.id}>
                                    <div style={{display: "flex", flexDirection: "column"}}>
                                        <Typography variant="body1"><b>{armor.name}</b></Typography>
                                        <Typography variant="subtitle1">AC: {armor.ac}</Typography>
                                    </div>
                                </ExpansionPanelSummary>
                                <ExpansionPanelDetails>
                                    <DisplayList 
                                        itemId={`armor_${armor.id}`}
                                        displayItems={[armor.comment, armor.armorType, armor.strengthRequirement, armor.stealthDisdvantage, armor.weight, armor.valueInGp, armor.quantity]} 
                                        displayHeadings={["Comment", "Type", "Strength requirement", "Stealth disadvantage", "Weight", "Value (GP)", "Quantity"]}/>
                                </ExpansionPanelDetails>
                            </ExpansionPanel>
                        );
                    })}
                </div>

                <div>
                    <div style={{display: "flex", alignItems: "center"}}>
                        <Typography variant="h6">Magical items</Typography>
                        <IconButton color="primary" title="Add a new magical item"><AddIcon/></IconButton>
                    </div>                 
                    {item.magicalItems.map((magicalItem) => {
                        return (
                            <ExpansionPanel key={magicalItem.id}>
                                <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />} id={magicalItem.id}>
                                    <div style={{display: "flex", flexDirection: "column"}}>
                                        <Typography variant="body1"><b>{magicalItem.name}</b></Typography>
                                        <Typography variant="subtitle1">{magicalItem.spell}</Typography>
                                    </div>
                                </ExpansionPanelSummary>
                                <ExpansionPanelDetails>
                                    <DisplayList 
                                        itemId={`magicalItems_${magicalItem.id}`}
                                        displayItems={[magicalItem.comment, magicalItem.maxCharges, magicalItem.currentCharges, magicalItem.weight, magicalItem.valueInGp, magicalItem.quantity]} 
                                        displayHeadings={["Comment", "Maximum charges", "Current charges", "Weight", "Value (GP)", "Quantity"]}/>
                                </ExpansionPanelDetails>
                            </ExpansionPanel>
                        );
                    })}
                </div>

                <div>
                    <div style={{display: "flex", alignItems: "center"}}>
                        <Typography variant="h6">Other equipment</Typography>
                        <IconButton color="primary" title="Add a new equipment"><AddIcon/></IconButton>
                    </div>
                    {item.otherEquipment.map((eq) => {
                        return (
                            <ExpansionPanel key={eq.id}>
                                <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />} id={eq.id}>
                                    <div style={{display: "flex", flexDirection: "column"}}>
                                        <Typography variant="body1"><b>{eq.name}</b></Typography>
                                    </div>
                                </ExpansionPanelSummary>
                                <ExpansionPanelDetails>
                                    <DisplayList 
                                        itemId={`eq_${eq.id}`}
                                        displayItems={[eq.comment, eq.weight, eq.valueInGp, eq.quantity]} 
                                        displayHeadings={["Comment", "Weight", "Value (GP)", "Quantity"]}/>
                                </ExpansionPanelDetails>
                            </ExpansionPanel>
                        );
                    })}
                </div>
            </div>
        
            {modalOpen && <CharacterModal closeModal={handleModalClose} onSave={(body) => editChar(body)} oldBody={{name: item.name, comment: item.comment, platinumPieces: item.platinumPieces, goldPieces: item.goldPieces, electrumPieces: item.electrumPieces, silverPieces: item.silverPieces, copperPieces: item.copperPieces }}/>}
        </div>
    );
}
