import React from 'react';

import {useHistory} from 'react-router-dom';
import {Button, FormControl, IconButton, Input, InputAdornment, InputLabel, TextField} from '@material-ui/core';
import {VisibilityOff, Visibility} from '@material-ui/icons';

import {TranslateServerResponse} from '../../Utils/ServerResponse';
import {registerReq} from '../../Utils/AccountActions';


function Register() {
    const history = useHistory();

    const [email, setEmail] = React.useState(""); 
    const [firstName, setFirstName] = React.useState(""); 
    const [lastName, setLastName] = React.useState(""); 
    const [password, setPassword] = React.useState("");
    const [confirmPassword, setConfirmPassword] = React.useState("");

    const [showPassword, setShowPassword] = React.useState(false);
    const [error, setError] = React.useState(false);

    const register = async () => {
        if (email === "" || firstName === "" || lastName === "" || password === "" || confirmPassword === "") {
            setError("Make sure all inputs are filled.");
            return;
        } else if (password !== confirmPassword) {
            setError("Make sure password and passowrd confirmation are matching.");
            return;
        }

        const res = await registerReq({email, firstName, lastName, password});
        console.log(res)

        if (res !== undefined && res.status === "Registration successful") {
            setError(false);
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
            <h1>Register</h1>
            <div className="row">
                <div className="col-md-4">
                    <h4>Create a new account.</h4>
                    <hr />
                    <TextField required className="space-top-bottom" type="email" name="email" label="Email" value={email} fullWidth onChange={(e) => setEmail(e.target.value)}/>
                    <TextField required className="space-top-bottom" name="firstName" label="First name" value={firstName} fullWidth onChange={(e) => setFirstName(e.target.value)}/>
                    <TextField required className="space-top-bottom" name="lastName" label="Last name" value={lastName} fullWidth onChange={(e) => setLastName(e.target.value)}/>
                    
                    <FormControl fullWidth className="space-top-bottom">
                        <InputLabel htmlFor="password">Password</InputLabel>
                        <Input
                            required
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
                    Must be at least 6 characters long, contain a symbol, a number, a lowercase and an uppercase letter.

                    <TextField required className="space-top-bottom" type="password" name="confirmPassword" label="Confirm password" value={confirmPassword} fullWidth onChange={(e) => setConfirmPassword(e.target.value)}/>

                    <div className="space-top-bottom"><Button variant="contained" color="primary" onClick={() => register()}>Register</Button></div>
                </div>

                {error && <div className="col-md-8">
                    <h4>Nope.</h4>
                    <hr />
                    <div>Your registration attempt has failed.</div>
                    <div>{error}</div>
                </div>}
            </div>
        </div>
    );
}

export default Register;