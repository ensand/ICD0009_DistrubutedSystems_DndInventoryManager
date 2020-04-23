import React from 'react';

import Modal from '../../Components/Modal/Modal.jsx';
import {Button, TextField} from '@material-ui/core';

function View() {

    const [items, setItems] = React.useState([]);
    const [modalOpen, toggleModal] = React.useState(false);
    const [uploadType, setUploadType] = React.useState();
    const [itemId, setItemId] = React.useState();

    const [name, setName] = React.useState("");
    const [weight, setWeight] = React.useState("");
    const [valueInGp, setValueInGp] = React.useState("");
    const [quantity, setQuantity] = React.useState("");
    const [comment, setComment] = React.useState("");

    const fetchItems = async () => {
        const apiCall = await fetch("https://localhost:5001/api/OtherEquipments");
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
        await fetch(`https://localhost:5001/api/OtherEquipments/${id}`, {method: 'DELETE'});
        fetchItems();
    }

    const createItem = async () => {
        const newObj = {
            name,
            weight: parseFloat(weight),
            valueInGp: parseFloat(valueInGp),
            quantity: quantity.value === undefined ? 1 : parseInt(quantity.value, 10),
            comment: comment === "" ? null : comment
        };

        if (uploadType === 'PUT' && itemId) {
            newObj["id"] = itemId;
        }
        
        await fetch(`https://localhost:5001/api/OtherEquipments/${uploadType === 'PUT' && itemId ? itemId : ""}`, 
            {
                method: uploadType, 
                headers: {
                    "Content-Type": "application/json"
                }, body: JSON.stringify(newObj)
            });
        fetchItems();
    }

    const editItem = async (id) => {
        const item = await fetch(`https://localhost:5001/api/OtherEquipments/${id}`, {method: 'GET', headers: {"Content-Type": "application/json"}})
            .then(response => response.json())
            .catch(error => console.log('error', error));
        
        toggleModal(true);
        setUploadType('PUT');
        setItemId(id);

        setName(item.name);
        setWeight(item.weight);
        setValueInGp(item.valueInGp);
        setQuantity(item.quantity);
        setComment(item.comment ? item.comment : "");
    }

    const handleModalClose = () => {
        setName("");
        setWeight("");
        setValueInGp("");
        setQuantity("");
        setComment("");

        toggleModal(false);
    }

    React.useEffect(() => {
        fetchItems();
    }, []);

    return (
        <div>
            <h1>Index</h1>
            <p><Button variant="contained" color="primary" onClick={() => {setUploadType('POST'); toggleModal(true);}}>Create New</Button></p>
            {modalOpen && 
                <Modal onClose={() => handleModalClose()} onSave={(e) => {handleModalClose(); createItem();}} title="Create new equipment">
                    <div style={{display: "flex", flexDirection: "column"}}>
                        <TextField name="name" label="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                        <TextField type="number" name="weight" step="0.01" label="Weight" value={weight} onChange={(e) => setWeight(e.target.value)}/>
                        <TextField type="float" name="valueInGp" step="0.01" label="Value in gp" value={valueInGp} onChange={(e) => setValueInGp(e.target.value)}/>
                        <TextField type="number" name="quantity" label="Quantity" value={quantity} onChange={(e) => setQuantity(e.target.value)}/>
                        <TextField name="comment" label="Comment" value={comment} onChange={(e) => setComment(e.target.value)}/>
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
                                    <Button variant="contained" size="small" onClick={() => editItem(item.id)}>Edit</Button>
                                    <Button variant="contained" size="small" onClick={() => deleteItem(item.id)}>Delete</Button>
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