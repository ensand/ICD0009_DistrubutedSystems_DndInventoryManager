import React from 'react';

import {useStoreActions} from 'easy-peasy';
import {Link, useHistory} from 'react-router-dom';
import {Button, Checkbox, FormControl, FormControlLabel, IconButton, Input, InputAdornment, InputLabel, TextField} from '@material-ui/core';
import {VisibilityOff, Visibility} from '@material-ui/icons';

import {TranslateServerResponse} from '../../Utils/ServerResponse';
import {loginReq} from '../../Utils/AccountActions';


function Login() {
    const history = useHistory();

    const loginAction = useStoreActions(state => state.appState.login);

    const [email, setEmail] = React.useState(""); 
    const [password, setPassword] = React.useState("");
    const [rememberMe, setRememberMe] = React.useState(false);

    const [showPassword, setShowPassword] = React.useState(false);
    const [error, setError] = React.useState(false);

    const login = async () => {
        if (email === "" || password === "") {
            console.log("No.");
            return;
        }

        const res = await loginReq({email, password});


        if (res !== undefined && res.status === "Login successful") {
            setError(false);
            loginAction({token: res.token, rememberMe});
            history.push("/");
        } else {
            if (res === undefined) {
                setError("Something went wrong...");
            } else {
                setError(res.status);
            }
        }
    }

    return (
        <div>
            <h1>Log in</h1>
            <div className="row">
                <div className="col-md-4">
                    <h4>Use a local account to log in.</h4>
                    <hr />
                    <TextField className="space-top-bottom" type="email" name="email" label="Email" value={email} fullWidth onChange={(e) => setEmail(e.target.value)}/>
                    
                    <FormControl fullWidth className="space-top-bottom">
                        <InputLabel htmlFor="password">Password</InputLabel>
                        <Input
                            id="password"
                            type={showPassword ? 'text' : 'password'}
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            endAdornment={
                            <InputAdornment position="end">
                                <IconButton
                                    onClick={() => setShowPassword(!showPassword)}
                                    onMouseDown={(e) => e.preventDefault()}
                                    >
                                {showPassword ? <Visibility /> : <VisibilityOff />}
                                </IconButton>
                            </InputAdornment>
                            }
                        />
                    </FormControl>

                    <FormControlLabel
                        control={<Checkbox checked={rememberMe} onChange={(e) => setRememberMe(e.target.checked)} color="primary"/>}
                        label="Remember me?"/>
                    
                    <div className="space-top-bottom"><Button variant="contained" color="primary" onClick={() => login()}>Log in</Button></div>
                    <div>
                        <p><Link to="/ForgotPassword">Forgot your password?</Link></p>
                        <p><Link to="/Register">Register as a new user</Link></p>
                    </div>
                </div>

                {error && <div className="col-md-8">
                    <h4>Nope.</h4>
                    <hr />
                    <div>Your log-in attempt has failed.</div>
                    <div>{error}</div>
                </div>}
            </div>
        </div>
    );
}

export default Login;