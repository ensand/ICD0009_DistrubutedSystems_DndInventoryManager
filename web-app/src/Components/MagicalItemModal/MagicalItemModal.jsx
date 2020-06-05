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
    const [weight, setWeight] = React.useState("");
    const [valueInGp, setValueInGp] = React.useState("");
    const [quantity, setQuantity] = React.useState("");
    const [spell, setSpell] = React.useState("");
    const [maxCharges, setMaxCharges] = React.useState("");
    const [currentCharges, setCurrentCharges] = React.useState("");

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
        setWeight("");
        setValueInGp("");
        setQuantity("");
        setSpell("");
        setMaxCharges("");
        setCurrentCharges("");

        closeModal();
    }

    const save = () => {
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
            setSpell(oldBody.spell);
            setMaxCharges(oldBody.maxCharges);
            setCurrentCharges(oldBody.currentCharges);
        }

        fetchApiSpells();
    }, []);


    return (
        <Modal onClose={() => handleModalClose()} onSave={(e) => {handleModalClose(); save();}} title={oldBody ? "Edit equipment" : "Create new equipment"}>
            <div style={{display: "flex", flexDirection: "column"}}>
                <TextField name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>

                <TextField type="number" step="0.01" name="weight" label="Weight" value={weight} onChange={(e) => setWeight(e.target.value)}/>
                <TextField type="number" step="0.01" name="valueInGp" label="Value in gold pieces" value={valueInGp} onChange={(e) => setValueInGp(e.target.value)}/>
                <TextField type="number" name="quantity" label="Quantity" value={quantity} onChange={(e) => setQuantity(e.target.value)}/>

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
                                // value={spell}
                                onKeyDown={(e) => setSpell(e.target.value)}
                                label="Spell"
                                InputProps={{...params.InputProps, type: 'search'}}
                                />
                        )}
                        />


                <TextField type="number" name="maxCharges" label="Max charges" value={maxCharges} onChange={(e) => setMaxCharges(e.target.value)}/>
                <TextField type="number" name="currentCharges" label="Current charges" value={currentCharges} onChange={(e) => setCurrentCharges(e.target.value)}/>
            </div>
        </Modal>
    );
}