import React from 'react';

import {Link} from 'react-router-dom';


function AccountLinks(props) {

    const userIsLoggedIn = props.userIsLoggedIn;

    return (
        userIsLoggedIn ? 
        <ul className="navbar-nav">
            <li className="nav-item">
                <Link className="nav-link text-dark" to="/AccountDetails">Hello there, [user firstName goes here]</Link>
            </li>
            <li className="nav-item">
                <Link className="nav-link text-dark" to="/">Logout</Link>
            </li>
        </ul>

        : 
        <ul className="navbar-nav">
            <li className="nav-item">
                <Link className="nav-link text-dark" to="/Register">Register</Link>
            </li>
            <li className="nav-item">
                <Link className="nav-link text-dark" to="/Login">Login</Link>
            </li>
        </ul>
    );
}

export default AccountLinks;