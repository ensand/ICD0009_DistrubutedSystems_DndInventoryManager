import React from 'react';

import {useStoreState} from 'easy-peasy';
import {useHistory, useParams} from 'react-router-dom';

import {ApiGet, ApiPost} from '../../Utils/AccountActions';


import {Button, TextField, Typography} from '@material-ui/core';


export default function Quiz(props) {
    const history = useHistory();
    const {id} = useParams();

    const [item, setItem] = React.useState();

    const [questionAnswers, setQuestionAnswers] = React.useState([]);

    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const token = useStoreState(state => state.appState.token);

    const fetchItem = async () => {
        const apiCall = await ApiGet(token, "Quiz", id);

        let data;
        try {
            data = await apiCall.json();
        } catch (e) {
            console.log("Error: ", e);
        }
        if (data !== undefined) {
            setItem(data);
            setQuestionAnswers(data.textEntryQuestions.map(q => {return {TextEntryQuestionId: q.id, answer: ""}}));
        }
    }

    const changeAnswer = (id, newValue) => {
        var state = [...questionAnswers];
        var index = state.findIndex(x => x.TextEntryQuestionId === id);
        if (index === -1)
            return;

        var newItem = {...state[index]};
        newItem.answer = newValue;

        state.splice(index, 1, newItem);

        setQuestionAnswers(state);
    }

    const answerQuestions = async () => {
        var returnToHomePage = true;

        for (var i = 0; i < item.textEntryQuestions.length; i++) {
            var question = item.textEntryQuestions[i];
            let pieceOfState = questionAnswers.find(q => q.TextEntryQuestionId === question.id);
            if (pieceOfState === undefined) {
                console.log("Something went wrong with ", question.id);
                return;
            }
            let body = {...pieceOfState};

            const apiCall = await ApiPost(token, "TextEntryAnswer", body);
            
            if (apiCall.status !== 200) {
                console.log("Something went wrong with ", body);
                returnToHomePage = false;
            }
            
        }

        if (returnToHomePage) {
            history.push("/");
        }
    }

    React.useEffect(() => {
        fetchItem();
    }, [userIsLoggedIn]);


    if (!userIsLoggedIn || item === undefined || item.title === undefined)
        return <>Not found</>;

    return (
        <div>
            <div>
                <h1>{item.title}</h1>                    

                {item.textEntryQuestions.map((question) => {
                    var pieceOfState = questionAnswers.find(q => q.TextEntryQuestionId === question.id);
                    if (pieceOfState === undefined)
                        return;

                    return (
                        <div style={{borderTop: "1px solid lightgray", padding: "1rem"}} key={question.id}>
                            <Typography>{question.question}</Typography>
                            <TextField required fullWidth value={pieceOfState.answer} onChange={(e) => changeAnswer(question.id, e.target.value)}/>
                        </div>
                    );
                })}

                <Button variant="contained" color="primary" onClick={() => answerQuestions()}>Submit</Button>
                <Button variant="outlined" color="primary" onClick={() => history.push("/")}>Cancel</Button>
            </div>
        </div>
    );
}