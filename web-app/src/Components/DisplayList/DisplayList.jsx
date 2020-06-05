import React from 'react';
import {Typography} from '@material-ui/core';


export default function DisplayList(props) {
    const {displayItems, displayHeadings, itemId} = props;

    return (
        <div style={{display: "grid", gridTemplateColumns: "1fr", gridColumnGap: "1rem", justifyItems: "start", width: "100%"}}>
            {displayItems.map((displayItem, index) => {
                if (displayItem === undefined)
                    return <></>;

                return <div key={`${itemId}_${index}`} style={{display: "flex", justifyContent: "space-between", width: "inherit", borderBottom: "1px solid lightgray", padding: "5px 0 5px 0"}}>
                    <Typography variant="body1">{displayHeadings[index]}:</Typography>
                    <Typography variant="body1">{typeof displayItem === "boolean" ? JSON.stringify(displayItem) : displayItem}</Typography>
                </div>
            })}
        </div>
    );
}