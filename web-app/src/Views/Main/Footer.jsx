import React from 'react';

import {Link} from 'react-router-dom';


function Footer(props) {

    return (
        <footer className="border-top footer text-muted">
            <div className="container">
                &copy; 2020 - Inventory manager - <Link to="/Privacy">Privacy</Link>
            </div>
        </footer>
    );
}

export default Footer;