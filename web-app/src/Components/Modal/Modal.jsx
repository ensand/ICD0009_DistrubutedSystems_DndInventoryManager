import React from 'react';
import "./Modal.css";

import {Button, ClickAwayListener} from '@material-ui/core';

function CustomModal(props) {

    const {onSave, onClose, title} = props;

    const handleClose = () => {
        onClose();
    }

    return (
        <div>
            <div className="modal-backdrop"/>
            <ClickAwayListener onClickAway={() => handleClose()}>
                <form className="modal-container" onSubmit={(e) => onSave(e)}>
                    <h5 style={{gridArea: "header"}}>{title ? title : "[Title goes here]"}</h5>
                    <div style={{gridArea: "body"}}>
                        {props.children}
                    </div>
                    <div style={{gridArea: "footer"}} className="modal-footer">
                        <Button size="small" type="submit" variant="contained" color="primary">Save</Button>
                        <Button size="small" type="button" variant="contained" color="default" onClick={() => handleClose()}>Cancel</Button>
                    </div>
                </form>
            </ClickAwayListener>
        </div>
    );
}

export default CustomModal;