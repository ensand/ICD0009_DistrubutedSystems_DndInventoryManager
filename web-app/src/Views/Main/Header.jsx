import React from 'react';

import {Link} from 'react-router-dom';

import AccountLinks from './AccountLinks.jsx';


function Header(props) {
    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div className="container">
                    <Link className="navbar-brand" to="/">Inventory manager</Link>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row">                            
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
                        <AccountLinks userIsLoggedIn={props.userIsLoggedIn}/>
                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Header;