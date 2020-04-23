import React from 'react';

import {Link} from 'react-router-dom';


function Login(props) {

    return (
        <ul className="navbar-nav">
            <li className="nav-item">
                <Link className="nav-link text-dark">Register</Link>
            </li>
            <li className="nav-item">
                <Link className="nav-link text-dark">Login</Link>
            </li>
        </ul>
    );
}

export default Login;