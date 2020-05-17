import {useStoreState} from 'easy-peasy';

const domain = "https://localhost:5001/api";
const version = "v1.0";


async function login(body, loginWithToken) {
    let url = `${domain}/${version}/account/login${loginWithToken ? 'WithToken' : ''}`;

    const res = await fetch(url, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(body)
    }).then(response => response.json())
    .catch(error => console.log('error', error));

    return res;
}

async function ApiGet(item, itemId) {
    let url = `${domain}/${version}/${item}${itemId ? `/${itemId}` : ''}`;

    const userLoggedIn = useStoreState(state => state.appState.userLoggedIn);
    const token = useStoreState(state => state.appState.token);

    if (userLoggedIn) {
        const res = await fetch(url, {
            method: "GET",
            hearders: {
                "Content-Type": "application/json",
                "Authorization": `bearer ${token}`
            }
        }).then(response => response.json())
        .catch(error => console.log('error', error));

        return res;
    }
    return null;
}


export {login as loginReq};