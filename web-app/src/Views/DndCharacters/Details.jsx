import React from 'react';

import {useStoreState} from 'easy-peasy';
import {useHistory, useParams} from 'react-router-dom';

import {ApiGet, ApiPut, ApiPost, ApiDelete} from '../../Utils/AccountActions';

import DisplayList from '../../Components/DisplayList/DisplayList.jsx';
import CharacterModal from '../../Components/CharacterModal/CharacterModal.jsx';
import ArmorModal from '../../Components/ArmorModal/ArmorModal.jsx';
import WeaponModal from '../../Components/WeaponModal/WeaponModal.jsx';
import MagicalItemModal from '../../Components/MagicalItemModal/MagicalItemModal.jsx';
import OtherEquipmentModal from '../../Components/OtherEquipmentModal/OtherEquipmentModal.jsx';

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
    const [modalOpen, toggleModal] = React.useState(false);


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

    const handleModalClose = () => {
        toggleModal(false);
    }

    const deleteItem = async (dbObj, id) => {
        const apiCall = await ApiDelete(token, dbObj, id);

        if (apiCall.status === "200" || apiCall.status === 200) {
            if (dbObj === "DndCharacters") {
                history.push("/Characters");
            } else {
                fetchItem();
            }
        }
    }
    
    const edit = async (dbObj, body, id) => {
        if (dbObj === "DndCharacters") {
            body.id = item.id;
        } else {
            body.dndCharacterId = item.id;
        }
        const apiCall = await ApiPut(token, dbObj, id, body);
        
        if (apiCall.status === "200" || apiCall.status === 200) {
            await fetchItem();
        }
    }

    const create = async (dbObj, body) => {
        body.dndCharacterId = item.id;
        const apiCall = await ApiPost(token, dbObj, body);
        
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
                    <IconButton color="primary" title="Edit the name, comment or treasure" onClick={() => toggleModal("character")}><EditIcon/></IconButton>
                </div>
                <div style={{display: "flex", flexDirection: "column"}}>
                    <Button variant="contained" color="secondary" onClick={() => deleteItem("DndCharacters", item.id)}>Delete</Button>
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
                        <IconButton color="primary" title="Add a new weapon" onClick={() => toggleModal("newWeapon")}><AddIcon/></IconButton>
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
                                <ExpansionPanelDetails style={{display: "flex", flexDirection: "column"}}>
                                    <DisplayList 
                                        itemId={`weapons_${weapon.id}`}
                                        displayItems={[weapon.comment, weapon.weaponType, weapon.weaponRange, weapon.properties, weapon.weight, weapon.valueInGp, weapon.quantity]} 
                                        displayHeadings={["Comment", "Type", "Range", "Properties", "Weight", "Value (GP)", "Quantity"]}/>

                                    <hr/>
                                    <div style={{display: "flex", justifyContent: "space-between"}}>
                                        <Button variant="outlined" color="primary" size="small" title="Edit this thing" onClick={() => toggleModal({dbObj: "weapons", id: weapon.id})}>Edit</Button>
                                        <Button variant="outlined" color="secondary" size="small" title="Delete this thing" onClick={() => deleteItem("weapons", weapon.id)}>Delete</Button>
                                    </div>
                                </ExpansionPanelDetails>
                            </ExpansionPanel>
                        );
                    })}
                </div>

                <div>
                    <div style={{display: "flex", alignItems: "center"}}>
                        <Typography variant="h6">Armor</Typography>
                        <IconButton color="primary" title="Add a new armor" onClick={() => toggleModal("newArmor")}><AddIcon/></IconButton>
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
                                <ExpansionPanelDetails style={{display: "flex", flexDirection: "column"}}>
                                    <DisplayList 
                                        itemId={`armor_${armor.id}`}
                                        displayItems={[armor.comment, armor.armorType, armor.strengthRequirement, armor.stealthDisadvantage, armor.weight, armor.valueInGp, armor.quantity]} 
                                        displayHeadings={["Comment", "Type", "Strength requirement", "Stealth disadvantage", "Weight", "Value (GP)", "Quantity"]}/>

                                    <hr/>
                                    <div style={{display: "flex", justifyContent: "space-between"}}>
                                        <Button variant="outlined" color="primary" size="small" title="Edit this thing" onClick={() => toggleModal({dbObj: "armor", id: armor.id})}>Edit</Button>
                                        <Button variant="outlined" color="secondary" size="small" title="Delete this thing" onClick={() => deleteItem("armor", armor.id)}>Delete</Button>
                                    </div>
                                </ExpansionPanelDetails>
                            </ExpansionPanel>
                        );
                    })}
                </div>

                <div>
                    <div style={{display: "flex", alignItems: "center"}}>
                        <Typography variant="h6">Magical items</Typography>
                        <IconButton color="primary" title="Add a new magical item" onClick={() => toggleModal("newMi")}><AddIcon/></IconButton>
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
                                <ExpansionPanelDetails style={{display: "flex", flexDirection: "column"}}>
                                    <DisplayList 
                                        itemId={`magicalItems_${magicalItem.id}`}
                                        displayItems={[magicalItem.comment, magicalItem.maxCharges, magicalItem.currentCharges, magicalItem.weight, magicalItem.valueInGp, magicalItem.quantity]} 
                                        displayHeadings={["Comment", "Maximum charges", "Current charges", "Weight", "Value (GP)", "Quantity"]}/>

                                    <hr/>
                                    <div style={{display: "flex", justifyContent: "space-between"}}>
                                        <Button variant="outlined" color="primary" size="small" title="Edit this thing" onClick={() => toggleModal({dbObj: "mi", id: magicalItem.id})}>Edit</Button>
                                        <Button variant="outlined" color="secondary" size="small" title="Delete this thing" onClick={() => deleteItem("magicalItems", magicalItem.id)}>Delete</Button>
                                    </div>
                                </ExpansionPanelDetails>
                            </ExpansionPanel>
                        );
                    })}
                </div>

                <div>
                    <div style={{display: "flex", alignItems: "center"}}>
                        <Typography variant="h6">Other equipment</Typography>
                        <IconButton color="primary" title="Add a new equipment" onClick={() => toggleModal("newEq")}><AddIcon/></IconButton>
                    </div>
                    {item.otherEquipment.map((eq) => {
                        return (
                            <ExpansionPanel key={eq.id}>
                                <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />} id={eq.id}>
                                    <div style={{display: "flex", flexDirection: "column"}}>
                                        <Typography variant="body1"><b>{eq.name}</b></Typography>
                                    </div>
                                </ExpansionPanelSummary>
                                <ExpansionPanelDetails style={{display: "flex", flexDirection: "column"}}>
                                    <DisplayList 
                                        itemId={`eq_${eq.id}`}
                                        displayItems={[eq.comment, eq.weight, eq.valueInGp, eq.quantity]} 
                                        displayHeadings={["Comment", "Weight", "Value (GP)", "Quantity"]}/>

                                    <hr/>
                                    <div style={{display: "flex", justifyContent: "space-between"}}>
                                        <Button variant="outlined" color="primary" size="small" title="Edit this thing" onClick={() => toggleModal({dbObj: "eq", id: eq.id})}>Edit</Button>
                                        <Button variant="outlined" color="secondary" size="small" title="Delete this thing" onClick={() => deleteItem("otherEquipments", eq.id)}>Delete</Button>
                                    </div>
                                </ExpansionPanelDetails>
                            </ExpansionPanel>
                        );
                    })}
                </div>
            </div>
        
            {modalOpen === "character" && <CharacterModal closeModal={handleModalClose} onSave={(body) => edit("DndCharacters", body, item.id)} oldBody={prepareEditBody(item, "DndCharacters")}/>}
            
            {modalOpen === "newEq" && <OtherEquipmentModal closeModal={handleModalClose} onSave={(body) => create("otherEquipments", body)}/>}
            {modalOpen.dbObj === "eq" && modalOpen.id !== undefined && <OtherEquipmentModal closeModal={handleModalClose} onSave={(body) => edit("otherEquipments", body, modalOpen.id)} oldBody={prepareEditBody(item, "otherEquipment", modalOpen.id)}/>}
        
            {modalOpen === "newMi" && <MagicalItemModal closeModal={handleModalClose} onSave={(body) => create("magicalItems", body)}/>}
            {modalOpen.dbObj === "mi" && modalOpen.id !== undefined && <MagicalItemModal closeModal={handleModalClose} onSave={(body) => edit("magicalItems", body, modalOpen.id)} oldBody={prepareEditBody(item, "magicalItems", modalOpen.id)}/>}

            {modalOpen === "newArmor" && <ArmorModal closeModal={handleModalClose} onSave={(body) => create("armor", body)}/>}
            {modalOpen.dbObj === "armor" && modalOpen.id !== undefined && <ArmorModal closeModal={handleModalClose} onSave={(body) => edit("armor", body, modalOpen.id)} oldBody={prepareEditBody(item, "armor", modalOpen.id)}/>}
            
        </div>
    );
}

function prepareEditBody(characterDetails, dbObj, id) {
    let item;
    if (id) {
        item = characterDetails[dbObj].find(x => x.id === id);
    }

    switch(dbObj){
        case "DndCharacters":
            return {
                name: characterDetails.name, 
                comment: characterDetails.comment, 
                platinumPieces: characterDetails.platinumPieces, 
                goldPieces: characterDetails.goldPieces, 
                electrumPieces: characterDetails.electrumPieces, 
                silverPieces: characterDetails.silverPieces, 
                copperPieces: characterDetails.copperPieces
            };

        case "otherEquipment":
            return {
                id,
                name: item.name, 
                comment: item.comment,
                weight: item.weight,
                valueInGp: item.valueInGp,
                quantity: item.quantity
            };

        case "magicalItems": 
            return {
                id,
                name: item.name, 
                comment: item.comment,
                weight: item.weight,
                valueInGp: item.valueInGp,
                quantity: item.quantity,
                spell: item.spell,
                maxCharges: item.maxCharges,
                currentCharges: item.currentCharges
            };

        case "armor":
            return {
                id,
                name: item.name, 
                comment: item.comment,
                weight: item.weight,
                valueInGp: item.valueInGp,
                quantity: item.quantity,
                armorType: item.armorType,
                ac: item.ac,
                stealthDisadvantage: item.stealthDisadvantage,
                strengthRequirement: item.strengthRequirement
            };
    }
}
