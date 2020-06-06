import React from 'react';

import TabPanel from '../MaterialTabPanel/MaterialTabPanel.jsx';

import {Checkbox, FormControlLabel, Tabs, Tab, TextField} from '@material-ui/core';
import {Autocomplete} from '@material-ui/lab';
import Modal from '../Modal/Modal.jsx';


export default function WeaponModal(props) {
    const {closeModal, onSave, oldBody} = props;

    const [activeTab, setActiveTab] = React.useState(0);

    const [apiWeapons, setApiWeapons] = React.useState([]);
    
    const [weapon, setWeapon] = React.useState("");

    const [name, setName] = React.useState("");
    const [comment, setComment] = React.useState("");
    const [weight, setWeight] = React.useState("0");
    const [valueInGp, setValueInGp] = React.useState("0");
    const [quantity, setQuantity] = React.useState("1");

    const [damageDice, setDamageDice] = React.useState("");
    const [damageType, setDamageType] = React.useState("");
    const [weaponType, setWeaponType] = React.useState("");
    const [weaponRange, setWeaponRange] = React.useState("");
    const [properties, setProperties] = React.useState("");

    const fetchApiWeapons = async () => {
        const apiCall = await fetch("https://www.dnd5eapi.co/api/equipment-categories/weapon");
        const res = await apiCall.json();

        if (res.equipment) {
            let justNames = res.equipment.map(x => x.name);
            setApiWeapons(justNames);
        }
    }


    const handleModalClose = () => {
        setName("");
        setComment("");
        setWeight("0");
        setValueInGp("0");
        setQuantity("1");

        setDamageDice("");
        setDamageType("");
        setWeaponType("");
        setWeaponRange("");
        setProperties("");

        closeModal();
    }

    const save = async () => {
        if (oldBody === undefined && activeTab === 0 && weapon.length > 0) {
            let apiFriendly = weapon.toLowerCase().split(" ").join("-").replace(new RegExp(',', 'g'), '');
            let url = `https://www.dnd5eapi.co/api/equipment/${apiFriendly}`;
    
            const apiCall = await fetch(url);
            const res = await apiCall.json();
    
            let body = {
                Name: weapon,
                Weight: res.weight,
                ValueInGp: calculateValueInGp(res.cost),
                Quantity: 1,
                damageDice: `${res.damage.damage_dice}${res.damage.damage_bonus > 0 ? ` + ${res.damage.damage_bonus}` : ""}`,
                damageType: res.damage.damage_type.name,
                weaponType: res.weapon_category,
                weaponRange: res.weapon_range,
                properties: getProperties(res.properties, res["2h_damage"] !== undefined ? res["2h_damage"] : null, res.throw_range !== undefined ? res.throw_range : null, res.range !== undefined ? res.range : null)
            };

            console.log(body);
    
            handleModalClose(); 
            closeModal();
    
            onSave(body);

            return;
        }

        if (name.length <= 0 || damageDice.length <= 0 || damageType.length <= 0 || weaponType.length <= 0 || weaponRange.length <= 0)
            return;


        let body = {
            name,
            comment: comment === "" ? null : comment,
            weight: weight === "" ? 0 : parseFloat(weight), 
            valueInGp: valueInGp === "" ? 0 : parseFloat(valueInGp), 
            quantity: quantity === "" ? 0 : parseInt(quantity, 10),

            damageDice,
            damageType,
            weaponType,
            weaponRange,
            properties: properties === "" ? null : properties
        };

        if (oldBody) {
            body.id = oldBody.id;
        }

        handleModalClose(); 
        closeModal();

        onSave(body);
    }

    const setNumberValue = (method, newValue) => {
        if (newValue < 0) {

        } else 
        {
            method(newValue)
        }
    }

    React.useEffect(() => {
        if (oldBody) {
            setName(oldBody.name);
            setComment(oldBody.comment);
            setWeight(oldBody.weight);
            setValueInGp(oldBody.valueInGp);
            setQuantity(oldBody.quantity);

            setDamageDice(oldBody.damageDice);
            setDamageType(oldBody.damageType);
            setWeaponType(oldBody.weaponType);
            setWeaponRange(oldBody.weaponRange);
            setProperties(oldBody.properties === null ? "" : oldBody.properties);
        }

        fetchApiWeapons();
    }, []);


    if (oldBody === undefined) {
        return (
            <Modal onClose={() => handleModalClose()} onSave={(e) => save()} title={oldBody ? "Edit weapon" : "Create new weapon"}>
                <Tabs value={activeTab} onChange={(e, newValue) => setActiveTab(newValue)}>
                    <Tab label="Predefined"/>
                    <Tab label="Custom"/>
                </Tabs>
    
                <TabPanel value={activeTab} index={0}>
                    <div>
                    <Autocomplete
                        value={weapon}
                        onChange={(e, newValue) => setWeapon(newValue === null ? "" : newValue)}
                        freeSolo
                        disableClearable
                        options={apiWeapons}
                        renderInput={(params) => (
                            <TextField
                                {...params}
                                onKeyDown={(e) => setWeapon(e.target.value)}
                                label="Weapon"
                                InputProps={{...params.InputProps, type: 'search'}}
                                />
                        )}
                        />
                    </div>
                </TabPanel>
                
                <TabPanel value={activeTab} index={1}>
                    <div style={{display: "flex", flexDirection: "column"}}>
                        <TextField required name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                        <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>
    
                        <TextField type="number" step="0.01" name="weight" label="Weight" value={weight} onChange={(e) => setNumberValue(setWeight, e.target.value)}/>
                        <TextField type="number" step="0.01" name="valueInGp" label="Value in gold pieces" value={valueInGp} onChange={(e) => setNumberValue(setValueInGp, e.target.value)}/>
                        <TextField type="number" name="quantity" label="Quantity" value={quantity} onChange={(e) => setNumberValue(setQuantity, e.target.value)}/>

                        <TextField required name="damageDice" label="Damage dice" value={damageDice} onChange={(e) => setDamageDice(e.target.value)} title="Dice + bonuses"/>
                        <TextField required name="damageType" label="Damage type" value={damageType} onChange={(e) => setDamageType(e.target.value)} title="Acid, bludgeoning, cold, fire, force, lightning, necrotic, piercing, poison, psychic, radiant, slashing, thunder"/>
                        <TextField required name="weaponType" label="Weapon type" value={weaponType} onChange={(e) => setWeaponType(e.target.value)} title="Simple, martial"/>
                        <TextField required name="weaponRange" label="Weapon range" value={weaponRange} onChange={(e) => setWeaponRange(e.target.value)} title="Melee, ranged"/>
                        <TextField name="properties" label="Properties" value={properties} onChange={(e) => setProperties(e.target.value)}/>                
                    </div>
                </TabPanel>
            </Modal>
        );
    }

    return (
        <Modal onClose={() => handleModalClose()} onSave={(e) => save()} title={oldBody ? "Edit weapon" : "Create new weapon"}>
            <div style={{display: "flex", flexDirection: "column"}}>
                <TextField required name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>

                <TextField type="number" step="0.01" name="weight" label="Weight" value={weight} onChange={(e) => setNumberValue(setWeight, e.target.value)}/>
                <TextField type="number" step="0.01" name="valueInGp" label="Value in gold pieces" value={valueInGp} onChange={(e) => setNumberValue(setValueInGp, e.target.value)}/>
                <TextField type="number" name="quantity" label="Quantity" value={quantity} onChange={(e) => setNumberValue(setQuantity, e.target.value)}/>

                <TextField required name="damageDice" label="Damage dice" value={damageDice} onChange={(e) => setDamageDice(e.target.value)} title="Dice + bonuses"/>
                <TextField required name="damageType" label="Damage type" value={damageType} onChange={(e) => setDamageType(e.target.value)} title="Acid, bludgeoning, cold, fire, force, lightning, necrotic, piercing, poison, psychic, radiant, slashing, thunder"/>
                <TextField required name="weaponType" label="Weapon type" value={weaponType} onChange={(e) => setWeaponType(e.target.value)} title="Simple, martial"/>
                <TextField required name="weaponRange" label="Weapon range" value={weaponRange} onChange={(e) => setWeaponRange(e.target.value)} title="Melee, ranged"/>
                <TextField name="properties" label="Properties" value={properties} onChange={(e) => setProperties(e.target.value)}/>                       
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

function getProperties(properties, h_damage, throw_range, range) {
    let arr = properties.map(p => {
        if (p.name.toLowerCase() === "versatile")
            return `Versatile${h_damage !== null ? ` (${h_damage.damage_dice})` : ""}`

        if (p.name.toLowerCase() === "thrown")
            return `Thrown${throw_range !== null ? ` (${throw_range.normal}/${throw_range.long})` : ""}`

        if (p.name.toLowerCase() === "ammunition")
            return `Ammunition${range !== null ? ` (${range.normal}/${range.long})` : ""}`

        return p.name
    });

    return arr.join(", ");
}