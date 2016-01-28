import Immutable from 'immutable';

// state has two fields, one to let us know if we are in the middle
// of a query, and another that contains the response data
const immutableState = Immutable.Map({
  fetching: false,
  data: Immutable.Map({})
})

/*
Reducer function, STARTING_REQUEST action is dispatche when fetching state is true,
FINISHED_REQUEST for false, and data is set to response data
*/
export const queryReducer = (state = immutableState, action) => {
  switch (action.type) {
    case "STARTING_REQUEST":
      return state.set("fetching", true);
    case "FINISHED_REQUEST":
      return state.set("fetching", false).set("data", Immutable.Map(action.response.data.goldberg));
    default:
      return state
  }
}