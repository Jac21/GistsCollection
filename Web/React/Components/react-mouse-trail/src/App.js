import React from 'react';
import logo from './logo.svg';
import './App.css';

import MouseTrail from './Components/MouseTrail/MouseTrail';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <MouseTrail />
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
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
