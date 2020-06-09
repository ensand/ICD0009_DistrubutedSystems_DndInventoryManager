function TranslateServerResponse(props) {
    switch(props.res) {
        case 400:
            return "Bad request";
        case 401:
            return "Unauthorized";
        case 403:
            return "Forbidden";
        case 404:
            return "Not found";
        case 408:
            return "Request timeout";
        case 413:
            return "Payload too large. Fuck off you bloody wanker-hacker >:(";

        case 500:
            return "Internal server error";
        case 501:
            return "Not implemented";
        case 502:
            return "Bad gateway";
        case 503:
            return "Service unavailable";
        case 504:
            return "Gateway timeout";
        case 507:
            return "Insufficient Storage";

        default:
            return "[not handled]";
    }
}

export {TranslateServerResponse};