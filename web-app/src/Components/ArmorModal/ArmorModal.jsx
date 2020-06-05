import React from 'react';

import TabPanel from '../MaterialTabPanel/MaterialTabPanel.jsx';

import {Checkbox, FormControlLabel, Tabs, Tab, TextField} from '@material-ui/core';
import {Autocomplete} from '@material-ui/lab';
import Modal from '../Modal/Modal.jsx';


export default function ArmorModal(props) {
    const {closeModal, onSave, oldBody} = props;

    const [activeTab, setActiveTab] = React.useState(0);

    const [apiArmors, setApiArmors] = React.useState([]);
    
    const [armor, setArmor] = React.useState("");

    const [name, setName] = React.useState("");
    const [comment, setComment] = React.useState("");
    const [weight, setWeight] = React.useState("");
    const [valueInGp, setValueInGp] = React.useState("");
    const [quantity, setQuantity] = React.useState("");
    const [armorType, setArmorType] = React.useState("");
    const [ac, setAc] = React.useState("");
    const [strengthRequirement, setStrengthRequirement] = React.useState("");
    const [stealthDisadvantage, setStealthDisadvantage] = React.useState(false);

    const fetchApiArmors = async () => {
        const apiCall = await fetch("https://www.dnd5eapi.co/api/equipment-categories/armor");
        const res = await apiCall.json();

        if (res.equipment) {
            let justNames = res.equipment.map(x => x.name);
            setApiArmors(justNames);
        }
    }

    const handleModalClose = () => {
        setName("");
        setComment("");
        setWeight("");
        setValueInGp("");
        setQuantity("");
        setAc("");
        setArmorType("");
        setStrengthRequirement("");
        setStealthDisadvantage(false);

        closeModal();
    }

    const save = async () => {
        if (oldBody === undefined && activeTab === 0) {
            let apiFriendly = armor.toLowerCase().split(" ").join("-");
            let url = `https://www.dnd5eapi.co/api/equipment/${apiFriendly}`;
    
            const apiCall = await fetch(url);
            const res = await apiCall.json();
    
            let body = {
                Name: armor,
                Weight: res.weight,
                ValueInGp: calculateValueInGp(res.cost),
                Quantity: 1,
                ArmorType: res.armor_category,
                Ac: `${res.armor_class.base}${res.armor_class.dex_bonus !== false ? " + dex" : ""}${res.armor_class.max_bonus !== null ? ` (max bonus ${res.armor_class.max_bonus})` : ""}`,
                StealthDisadvantage: res.stealth_disadvantage,
                StrengthRequirement: res.str_minimum === 0 ? null : res.str_minimum,
            };
    
            handleModalClose(); 
            closeModal();
    
            onSave(body);

            return;
        }

        let body = {
            name,
            comment: comment === "" ? null : comment,
            weight: weight === "" ? 0 : parseFloat(weight), 
            valueInGp: valueInGp === "" ? 0 : parseFloat(valueInGp), 
            quantity: quantity === "" ? 0 : parseInt(quantity, 10),
            ac,
            armorType: armorType === "" ? null : armorType,
            strengthRequirement: strengthRequirement === "" ? 0 : parseInt(strengthRequirement, 10),
            stealthDisadvantage: stealthDisadvantage,
        };

        if (oldBody) {
            body.id = oldBody.id;
        }

        handleModalClose(); 
        closeModal();

        onSave(body);
    }

    React.useEffect(() => {
        if (oldBody) {
            setName(oldBody.name);
            setComment(oldBody.comment === null ? "" : oldBody.comment);
            setWeight(oldBody.weight);
            setValueInGp(oldBody.valueInGp);
            setQuantity(oldBody.quantity);
            setAc(oldBody.ac);
            setArmorType(oldBody.armorType);
            setStrengthRequirement(oldBody.strengthRequirement);
            setStealthDisadvantage(oldBody.stealthDisadvantage);
        }
        fetchApiArmors();
    }, []);

    if (oldBody === undefined) {
        return (
            <Modal onClose={() => handleModalClose()} onSave={(e) => save()} title={oldBody ? "Edit armor" : "Create new armor"}>
    
                <Tabs value={activeTab} onChange={(e, newValue) => setActiveTab(newValue)}>
                    <Tab label="Predefined"/>
                    <Tab label="Custom"/>
                </Tabs>
    
                <TabPanel value={activeTab} index={0}>
                    <Autocomplete
                        value={armor}
                        onChange={(e, newValue) => setArmor(newValue === null ? "" : newValue)}
                        freeSolo
                        disableClearable
                        options={apiArmors}
                        renderInput={(params) => (
                            <TextField
                                {...params}
                                onKeyDown={(e) => setArmor(e.target.value)}
                                label="Armor"
                                InputProps={{...params.InputProps, type: 'search'}}
                                />
                        )}
                        />
                </TabPanel>
                
                <TabPanel value={activeTab} index={1}>
                    <div style={{display: "flex", flexDirection: "column"}}>
                        <TextField name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                        <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>
    
                        <TextField type="number" step="0.01" name="weight" label="Weight" value={weight} onChange={(e) => setWeight(e.target.value)}/>
                        <TextField type="number" step="0.01" name="valueInGp" label="Value in gold pieces" value={valueInGp} onChange={(e) => setValueInGp(e.target.value)}/>
                        <TextField type="number" name="quantity" label="Quantity" value={quantity} onChange={(e) => setQuantity(e.target.value)}/>
    
                        <TextField name="armorType" label="Armor type" value={armorType} onChange={(e) => setArmorType(e.target.value)}/>
                        <TextField name="ac" label="AC" value={ac} onChange={(e) => setAc(e.target.value)}/>
                        <TextField type="number" name="strengthRequirement" label="Strength requirement" value={strengthRequirement} onChange={(e) => setStrengthRequirement(e.target.value)}/>
                        <FormControlLabel
                                control={<Checkbox checked={stealthDisadvantage} onChange={(e) => setStealthDisadvantage(e.target.checked)} color="primary"/>}
                                label="Stealth disadvantage"/>
                    </div>
                </TabPanel>
            </Modal>
        );
    }

    return (
        <Modal onClose={() => handleModalClose()} onSave={(e) => save()} title={oldBody ? "Edit armor" : "Create new armor"}>
            <div style={{display: "flex", flexDirection: "column"}}>
                <TextField name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>

                <TextField type="number" step="0.01" name="weight" label="Weight" value={weight} onChange={(e) => setWeight(e.target.value)}/>
                <TextField type="number" step="0.01" name="valueInGp" label="Value in gold pieces" value={valueInGp} onChange={(e) => setValueInGp(e.target.value)}/>
                <TextField type="number" name="quantity" label="Quantity" value={quantity} onChange={(e) => setQuantity(e.target.value)}/>

                <TextField name="armorType" label="Armor type" value={armorType} onChange={(e) => setArmorType(e.target.value)}/>
                <TextField name="ac" label="AC" value={ac} onChange={(e) => setAc(e.target.value)}/>
                <TextField type="number" name="strengthRequirement" label="Strength requirement" value={strengthRequirement} onChange={(e) => setStrengthRequirement(e.target.value)}/>
                <FormControlLabel
                        control={<Checkbox checked={stealthDisadvantage} onChange={(e) => setStealthDisadvantage(e.target.checked)} color="primary"/>}
                        label="Stealth disadvantage"/>
            </div>
        </Modal>
    );


    
}


function calculateValueInGp(costObj) {
    switch(costObj.unit) {
        case "pp":
            return costObj.quantity * 10;
        
        case "gp":
            return costObj.quantity;

        case "ep":
            return costObj.quantity / 2;

        case "sp":
            return costObj.quantity / 10;

        case "cp":
            return costObj.quantity / 100;
    }
}