
const domain = "https://localhost:5001/api/";


async function login(body, loginWithToken) {
    let url = `${domain}account/login${loginWithToken ? 'WithToken' : ''}`;

    const res = await fetch(url, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(body)
    }).then(response => response.json())
    .catch(error => console.log('error', error));

    return res;
}


export {login as loginReq};