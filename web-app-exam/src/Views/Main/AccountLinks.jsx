import React from 'react';

import {Link} from 'react-router-dom';
import {useStoreState, useStoreActions} from 'easy-peasy';


function AccountLinks(props) {

    const userIsLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const nickname = useStoreState(state => state.appState.nickname);
    const logout = useStoreActions(state => state.appState.logout);

    return (
        userIsLoggedIn ? 
        <ul className="navbar-nav">
            <li className="nav-item">
            <Link className="nav-link text-dark" to="/AccountDetails">Hello there, {nickname}</Link>
            </li>
            <li className="nav-item">
                <Link className="nav-link text-dark" to="/" onClick={() => logout()}>Logout</Link>
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