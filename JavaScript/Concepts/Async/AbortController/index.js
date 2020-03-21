"use-strict";

// Abort signal
const abortController = new AbortController();
const abortSignal = abortController.signal;

fetch('https://api.github.com/repos/Jac21/GistsCollection', {
  signal: abortSignal
}).catch(({ message }) => {
  console.log(message);
});

abortController.abort(); // The user aborted a request.

// aborting a long-running task

document.querySelector('#calculate').addEventListener('click', async ({ target }) => { // 1
  target.innerText = 'Stop calculation';

  const result = await calculate(); // 2

  alert(result); // 3

  target.innerText = 'Calculate';
});

function calculate() {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      resolve(1);
    }, 5000);
  });
}