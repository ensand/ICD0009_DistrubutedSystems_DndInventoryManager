const domain = "https://dndinventorymanagerapi.azurewebsites.net/api";
// const domain = "https://localhost:5001/api";
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

    console.log(res)

    return res;
}

async function register(body) {
    let url = `${domain}/${version}/account/register`;
    console.log(url, body)

    const res = await fetch(url, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(body)
    }).then(response => response.json())
    .catch(error => console.log('error', error));
    console.log(res)

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
    console.log(url, body)

    const res = await fetch(url, {
        method: 'POST', 
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${token}`
        },
        body: JSON.stringify(body)
    });
    console.log(res)

    return res;
}

async function ApiPut(token, item, itemId, body) {
    let url = `${domain}/${version}/${item}/${itemId}`;
    console.log(url, body)

    const res = await fetch(url, {
        method: 'PUT', 
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${token}`
        },
        body: JSON.stringify(body)
    });
    console.log(res)

    return res;
}


async function ApiDelete(token, item, itemId) {
    let url = `${domain}/${version}/${item}/${itemId}`;
    console.log(url, item)

    const res = await fetch(url, {
        method: 'DELETE', 
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${token}`
        }
    });
    console.log(res)

    return res;
}



export {login as loginReq, register as registerReq, 
    ApiGet, ApiPost, ApiPut, ApiDelete};