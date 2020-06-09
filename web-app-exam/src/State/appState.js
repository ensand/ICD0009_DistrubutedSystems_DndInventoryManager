import {action} from 'easy-peasy';

const appState = {
    userLoggedIn: false,
    userIsAdmin: false,
    token: null,
    nickname: null,

    login: action((state, payload) => {
        state.userLoggedIn = true;
        state.token = payload.token;
        state.nickname = payload.nickname;
        state.userIsAdmin = payload.isAdmin;

        if (payload.rememberMe) {
            localStorage.setItem("appuser_rememberMe", payload.rememberMe);
            localStorage.setItem("appuser_email", payload.email);
            localStorage.setItem("appuser_token", payload.token);
            localStorage.setItem("appuser_password", payload.password);
        }
    }),

    logout: action((state) => {
        state.userLoggedIn = false;
        state.userIsAdmin = false;
        state.token = null;
        state.nickname = null;
        localStorage.clear();
    })
};

export default appState;