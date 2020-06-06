import React from 'react';

import {Link} from 'react-router-dom';


function Footer(props) {

    return (
        <footer className="border-top text-muted" style={{position: "fixed", bottom: "0px", width: "100vw", height: "60px", lineHeight: "60px", background: "white"}}>
            <div className="container">
                &copy; 2020 - Inventory manager - <Link to="/Links">Other useful links</Link>
            </div>
        </footer>
    );
}

export default Footer;