import React from 'react';

import {TextField} from '@material-ui/core';
import Modal from '../Modal/Modal.jsx';


export default function QuizModal(props) {
    const {closeModal, onSave} = props;

    const [title, setTitle] = React.useState("");


    const handleModalClose = () => {
        setTitle("");

        closeModal();
    }

    const save = () => {
        if (title.length === undefined || title.length === 0) {
            return;
        }
        let body = {title};

        closeModal();
        handleModalClose();

        onSave(body);
    }


    return (
        <Modal onClose={() => handleModalClose()} onSave={(e) => save()} title={"What will the quiz be called?"}>
            <div style={{display: "flex", flexDirection: "column"}}>
                <TextField required name="title" label="Title" value={title} onChange={(e) => setTitle(e.target.value)}/>
            </div>
        </Modal>
    );
}