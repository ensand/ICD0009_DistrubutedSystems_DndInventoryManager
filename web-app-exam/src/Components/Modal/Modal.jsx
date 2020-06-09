import React from 'react';
import "./Modal.css";

import {Button, ClickAwayListener} from '@material-ui/core';

function CustomModal(props) {

    const {onSave, onClose, title} = props;

    return (
        <div>
            <div className="modal-backdrop"/>
            <ClickAwayListener onClickAway={() => onClose()}>
                <div className="modal-container">
                    <h5 style={{gridArea: "header"}}>{title ? title : "[Title goes here]"}</h5>
                    <div style={{gridArea: "body"}}>
                        {props.children}
                    </div>
                    <div style={{gridArea: "footer"}} className="modal-footer">
                        <Button size="small" variant="contained" color="primary" onClick={() => onSave()}>Save</Button>
                        <Button size="small" variant="contained" color="default" onClick={() => onClose()}>Cancel</Button>
                    </div>
                </div>
            </ClickAwayListener>
        </div>
    );
}

export default CustomModal;