import React from 'react';
import ReactDOM from 'react-dom';

import { createStore, applyMiddleware } from 'redux';
import { Provider } from 'react-redux';
import { queryReducer } from "./app/reducers/reducer.js";
import thunkMiddleware from 'redux-thunk'

import { QueryContainer } from "./app/components/query.js";

const Main = () => {
  return (
    <div>
      <QueryContainer />
    </div>
  )
};

// apply redux-thunk middleware
const createStoreWithMiddleware = applyMiddleware(
  thunkMiddleware
)(createStore)

// wrap main component in a Redux Provider nad pass in the queryReducer
ReactDOM.render(
  <Provider store={createStoreWithMiddleware(queryReducer)}>
    <Main />
  </Provider>,
  document.getElementById('example')
);