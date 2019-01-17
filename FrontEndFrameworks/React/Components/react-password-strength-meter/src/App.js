import React, { Component } from "react";
import logo from "./logo.svg";
import "./App.css";

import PasswordStrengthMeter from "./components/PasswordStrengthMeter";

class App extends Component {
  constructor() {
    super();

    this.state = {
      password: ""
    };
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn React by building a Password Strength Meter
          </a>
        </header>
        <div className="password-strength-meter-wrapper">
          <div className="meter">
            <input
              autoComplete="off"
              type="password"
              onChange={e => this.setState({ password: e.target.value })}
            />
            <PasswordStrengthMeter password={this.state.password} />
          </div>
        </div>
      </div>
    );
  }
}

export default App;
