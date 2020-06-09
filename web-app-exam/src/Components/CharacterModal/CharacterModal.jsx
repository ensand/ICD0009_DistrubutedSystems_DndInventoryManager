import React from 'react';

import {TextField} from '@material-ui/core';
import Modal from '../Modal/Modal.jsx';


export default function CharacterModal(props) {
    const {closeModal, onSave, oldBody} = props;

    const [name, setName] = React.useState("");
    const [comment, setComment] = React.useState("");
    const [platinumPieces, setPlatinumPieces] = React.useState("0");
    const [goldPieces, setGoldPieces] = React.useState("0");
    const [electrumPieces, setElectrumPieces] = React.useState("0");
    const [silverPieces, setSilverPieces] = React.useState("0");
    const [copperPieces, setCopperPieces] = React.useState("0");


    const handleModalClose = () => {
        setName("0");
        setComment("0");
        setPlatinumPieces("0");
        setGoldPieces("0");
        setElectrumPieces("0");
        setSilverPieces("0");
        setCopperPieces("0");

        closeModal();
    }

    const save = () => {
        if (name.length === undefined || name.length === 0) {
            return;
        }
        let body = {
            name,
            comment: comment === "" ? null : comment,
            platinumPieces: platinumPieces === "" ? 0 : parseInt(platinumPieces, 10), 
            goldPieces: goldPieces === "" ? 0 : parseInt(goldPieces, 10), 
            electrumPieces: electrumPieces === "" ? 0 : parseInt(electrumPieces, 10), 
            silverPieces: silverPieces === "" ? 0 : parseInt(silverPieces, 10),
            copperPieces: copperPieces === "" ? 0 : parseInt(copperPieces, 10)
        };

        closeModal();
        handleModalClose();

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
            setPlatinumPieces(oldBody.platinumPieces);
            setGoldPieces(oldBody.goldPieces);
            setElectrumPieces(oldBody.electrumPieces);
            setSilverPieces(oldBody.silverPieces);
            setCopperPieces(oldBody.copperPieces);
        }
    }, []);


    return (
        <Modal onClose={() => handleModalClose()} onSave={(e) => save()} title={oldBody ? "Edit character" : "Create new character"}>
            <div style={{display: "flex", flexDirection: "column"}}>
                <TextField required name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>

                <TextField type="number" name="platinumPieces" label="Platinum pieces" value={platinumPieces} onChange={(e) => setNumberValue(setPlatinumPieces, e.target.value)}/>
                <TextField type="number" name="goldPieces" label="Gold pieces" value={goldPieces} onChange={(e) => setNumberValue(setGoldPieces, e.target.value)}/>
                <TextField type="number" name="electrumPieces" label="Electrum pieces" value={electrumPieces} onChange={(e) => setNumberValue(setElectrumPieces, e.target.value)}/>
                <TextField type="number" name="silverPieces" label="Silver pieces" value={silverPieces} onChange={(e) => setNumberValue(setSilverPieces, e.target.value)}/>
                <TextField type="number" name="copperPieces" label="Copper pieces" value={copperPieces} onChange={(e) => setNumberValue(setCopperPieces, e.target.value)}/>
            </div>
        </Modal>
    );
}