import React from 'react';

import {TextField} from '@material-ui/core';
import Modal from '../Modal/Modal.jsx';


export default function CharacterModal(props) {
    const {closeModal, onSave, oldBody} = props;

    const [name, setName] = React.useState("");
    const [comment, setComment] = React.useState("");
    const [platinumPieces, setPlatinumPieces] = React.useState("");
    const [goldPieces, setGoldPieces] = React.useState("");
    const [electrumPieces, setElectrumPieces] = React.useState("");
    const [silverPieces, setSilverPieces] = React.useState("");
    const [copperPieces, setCoppperPieces] = React.useState("");


    const handleModalClose = () => {
        setName("");
        setComment("");
        setPlatinumPieces("");
        setGoldPieces("");
        setElectrumPieces("");
        setSilverPieces("");
        setCoppperPieces("");

        closeModal();
    }

    const save = () => {
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

        onSave(body);
    }

    React.useEffect(() => {
        if (oldBody) {
            setName(oldBody.name);
            setComment(oldBody.comment);
            setPlatinumPieces(oldBody.platinumPieces);
            setGoldPieces(oldBody.goldPieces);
            setElectrumPieces(oldBody.electrumPieces);
            setSilverPieces(oldBody.silverPieces);
            setCoppperPieces(oldBody.copperPieces);
        }
    }, []);


    return (
        <Modal onClose={() => handleModalClose()} onSave={(e) => {handleModalClose(); save();}} title={oldBody ? "Edit character" : "Create new character"}>
            <div style={{display: "flex", flexDirection: "column"}}>
                <TextField name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>

                <TextField type="number" name="platinumPieces" label="Platinum pieces" value={platinumPieces} onChange={(e) => setPlatinumPieces(e.target.value)}/>
                <TextField type="number" name="goldPieces" label="Gold pieces" value={goldPieces} onChange={(e) => setGoldPieces(e.target.value)}/>
                <TextField type="number" name="electrumPieces" label="Electrum pieces" value={electrumPieces} onChange={(e) => setElectrumPieces(e.target.value)}/>
                <TextField type="number" name="silverPieces" label="Silver pieces" value={silverPieces} onChange={(e) => setSilverPieces(e.target.value)}/>
                <TextField type="number" name="copperPieces" label="Coppper pieces" value={copperPieces} onChange={(e) => setCoppperPieces(e.target.value)}/>
            </div>
        </Modal>
    );
}