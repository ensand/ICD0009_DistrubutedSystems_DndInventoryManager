import {createStore} from 'easy-peasy';

import appState from './appState.js';

const defaultStore = {
    appState
};

const store = createStore(defaultStore);

export default store;