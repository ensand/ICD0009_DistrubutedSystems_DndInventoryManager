import React from 'react';
import './App.css';

import {Switch, Route, useHistory} from 'react-router-dom';
import {useStoreState, useStoreActions} from 'easy-peasy';

import Header from './Views/Main/Header.jsx';
import Main from './Views/Main/Main.jsx';
import Footer from './Views/Main/Footer.jsx';
import Privacy from './Views/Privacy/Privacy.jsx';
import Error from './Views/Main/Error.jsx';

import Login from './Views/Account/Login.jsx';
import Register from './Views/Account/Register.jsx';
import AccountDetails from './Views/Account/AccountDetails.jsx';

import Quiz from './Views/Quiz/Quiz.jsx';
import QuizEdit from './Views/Quiz/QuizEdit.jsx';

import {loginReq} from './Utils/AccountActions';

function App() {
    const history = useHistory();

    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const userIsAdmin = useStoreState(state => state.appState.userIsAdmin);
    const loginAction = useStoreActions(state => state.appState.login);
    const logoutAction = useStoreActions(state => state.appState.logout);
    
    React.useEffect(() => {
        async function doLogin() {
            const res = await loginReq({email: localStorage.getItem("appuser_email"), password: localStorage.getItem("appuser_password"), token: localStorage.getItem("appuser_token"), refresh: true}); 
            if (res !== undefined && res.status === "Login successful") {
                loginAction({token: res.token, rememberMe: true, email: localStorage.getItem("appuser_email"), password: localStorage.getItem("appuser_password"), nickname: res.nickname, isAdmin: res.isAdmin});
            }
        } 

        if ((localStorage.getItem("appuser_rememberMe") === 'true' || localStorage.getItem("appuser_rememberMe") === true)
            && localStorage.getItem("appuser_token") !== undefined && localStorage.getItem("appuser_email") !== undefined
            && localStorage.getItem("appuser_password") !== undefined) {
                doLogin();                
        }

        return () => {
            if (!(localStorage.getItem("appuser_rememberMe") === 'true' || localStorage.getItem("appuser_rememberMe") === true)) {
                logoutAction();
                history.push("/");
            }
        };
    }, []);

    return (
        <div className="main">
            <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
            <Header/>
            <div className="container" style={{marginBottom: "80px"}}>
                <main role="main" className="pb-3">
                    <Switch>
                        <Route exact path="/">
                            <Main />
                        </Route>
                        <Route path="/Error">
                            <Error />
                        </Route>
                        <Route path="/Links">
                            <Privacy />
                        </Route>
                        <Route path="/ForgotPassword">
                            <div>Make a new account and write the password down.</div>
                        </Route>
                        {!userIsLoggedIn && <Route path="/Register">
                            <Register />
                        </Route>}
                        {!userIsLoggedIn && <Route path="/Login">
                            <Login />
                        </Route>}
                        {userIsLoggedIn && <Route path="/AccountDetails">
                            <AccountDetails />
                        </Route>}
                        <Route exact path="/quiz/:id">
                            <Quiz />
                        </Route>

                        <Route path="/quiz/:id/edit">
                            <QuizEdit />
                        </Route>
                    </Switch>
                </main>
            </div>
            <Footer />
        </div>
    );
}

export default App;
