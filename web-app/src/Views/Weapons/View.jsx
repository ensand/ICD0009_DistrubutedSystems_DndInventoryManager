import React from 'react';

function View() {

    const [items, setItems] = React.useState([]);

    const fetchItems = async () => {
        const apiCall = await fetch("https://localhost:5001/api/v1.0/Weapons");
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
        await fetch(`https://localhost:5001/api/Weapons/${id}`, {method: 'DELETE'});
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
                            Damage dice
                        </th>
                        <th>
                            Damage type
                        </th>
                        <th>
                            Weapon type
                        </th>
                        <th>
                            Weapon range
                        </th>
                        <th>
                            Properties
                        </th>
                        <th>
                            Weight
                        </th>
                        <th>
                            ValueInGp
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
                                    {item.damageDice}
                                </td>
                                <td>
                                    {item.damageType}
                                </td>
                                <td>
                                    {item.weaponType}
                                </td>
                                <td>
                                    {item.weaponRange}
                                </td>
                                <td>
                                    {item.properties}
                                </td>
                                <td>
                                    {item.weight}
                                </td>
                                <td>
                                    {item.valueInGp}
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