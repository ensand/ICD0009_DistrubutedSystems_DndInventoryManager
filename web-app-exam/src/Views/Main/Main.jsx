import React from 'react';

import {useStoreState} from 'easy-peasy';
import {useHistory} from 'react-router-dom';

import {ApiGet, UnauthorizedApiGet, ApiPost, ApiDelete} from '../../Utils/AccountActions';

import QuizModal from '../../Components/QuizModal/QuizModal.jsx';

import {Button, Grid, Paper, Typography} from '@material-ui/core';


function Main(props) {
    const history = useHistory();

    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const userIsAdmin = useStoreState(state => state.appState.userIsAdmin);
    const token = useStoreState(state => state.appState.token);

    const [items, setItems] = React.useState([]);

    const [modalOpen, toggleModal] = React.useState(false);

    const fetchItems = async () => {
        const apiCall = await UnauthorizedApiGet("Quiz");

        let data;
        try {
            data = await apiCall.json();
        } catch (e) {
            console.log("Error: ", e);
        }
        if (data !== undefined) {
            setItems(data);
        }
    }

    const handleModalClose = () => {
        toggleModal(false);
    }

    const makeNewQuiz = async (body) => {
        const apiCall = await ApiPost(token, "Quiz", body);
        
        if (apiCall.status === "200" || apiCall.status === 200) {
            await fetchItems();
        }
    }

    React.useEffect(() => {
        fetchItems();
    }, [userIsLoggedIn]);

    return (
        <div>
            <div className="text-center">
            <h1 className="display-4">Quiz <s>master</s> junior</h1>
            {!(userIsAdmin && userIsLoggedIn) && <div>These are private polls and you won't see what other people have answered but we will and we WILL laugh at your answers.</div>}

            {userIsLoggedIn && userIsAdmin && <Button variant="contained" size="large" color="primary" onClick={() => toggleModal(true)}>New quiz</Button>}
            {!userIsLoggedIn && "Log in or signup to take quizzes"}
        </div>
        <br/><br/><br/>
        <div>

        </div>
            <Grid container spacing={3}>
                {items.map((item) => {
                    return <Grid item key={item.id} style={{minHeight: "10vh", width: "20rem", height: "fit-content"}}>
                        <Paper style={{backgroundColor: "#ececec", padding: "1rem"}}>
                            <Typography variant="h5">{item.title}</Typography>
                            <Typography variant="subtitle2">Total questions: {item.questionCount}</Typography>
                            <hr/>
                            <div style={{display: "flex", justifyContent: "space-between"}}>
                                <Button variant="outlined" color="primary" size="small" onClick={() => history.push(`/quiz/${item.id}`)}>Take the quiz</Button>
                                {userIsAdmin && <Button variant="outlined" color="primary" size="small" onClick={() => history.push(`/quiz/${item.id}/edit`)}>Edit {"&"} details</Button>}
                            </div>
                        </Paper>
                    </Grid>
                })}
            </Grid>

            {modalOpen && <QuizModal closeModal={handleModalClose} onSave={(body) => makeNewQuiz(body)}/>}
        </div>
    );
}

export default Main;