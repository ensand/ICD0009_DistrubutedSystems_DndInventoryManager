import React from 'react';

import {TextField} from '@material-ui/core';
import Modal from '../Modal/Modal.jsx';


export default function QuestionModal(props) {
    const {closeModal, onSave} = props;

    const [question, setQuestion] = React.useState("");


    const handleModalClose = () => {
        setQuestion("");

        closeModal();
    }

    const save = () => {
        if (question.length === undefined || question.length === 0) {
            return;
        }
        let body = {question};

        closeModal();
        handleModalClose();

        onSave(body);
    }


    return (
        <Modal onClose={() => handleModalClose()} onSave={(e) => save()} title={"Enter the question"}>
            <div style={{display: "flex", flexDirection: "column"}}>
                <TextField required name="question" label="Question" value={question} onChange={(e) => setQuestion(e.target.value)}/>
            </div>
        </Modal>
    );
}