import React from 'react';
import './App.css';

import Main from './Views/Main/Main.jsx';

function App() {
    return (
        <div className="main">
            <header>
                <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                    <div class="container">
                        <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">WebApp</a>
                        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                                aria-expanded="false" aria-label="Toggle navigation">
                            <span class="navbar-toggler-icon"></span>
                        </button>
                        <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                            <partial name="_LoginPartial" />
                            <ul class="navbar-nav flex-grow-1">
                                <li class="nav-item">
                                    <a class="nav-link text-dark">Home</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link text-dark">Characters</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link text-dark">Armor</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link text-dark">Weapons</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link text-dark">Magical items</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link text-dark">Other equipment</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
            </header>
            <div class="container">
                <main role="main" class="pb-3">
                    <Main />
                </main>
            </div>

            <footer class="border-top footer text-muted">
                <div class="container">
                    &copy; 2020 - WebApp - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
                </div>
            </footer>
        </div>
    );
}

export default App;
