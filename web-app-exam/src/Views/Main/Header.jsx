import React from 'react';

import {Link} from 'react-router-dom';

import AccountLinks from './AccountLinks.jsx';


function Header() {
    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                    <Link className="navbar-brand" to="/">Quiz <s>master</s> junior</Link>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row">                            
                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item">
                                {/* <Link className="nav-link text-dark" to="/Characters">Your characters</Link> */}
                            </li>
                        </ul>
                        <AccountLinks />
                    </div>
            </nav>
        </header>
    );
}

export default Header;