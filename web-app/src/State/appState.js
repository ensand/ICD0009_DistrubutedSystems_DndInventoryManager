import {action} from 'easy-peasy';

const appState = {
    userLoggedIn: false,
    token: null,
    userFirstName: null,

    login: action((state, payload) => {
        state.userLoggedIn = true;
        state.token = payload.token;
        state.userFirstName = payload.userFirstName;

        if (payload.rememberMe) {
            localStorage.setItem("appuser_rememberMe", payload.rememberMe);
            localStorage.setItem("appuser_email", payload.email);
            localStorage.setItem("appuser_token", payload.token);
            localStorage.setItem("appuser_password", payload.password);
        }
    }),

    logout: action((state) => {
        state.userLoggedIn = false;
        state.token = null;
        state.userFirstName = null;
        localStorage.clear();
    })
};

export default appState;