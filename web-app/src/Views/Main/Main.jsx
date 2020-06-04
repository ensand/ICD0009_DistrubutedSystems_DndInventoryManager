import React from 'react';

function Main(props) {
    return (
        <div className="text-center">
            <h1 className="display-4">Dungeons and Dragons Inventory Manager</h1>

            <div style={{height: "30vh"}}>
                <img style={{height: "inherit"}} alt="dndLogo" src={require("../../Images/dndLogo.jpg")}/>
            </div>
            
            <p>Back-end made with <a href="https://dotnet.microsoft.com/">.NET Core</a>.</p>
            <p>Front-end made with <a href="https://reactjs.org/docs/create-a-new-react-app.html">React</a>.</p>
            <p>Using <a href="https://www.dnd5eapi.co/docs/">5e API</a> for basic item creation.</p>
            <p>Does not include beginner-friendly content and tutorials.</p>
            <div>
                Useful links:
                <div>
                    <div><a href="https://5e.tools/">5e tools</a> - all the details about all the books</div>
                    <div><a href="https://www.dndbeyond.com/">dndbeyond</a> - simpler version of rules</div>
                    <div><a href="https://www.dungeonmastersvault.com/">DM's vault</a> - make characters</div>
                    <div><a href="https://roll20.net/welcome">Roll20</a> -  play online (requires friends from the real world)</div>
                </div>
            </div>
        </div>
    );
}

export default Main;