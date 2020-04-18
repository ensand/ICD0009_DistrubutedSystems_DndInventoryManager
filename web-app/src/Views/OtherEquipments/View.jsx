import React from 'react';

function View() {

    const [items, setItems] = React.useState([]);

    const fetchItems = async () => {
        const apiCall = await fetch("https://localhost:5001/api/OtherEquipments");
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
            <p><a>Create New</a></p>
            <table className="table">
                <thead>
                    <tr>
                        <th>
                            Name
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
                                    <a>Edit</a> |
                                    <a>Details</a> |
                                    <a>Delete</a>
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