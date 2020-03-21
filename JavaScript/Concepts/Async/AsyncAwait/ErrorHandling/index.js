"use-strict";

// Using catch() on the Function Call

/*
You should handle unexpected errors in your async functions 
in the calling function. The run() function shouldn't be 
responsible for handling every possible error, you should 
instead do run().catch(handleError).
*/

async function run() {
  await Promise.reject(new Error('Oops!'));
};

run().
  catch(function handleError(err) {
    console.error(err.message); // Oops!
  }).
  // Handle any errors in `handleError()`. If the error handler
  // throws an error, kill the process.
  catch(err => { process.nextTick(() => { throw err; }) });