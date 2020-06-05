import React from 'react';

import {useStoreState} from 'easy-peasy';
import {useParams} from 'react-router-dom';

import {ApiGet, ApiPut, ApiPost, ApiDelete} from '../../Utils/AccountActions';

import {Button, ExpansionPanel, ExpansionPanelDetails, ExpansionPanelSummary, Grid, IconButton, Paper, TextField, Typography} from '@material-ui/core';
import EditIcon from '@material-ui/icons/Edit';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';


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
            {/* <p><Button variant="contained" color="primary" onClick={() => {}}>Delete</Button></p> */}
            {item && <div>
                {item.comment && <Typography variant="h5">{item.comment}</Typography>}
                <hr/>
                    <div style={{display: "grid", gridTemplateColumns: "1fr 1fr 3fr"}}>
                        <Paper style={{backgroundColor: "#ececec", padding: "1rem", width: "13rem"}}>
                            <Typography variant="h6">Overview:</Typography>
                            <DisplayList 
                                displayItems={[item.treasureInGp, item.allItemsValueInGp, item.allItemsWeight]} 
                                displayHeadings={["Total treasure (GP)", "All items value (GP)", "All items weight"]}/>
                        </Paper>

                        <Paper style={{backgroundColor: "#ececec", padding: "1rem", width: "12rem"}}>
                            <div style={{display: "flex", justifyContent: "space-between", alignItems: "center"}}>
                                <Typography variant="h6">Treasure:</Typography>
                                <IconButton variant="outlined" color="primary" onClick={() => {}}><EditIcon/></IconButton>
                            </div>
                            <DisplayList 
                                displayItems={[item.platinumPieces, item.goldPieces, item.electrumPieces, item.silverPieces, item.copperPieces]} 
                                displayHeadings={["Platinum pieces", "Gold pieces", "Electrum pieces", "Silver pieces", "Copper pieces"]}/>
                        </Paper>
                    </div>

                    <div style={{display: "grid", gridTemplateColumns: "1fr 1fr", gridTemplateRows: "auto", columnGap: "1rem", rowGap: "1rem"}}>
                        <div>
                            <Typography variant="h6">Weapons</Typography>                        
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
                                                displayItems={[weapon.comment, weapon.weaponType, weapon.weaponRange, weapon.properties, weapon.weight, weapon.valueInGp, weapon.quantity]} 
                                                displayHeadings={["Comment", "Type", "Range", "Properties", "Weight", "Value (GP)", "Quantity"]}/>
                                        </ExpansionPanelDetails>
                                    </ExpansionPanel>
                                );
                            })}
                        </div>

                        <div>
                            <Typography variant="h6">Armor</Typography>                        
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
                                                displayItems={[armor.comment, armor.armorType, armor.strengthRequirement, armor.stealthDisdvantage, armor.weight, armor.valueInGp, armor.quantity]} 
                                                displayHeadings={["Comment", "Type", "Strength requirement", "Stealth disadvantage", "Weight", "Value (GP)", "Quantity"]}/>
                                        </ExpansionPanelDetails>
                                    </ExpansionPanel>
                                );
                            })}
                        </div>

                        <div>
                            <Typography variant="h6">Magical items</Typography>                        
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
                                                displayItems={[magicalItem.comment, magicalItem.maxCharges, magicalItem.currentCharges, magicalItem.weight, magicalItem.valueInGp, magicalItem.quantity]} 
                                                displayHeadings={["Comment", "Maximum charges", "Current charges", "Weight", "Value (GP)", "Quantity"]}/>
                                        </ExpansionPanelDetails>
                                    </ExpansionPanel>
                                );
                            })}
                        </div>

                        <div>
                            <Typography variant="h6">Other thingys</Typography>                        
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
                                                displayItems={[eq.comment, eq.weight, eq.valueInGp, eq.quantity]} 
                                                displayHeadings={["Comment", "Weight", "Value (GP)", "Quantity"]}/>
                                        </ExpansionPanelDetails>
                                    </ExpansionPanel>
                                );
                            })}
                        </div>
                    </div>
            </div>}
        </div>
    );
}

function DisplayList(props) {
    const {displayItems, displayHeadings} = props;

    return (
        <div style={{display: "grid", gridTemplateColumns: "1fr", gridColumnGap: "1rem", justifyItems: "start", width: "100%"}}>
            {displayItems.map((displayItem, index) => {
                if (displayItem === undefined)
                    return <></>;

                return <div key={index} style={{display: "flex", justifyContent: "space-between", width: "inherit", borderBottom: "1px solid lightgray", padding: "5px 0 5px 0"}}>
                    <Typography variant="body1">{displayHeadings[index]}:</Typography>
                    <Typography variant="body1">{displayItem}</Typography>
                </div>
            })}
        </div>
    );

}