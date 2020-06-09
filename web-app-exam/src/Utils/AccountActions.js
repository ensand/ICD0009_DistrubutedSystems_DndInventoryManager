const domain = "https://localhost:5001/api";  

async function login(body) {
    let url = `${domain}/account/login`;
    console.log(url, body);

    const res = await fetch(url, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(body)
    }).then(response => {
        console.log("responseeeeeeeeee ", response)
        return response.json();
    });

    console.log(res)

    return res;
}

async function register(body) {
    let url = `${domain}/account/register`;
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

async function GetQuizWithAnswers(token, itemId) {
    let url = `${domain}/QuizControllerAdmin/GetQuizWithAnswers/${itemId}`;

    const res = await fetch(url, {
        method: 'GET', 
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${token}`
        }
    });
    console.log(res)

    return res;
}


async function UnauthorizedApiGet(item, itemId) {
    let url = `${domain}/${item}${itemId ? `/${itemId}` : ''}`;
    console.log(url)

    const res = await fetch(url, {
        method: 'GET', 
        headers: {
            "Content-Type": "application/json"
        }
    });
    console.log(res)

    return res;
}

async function ApiGet(token, item, itemId) {
    let url = `${domain}/${item}${itemId ? `/${itemId}` : ''}`;

    const res = await fetch(url, {
        method: 'GET', 
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${token}`
        }
    });

    return res;
}


async function UnauthorizedApiPost(item, body) {
    let url = `${domain}/${item}`;
    console.log(url, body)

    const res = await fetch(url, {
        method: 'POST', 
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(body)
    });
    console.log(res)

    return res;
}

async function ApiPost(token, item, body) {
    let url = `${domain}/${item}`;
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
    let url = `${domain}/${item}/${itemId}`;
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
    let url = `${domain}/${item}/${itemId}`;
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
    ApiGet, UnauthorizedApiGet, GetQuizWithAnswers, ApiPost, UnauthorizedApiPost, ApiPut, ApiDelete};