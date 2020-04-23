import React from 'react';
import './App.css';

import {Switch, Route} from 'react-router-dom';

import Header from './Views/Main/Header.jsx';
import Main from './Views/Main/Main.jsx';
import Footer from './Views/Main/Footer.jsx';
import Privacy from './Views/Privacy/Privacy.jsx';
import Error from './Views/Main/Error.jsx';

import Login from './Views/Account/Login.jsx';
import Register from './Views/Account/Register.jsx';
import AccountDetails from './Views/Account/AccountDetails.jsx';

import CharacterView from './Views/DndCharacters/View.jsx';
import ArmorView from './Views/Armor/View.jsx';
import WeaponsView from './Views/Weapons/View.jsx';
import MagicalItemsView from './Views/MagicalItems/View.jsx';
import OtherEquipmentView from './Views/OtherEquipments/View.jsx';

function App() {

    const userIsLoggedIn = false;

    return (
        <div className="main">
            <Header userIsLoggedIn={userIsLoggedIn}/>
            <div className="container">
                <main role="main" className="pb-3">
                    <Switch>
                        <Route exact path="/">
                            <Main />
                        </Route>
                        <Route path="/Error">
                            <Error />
                        </Route>
                        <Route path="/Privacy">
                            <Privacy />
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
                        <Route path="/Characters">
                            <CharacterView />
                        </Route>
                        <Route path="/Armor">
                            <ArmorView />
                        </Route>
                        <Route path="/Weapons">
                            <WeaponsView />
                        </Route>
                        <Route path="/MagicalItems">
                            <MagicalItemsView />
                        </Route>
                        <Route path="/OtherEquipment">
                            <OtherEquipmentView />
                        </Route>
                    </Switch>
                </main>
            </div>
            <Footer />
        </div>
    );
}

export default App;
