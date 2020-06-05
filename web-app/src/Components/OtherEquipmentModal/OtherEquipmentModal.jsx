import React from 'react';

import {TextField} from '@material-ui/core';
import Modal from '../Modal/Modal.jsx';


export default function OtherEquipmentModal(props) {
    const {closeModal, onSave, oldBody} = props;

    const [name, setName] = React.useState("");
    const [comment, setComment] = React.useState("");
    const [weight, setWeight] = React.useState("");
    const [valueInGp, setValueInGp] = React.useState("");
    const [quantity, setQuantity] = React.useState("");


    const handleModalClose = () => {
        setName("");
        setComment("");
        setWeight("");
        setValueInGp("");
        setQuantity("");

        closeModal();
    }

    const save = () => {
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
                <TextField name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>

                <TextField type="number" step="0.01" name="weight" label="Weight" value={weight} onChange={(e) => setWeight(e.target.value)}/>
                <TextField type="number" step="0.01" name="valueInGp" label="Value in gold pieces" value={valueInGp} onChange={(e) => setValueInGp(e.target.value)}/>
                <TextField type="number" name="quantity" label="Quantity" value={quantity} onChange={(e) => setQuantity(e.target.value)}/>
            </div>
        </Modal>
    );
}