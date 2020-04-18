import React from 'react';
import './App.css';

import {Link, Switch, Route} from 'react-router-dom';

import Main from './Views/Main/Main.jsx';
import CharacterView from './Views/DndCharacters/View.jsx';
import ArmorView from './Views/Armor/View.jsx';
import WeaponsView from './Views/Weapons/View.jsx';
import MagicalItemsView from './Views/MagicalItems/View.jsx';
import OtherEquipmentView from './Views/OtherEquipments/View.jsx';

function App() {
    return (
        <div className="main">
            <header>
                <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                    <div className="container">
                        <Link className="navbar-brand" to="/">WebApp</Link>
                        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                                aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                            {/* <partial name="_LoginPartial" /> */}
                            <ul className="navbar-nav flex-grow-1">
                                <li className="nav-item">
                                    <Link className="nav-link text-dark" to="/Characters">Characters</Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link text-dark" to="/Armor">Armor</Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link text-dark" to="/Weapons">Weapons</Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link text-dark" to="/MagicalItems">Magical items</Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link text-dark" to="/OtherEquipment">Other equipment</Link>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
            </header>
            <div className="container">
                <main role="main" className="pb-3">
                    <Switch>
                        <Route exact path="/">
                            <Main />
                        </Route>
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

            <footer className="border-top footer text-muted">
                <div className="container">
                    &copy; 2020 - WebApp - <Link to="/">Privacy</Link>
                </div>
            </footer>
        </div>
    );
}

export default App;
