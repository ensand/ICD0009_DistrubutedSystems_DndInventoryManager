import React from 'react';

import {TextField} from '@material-ui/core';
import Modal from '../Modal/Modal.jsx';


export default function OtherEquipmentModal(props) {
    const {closeModal, onSave, oldBody} = props;

    const [name, setName] = React.useState("");
    const [comment, setComment] = React.useState("");
    const [weight, setWeight] = React.useState("0");
    const [valueInGp, setValueInGp] = React.useState("0");
    const [quantity, setQuantity] = React.useState("1");


    const handleModalClose = () => {
        setName("");
        setComment("");
        setWeight("0");
        setValueInGp("0");
        setQuantity("1");

        closeModal();
    }

    const save = () => {
        if (name.length <= 0)
            return;
            
        let body = {
            name,
            comment: comment === "" ? null : comment,
            weight: weight === "" ? 0 : parseFloat(weight), 
            valueInGp: valueInGp === "" ? 0 : parseFloat(valueInGp), 
            quantity: quantity === "" ? 0 : parseInt(quantity, 10), 
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
        }
    }, []);


    return (
        <Modal onClose={() => handleModalClose()} onSave={(e) => save()} title={oldBody ? "Edit equipment" : "Create new equipment"}>
            <div style={{display: "flex", flexDirection: "column"}}>
                <TextField required name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>

                <TextField type="number" step="0.01" name="weight" label="Weight" value={weight} onChange={(e) => setNumberValue(setWeight, e.target.value)}/>
                <TextField type="number" step="0.01" name="valueInGp" label="Value in gold pieces" value={valueInGp} onChange={(e) => setNumberValue(setValueInGp, e.target.value)}/>
                <TextField type="number" name="quantity" label="Quantity" value={quantity} onChange={(e) => setNumberValue(setQuantity, e.target.value)}/>
            </div>
        </Modal>
    );
}