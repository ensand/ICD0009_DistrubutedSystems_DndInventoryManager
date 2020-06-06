import React from 'react';

import {CircularProgress, TextField} from '@material-ui/core';
import {Autocomplete} from '@material-ui/lab';
import Modal from '../Modal/Modal.jsx';


export default function MagicalItemModal(props) {
    const {closeModal, onSave, oldBody} = props;

    const [apiSpells, setApiSpells] = React.useState([]);
    const [open, setOpen] = React.useState(false);
    const loading = false;

    const [name, setName] = React.useState("");
    const [comment, setComment] = React.useState("");
    const [weight, setWeight] = React.useState("0");
    const [valueInGp, setValueInGp] = React.useState("0");
    const [quantity, setQuantity] = React.useState("1");
    const [spell, setSpell] = React.useState("");
    const [maxCharges, setMaxCharges] = React.useState("0");
    const [currentCharges, setCurrentCharges] = React.useState("0");

    const fetchApiSpells = async () => {
        const apiCall = await fetch("https://www.dnd5eapi.co/api/spells");
        const res = await apiCall.json();

        if (res.results) {
            let justNames = res.results.map(x => x.name);
            setApiSpells(justNames);
        }
    }


    const handleModalClose = () => {
        setName("");
        setComment("");
        setWeight("0");
        setValueInGp("0");
        setQuantity("1");
        setSpell("");
        setMaxCharges("0");
        setCurrentCharges("0");

        closeModal();
    }

    const save = () => {
        if (name.length <= 0 || currentCharges > maxCharges)
            return;

        let body = {
            name,
            comment: comment === "" ? null : comment,
            weight: weight === "" ? 0 : parseFloat(weight), 
            valueInGp: valueInGp === "" ? 0 : parseFloat(valueInGp), 
            quantity: quantity === "" ? 0 : parseInt(quantity, 10),
            spell,
            maxCharges: maxCharges === "" ? 0 : parseInt(maxCharges, 10),
            currentCharges: currentCharges === "" ? 0 : parseInt(currentCharges, 10),
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
            setComment(oldBody.comment === null ? "" : oldBody.comment);
            setWeight(oldBody.weight);
            setValueInGp(oldBody.valueInGp);
            setQuantity(oldBody.quantity);
            setSpell(oldBody.spell);
            setMaxCharges(oldBody.maxCharges);
            setCurrentCharges(oldBody.currentCharges);
        }

        fetchApiSpells();
    }, []);


    return (
        <Modal onClose={() => handleModalClose()} onSave={(e) => save()} title={oldBody ? "Edit item" : "Create new magical item"}>
            <div style={{display: "flex", flexDirection: "column"}}>
                <TextField required name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>

                <TextField type="number" step="0.01" name="weight" label="Weight" value={weight} onChange={(e) => setNumberValue(setWeight, e.target.value)}/>
                <TextField type="number" step="0.01" name="valueInGp" label="Value in gold pieces" value={valueInGp} onChange={(e) => setNumberValue(setValueInGp, e.target.value)}/>
                <TextField type="number" name="quantity" label="Quantity" value={quantity} onChange={(e) => setNumberValue(setQuantity, e.target.value)}/>

                {/* <TextField name="spell" label="Spell" value={spell} onChange={(e) => setSpell(e.target.value)} list="apiSpellDataList"/> */}
                {/* <input name="spell" value={spell} onChange={(e) => setSpell(e.target.value)} list="apiSpellDataList"/> */}
                {/* <datalist id="apiSpellDataList">
                    {apiSpells.map(spell => <option value={spell}/>)}
                </datalist> */}

                {/* <Autocomplete
                    freeSolo
                    value={spell}
                    onChange={(e, newValue) => setSpell(newValue)}
                    open={open}
                    onOpen={() => setOpen(true)}
                    onClose={() => setOpen(false)}
                    // getOptionSelected={(option, value) => option.name === value.name}
                    // getOptionLabel={option => option}
                    options={apiSpells}
                    loading={loading}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            label="Spell"
                            value={spell}
                            onKeyDown={(e) => setSpell(e.target.value)}
                            InputProps={{
                                ...params.InputProps,
                                endAdornment: (
                                    <React.Fragment>
                                        {loading ? <CircularProgress color="inherit" size={20} /> : null}
                                        {params.InputProps.endAdornment}
                                    </React.Fragment>
                                )
                            }}
                            />
                    )}
                    /> */}

                    <Autocomplete
                        value={spell}
                        onChange={(e, newValue) => setSpell(newValue)}
                        freeSolo
                        disableClearable
                        options={apiSpells}
                        renderInput={(params) => (
                            <TextField
                                {...params}
                                onKeyDown={(e) => setSpell(e.target.value)}
                                label="Spell"
                                InputProps={{...params.InputProps, type: 'search'}}
                                />
                        )}
                        />


                <TextField type="number" name="maxCharges" label="Max charges" value={maxCharges} onChange={(e) => setNumberValue(setMaxCharges, e.target.value)}/>
                <TextField type="number" name="currentCharges" label="Current charges" value={currentCharges} onChange={(e) => setNumberValue(setCurrentCharges, e.target.value)}/>
            </div>
        </Modal>
    );
}