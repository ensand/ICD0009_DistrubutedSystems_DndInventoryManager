import React from 'react';

function View() {

    const [items, setItems] = React.useState([]);

    const fetchItems = async () => {
        const apiCall = await fetch("https://localhost:5001/api/MagicalItems");
        const data = await apiCall.json();
        setItems(data);
    }

    const deleteItem = async (id) => {
        await fetch(`https://localhost:5001/api/MagicalItems/${id}`, {method: 'DELETE'});
        fetchItems();
    }

    React.useEffect(() => {
        fetchItems();
    }, []);

    return (
        <div>
            <h1>Index</h1>
            <p><button>Create New</button></p>
            <table className="table">
                <thead>
                    <tr>
                        <th>
                            Name
                        </th>
                        <th>
                            Spell
                        </th>
                        <th>
                            Max charges
                        </th>
                        <th>
                            Current charges
                        </th>
                        <th>
                            Value in ValueInGp
                        </th>
                        <th>
                            Weight
                        </th>
                        <th>
                            Quantity
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
                                    {item.spell}
                                </td>
                                <td>
                                    {item.maxCharges}
                                </td>
                                <td>
                                    {item.currentCharges}
                                </td>
                                <td>
                                    {item.valueInGp}
                                </td>
                                <td>
                                    {item.weight}
                                </td>
                                <td>
                                    {item.quantity}
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
            </table>
        </div>
    );
}

export default View;