import React from 'react';

function View() {

    const [items, setItems] = React.useState([]);

    const fetchItems = async () => {
        const apiCall = await fetch("https://localhost:5001/api/Armor");
        const data = await apiCall.json();
        setItems(data);
    }

    React.useEffect(() => {
        fetchItems();
    }, []);

    React.useEffect(() => {
        console.log(items);
    }, [items]);

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
                            ArmorType
                        </th>
                        <th>
                            AC
                        </th>
                        <th>
                            StealthDisadvantage
                        </th>
                        <th>
                            StrengthRequirement
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
                                    {item.armorType}
                                </td>
                                <td>
                                    {item.ac}
                                </td>
                                <td>
                                    {item.stealthDisadvantage ? "Yes" : "Nope"}
                                </td>
                                <td>
                                    {item.strenghtRequirement ? item.strenghtRequirement : "-"}
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
                                    <button>Details</button> |
                                    <button>Delete</button>
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