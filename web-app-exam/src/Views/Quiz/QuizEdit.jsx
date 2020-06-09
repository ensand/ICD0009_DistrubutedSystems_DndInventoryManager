import React from 'react';

import {useStoreState} from 'easy-peasy';
import {useHistory, useParams} from 'react-router-dom';

import {ApiGet, GetQuizWithAnswers, ApiPut, ApiPost, ApiDelete} from '../../Utils/AccountActions';

import QuestionModal from '../../Components/QuestionModal/QuestionModal.jsx';

import {Button, ExpansionPanel, ExpansionPanelDetails, ExpansionPanelSummary, Grid, IconButton, Paper, TextField, Typography} from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import EditIcon from '@material-ui/icons/Edit';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';


export default function QuizEdit(props) {
    const {id} = useParams();

    const [modalOpen, toggleModal] = React.useState(false);

    const [item, setItem] = React.useState();

    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const token = useStoreState(state => state.appState.token);


    const fetchItem = async () => {
        const apiCall = await GetQuizWithAnswers(token, id);

        let data;
        try {
            data = await apiCall.json();
        } catch (e) {
            console.log("Error: ", e);
        }
        if (data !== undefined) {
            setItem(data);
        }
    }

    const addQuestion = async (_body) => {
        let body = {..._body, QuizId: item.id};
        console.log("NEW QUESTION", body)

        const apiCall = await ApiPost(token, "TextEntryQuestion", body);
        
        if (apiCall.status === "200" || apiCall.status === 200) {
            await fetchItem();
        }
    }

    React.useEffect(() => {
        fetchItem();
    }, [userIsLoggedIn]);

    console.log(item)

    if (!userIsLoggedIn || item === undefined || item.title === undefined)
        return <>Not found</>;

    return (
        <div>
            <div>
                <div style={{display: "flex", justifyContent: "space-between"}}>
                    <div style={{display: "flex", justifyContent: "space-between"}}>
                        <h1>{item.title}</h1>
                    </div>
                    <div style={{display: "flex", flexDirection: "column"}}>
                        <Button variant="contained" color="primary" onClick={() => toggleModal(true)}>Add question</Button>
                    </div>
                </div>

                {item.textEntryQuestions.map((question) => {
                        return (
                            <ExpansionPanel key={question.id}>
                                <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />} id={question.id}>
                                    <div style={{display: "flex", flexDirection: "column"}}>
                                        <Typography variant="body1"><b>{question.question}</b></Typography>
                                        <Typography variant="subtitle1">Answers: {question.length}</Typography>
                                    </div>
                                </ExpansionPanelSummary>
                                <ExpansionPanelDetails style={{display: "flex", flexDirection: "column"}}>
                                    {question.answers.map(answer => {
                                        return (
                                            <div key={answer.id} style={{borderTop: "1px solid lightgray", padding: "0.5rem"}}>
                                                {answer.answer}
                                            </div>
                                        );
                                    })}
                                </ExpansionPanelDetails>
                            </ExpansionPanel>
                        );
                    })}
            </div>



            {modalOpen && <QuestionModal closeModal={() => toggleModal(false)} onSave={(body) => addQuestion(body)}/>}
        </div>
    );
}