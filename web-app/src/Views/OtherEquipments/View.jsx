import React from 'react';

import Modal from '../../Components/Modal/Modal.jsx';
import {TextField} from '@material-ui/core';

function View() {

    const [items, setItems] = React.useState([]);
    const [modalOpen, toggleModal] = React.useState(false);

    const fetchItems = async () => {
        const apiCall = await fetch("https://localhost:5001/api/OtherEquipments");
        const data = await apiCall.json();
        setItems(data);
    }

    const deleteItem = async (id) => {
        await fetch(`https://localhost:5001/api/OtherEquipments/${id}`, {method: 'DELETE'});
        fetchItems();
    }

    const createItem = async (e) => {
        e.preventDefault();
        const newObj = {
            name: e.target.elements.name.value,
            weight: parseFloat(e.target.elements.weight.value),
            valueInGp: parseFloat(e.target.elements.valueInGp.value),
            quantity: parseInt(e.target.elements.quantity.value, 10),
            comment: e.target.elements.comment.value === "" || e.target.elements.comment.value === undefined ? null : e.target.elements.comment.value
        };
        
        await fetch('https://localhost:5001/api/OtherEquipments', 
            {
                method: 'POST', 
                headers: {
                    "Content-Type": "application/json"
                }, body: JSON.stringify(newObj)
            });
        fetchItems();
    }

    React.useEffect(() => {
        fetchItems();
    }, []);

    return (
        <div>
            <h1>Index</h1>
            <p><button onClick={() => toggleModal(true)}>Create New</button></p>
            {modalOpen && 
                <Modal onClose={() => toggleModal(false)} onSave={(e) => {toggleModal(false); createItem(e);}} title="Create new equipment">
                    <div style={{display: "flex", flexDirection: "column"}}>
                        <TextField name="name" label="Name"/>
                        <TextField type="number" name="weight" step="0.01" label="Weight"/>
                        <TextField type="float" name="valueInGp" step="0.01" label="Value in gp"/>
                        <TextField type="number" name="quantity" label="Quantity"/>
                        <TextField name="comment" label="Comment"/>
                    </div>
                </Modal>}
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