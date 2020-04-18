import React from 'react';

import {Link} from 'react-router-dom';

function View() {

    const [items, setItems] = React.useState([]);

    const fetchItems = async () => {
        const apiCall = await fetch("https://localhost:5001/api/DndCharacters");
        const data = await apiCall.json();
        setItems(data);
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
            <p><a>Create New</a></p>
            <table className="table">
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
                                    {item.name} (level {item.level})
                                </td>
                                <td>
                                    {item.totalTreasureInGp}
                                </td>
                                <td>
                                    {item.comment ? item.comment : "-"}
                                </td>
                                <td>
                                    <a>Edit</a> |
                                    <button onClick={() => deleteItem(item.id)}>Delete</button>
                                </td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
}

export default View;