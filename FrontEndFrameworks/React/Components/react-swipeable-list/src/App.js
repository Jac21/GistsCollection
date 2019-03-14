import React, { Component } from "react";
import "./styles/App.css";

import SwipeableListItem from "./SwipeableList/SwipeableListItem";
import SwipeableList from "./SwipeableList/SwipeableList";

const background = <span>Archive</span>;
const fakeContent = (
  <div className="FakeContent">
    <span>Swipe to delete</span>
  </div>
);

class App extends Component {
  render() {
    return (
      <div className="App">
        <SwipeableList background={background}>
          <SwipeableListItem>{fakeContent}</SwipeableListItem>
          <SwipeableListItem>{fakeContent}</SwipeableListItem>
          <SwipeableListItem>{fakeContent}</SwipeableListItem>
          <SwipeableListItem>{fakeContent}</SwipeableListItem>
          <SwipeableListItem>{fakeContent}</SwipeableListItem>
        </SwipeableList>
      </div>
    );
  }
}

export default App;
