import React from 'react';

import {TextField} from '@material-ui/core';
import Modal from '../Modal/Modal.jsx';


export default function NicknameModal(props) {
    const {closeModal, onSave} = props;

    const [nickname, setNickname] = React.useState("");


    const handleModalClose = () => {
        setNickname("");

        closeModal();
    }

    const save = () => {
        if (nickname.length === undefined || nickname.length === 0) {
            return;
        }
        // let body = {nickname};

        closeModal();
        handleModalClose();

        onSave(nickname);
    }


    return (
        <Modal onClose={() => handleModalClose()} onSave={(e) => save()} title={"You are not logged in. To submit your answer please enter a nickname"}>
            <div style={{display: "flex", flexDirection: "column"}}>
                <TextField required name="nickname" label="Nickname" value={nickname} onChange={(e) => setNickname(e.target.value)}/>
            </div>
        </Modal>
    );
}