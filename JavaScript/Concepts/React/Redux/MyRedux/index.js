"use strict";

// simplified createStore function
const createStore = (reducer) => {
  // stores subscribers, gets alerted of dispatches
  let listeners = [];

  // calling reducer with undefined and an empty object
  // returns the initial state, need getState() method
  let currentState = reducer(undefined, {});

  return {
    // returns the latest state from the store
    getState: () => currentState,
    // feeds an action and the current state to reducer
    // to get a new state, notifies listener
    dispatch: (action) => {
      currentState = reducer(currentState, action);

      listeners.forEach((listener) => {
        listener();
      });
    },
    // lets you be notified when store receives an action
    subscribe: (newListener) => {
      listeners.push(newListener);

      const unsubscribe = () => {
        listeners = listeners.filter((l) => l !== newListener);
      };

      return unsubscribe;
    }
  };
}

// architectual pieces
const initialState = { count: 0 };

const actions = {
  increment: { type: 'INCREMENT' },
  decrement: { type: 'DECREMENT' }
};

const countReducer = (state = initialState, action) => {
  switch (action.type) {
    case actions.increment.type:
      return {
        count: state.count + 1
      };

    case actions.decrement.type:
      return {
        count: state.count - 1
      };

    default:
      return state;
  }
};

const store = createStore(countReducer);

// DOM elements
const incrementButton = document.querySelector('.increment');
const decrementButton = document.querySelector('.decrement');

// Wire click events to actions
incrementButton.addEventListener('click', () => {
  store.dispatch(actions.increment);
});

decrementButton.addEventListener('click', () => {
  store.dispatch(actions.decrement);
});

// Initialize UI display
const counterDisplay = document.querySelector('h1');
counterDisplay.innerHTML = parseInt(initialState.count);

// Update UI when an action fires
store.subscribe(() => {
  const state = store.getState();

  counterDisplay.innerHTML = parseInt(state.count);
});