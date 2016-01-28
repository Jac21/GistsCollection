// actions to dispatch
const startingRequest = () => {
  return {
    type: "STARTING_REQUEST"
  }
}

const finishedRequest = (response) => {
  return {
    type: "FINISHED_REQUEST",
    response: response
  }
}

// dispatches startingRequest to indicate start of a new query,
// begin the async request, dispatch finishedRequest when done
// along with the results of the query
export const getGraph = (payload) => {
  return dispatch => {
    dispatch(startingRequest());
    return new Promise(function(resolve, reject) {
      let request=new XMLHttpRequest();
      request.open("POST", "/graphql", true);
      request.setRequestHeader("Content-Type", "application/graphql");
      request.send(payload);
      request.onreadystatechange = () => {
        if (request.readyState === 4) {
          resolve(request.responseText)
        }
      }
    }).then(response => dispatch(finishedRequest(JSON.parse(response))))
  }
}