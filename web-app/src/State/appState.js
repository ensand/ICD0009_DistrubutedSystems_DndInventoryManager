import {action} from 'easy-peasy';

const appState = {
    userLoggedIn: false,
    token: null,

    login: action((state, payload) => {
        state.userLoggedIn = true;
        state.token = payload.token;

        sessionStorage.setItem("appuser_rememberMe", payload.rememberMe);
        sessionStorage.setItem("appuser_token", payload.token);
    }),

    logout: action((state) => {
        state.userLoggedIn = false;
        state.token = null;
        sessionStorage.clear();
    })
};

export default appState;