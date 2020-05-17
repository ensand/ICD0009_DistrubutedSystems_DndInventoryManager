import React from 'react';

import {useStoreState} from 'easy-peasy';

import {Grid, Paper, Typography} from '@material-ui/core';

function View() {

    const token = useStoreState(state => state.appState.token);

    const [items, setItems] = React.useState([]);

    const fetchItems = async () => {
        const apiCall = await fetch("https://localhost:5001/api/v1.0/DndCharacters", {
                method: 'GET', 
                headers: {
                    "Content-Type": "application/json",
                    "Authorization":  `bearer ${token}`
                }
            });
        let data;
        try {
            data = await apiCall.json();
        } catch (e) {
            console.log("Error: ", e);
        }
        if (data !== undefined) {
            setItems(data);
        }
    }

    const deleteItem = async (id) => {
        await fetch(`https://localhost:5001/api/DndCharacters/${id}`, {method: 'DELETE'});
        fetchItems();
    }

    React.useEffect(() => {
        fetchItems();
    }, []);

    return (
        <div>
            <h1>Index</h1>
            <p><button>Create New</button></p>
            <Grid container>
                {items.map((item) => {
                    return <Grid item key={item.id} style={{minHeight: "10vh", minWidth: "15vw", width: "fit-content", height: "fit-content"}}>
                        <Paper style={{backgroundColor: "lightGray", padding: "1rem"}}>
                            <Typography variant="h5">{item.name}</Typography>
                            {item.comment && <Typography variant="subtitle2">{item.comment}</Typography>}
                            <Typography variant="body2">key</Typography>
                            <Typography variant="body1">value</Typography>
                        </Paper>
                    </Grid>
                })}
            </Grid>
            {/* <table className="table">
                <thead>
                    <tr>
                        <th>
                            Character
                        </th>
                        <th>
                            Total treasure in GP
                        </th>
                        <th>
                            Comment
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {items.map((item) => {
                        return (
                            <tr key={item.id}>
                                <td>
                                    {item.name}
                                </td>
                                <td>
                                    {item.totalTreasureInGp}
                                </td>
                                <td>
                                    {item.comment ? item.comment : "-"}
                                </td>
                                <td>
                                    <button>Edit</button> |
                                    <button onClick={() => deleteItem(item.id)}>Delete</button>
                                </td>
                            </tr>
                        );
                    })}
                </tbody>
            </table> */}
        </div>
    );
}

export default View;