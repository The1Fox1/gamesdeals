import React from "react";
import { Route, withRouter } from "react-router-dom";
// import logo from "./logo.svg";
import Home from "./components/Home.jsx";
import "./App.css";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img
          src="https://sledsworth.gallerycdn.vsassets.io/extensions/sledsworth/react-redux-es6-snippets/0.5.3/1530106605209/Microsoft.VisualStudio.Services.Icons.Default"
          className="App-logo"
          alt="logo"
        />
        <span style={{ marginLeft: "auto", marginRight: "auto" }}>
          React-Redux Graduation Project: Game Deals from 'Deuls Vult'
        </span>
      </header>
      <Route path="/" component={Home} />
      <footer className="container">
        <p>&copy; Company 2017-2018</p>
      </footer>
    </div>
  );
}

export default withRouter(App);
