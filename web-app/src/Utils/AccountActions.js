// const domain = "dndinventorymanagerapi.azurewebsites.net/api";
const domain = "https://localhost:5001/api";
const version = "v1.0";
  

async function login(body, loginWithToken) {
    let url = `${domain}/${version}/account/login${loginWithToken ? 'WithToken' : ''}`;

    const res = await fetch(url, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(body)
    }).then(response => {
        return response.json();
    });


    // var myHeaders = new Headers();
    // myHeaders.append("Content-Type", "application/json");

    // var raw = JSON.stringify({"email":"enola1998@gmail.com","password":"_Kibuvitsa196"});

    // var requestOptions = {
    //     method: 'POST',
    //     headers: myHeaders,
    //     body: raw,
    //     redirect: 'follow'
    // };

    // var res = fetch("dndinventorymanagerapi.azurewebsites.net/api/v1.0/account/login", requestOptions)
    //     .then(response => response.text())
    //     .then(result => console.log(result))
    //     .catch(error => console.log('error', error));

    console.log(res)

    return res;
}

async function register(body) {
    let url = `${domain}/${version}/account/register`;

    const res = await fetch(url, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(body)
    }).then(response => response.text())
    .catch(error => console.log('error', error));

    return res;
}

async function ApiGet(token, item, itemId) {
    let url = `${domain}/${version}/${item}${itemId ? `/${itemId}` : ''}`;

    const res = await fetch(url, {
        method: 'GET', 
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${token}`
        }
    });

    return res;
}

async function ApiPost(token, item, body) {
    let url = `${domain}/${version}/${item}`;

    const res = await fetch(url, {
        method: 'POST', 
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${token}`
        },
        body: JSON.stringify(body)
    });

    return res;
}

async function ApiPut(token, item, itemId, body) {
    let url = `${domain}/${version}/${item}/${itemId}`;

    const res = await fetch(url, {
        method: 'PUT', 
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${token}`
        },
        body: JSON.stringify(body)
    });

    return res;
}


async function ApiDelete(token, item, itemId) {
    let url = `${domain}/${version}/${item}${itemId ? `/${itemId}` : ''}`;

    const res = await fetch(url, {
        method: 'DELETE', 
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${token}`
        }
    });

    return res;
}



export {login as loginReq, register as registerReq, 
    ApiGet, ApiPost, ApiPut, ApiDelete};