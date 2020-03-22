"use-strict";

// # tells the browser to run a callback function right 
// before the next repaint happens.

var loggingAction = window.requestAnimationFrame(function () {
  console.log("Ran!");
});

// cancel the above
window.cancelAnimationFrame(loggingAction);

// # looping animations via recursion

var counter = document.querySelector('#counter');
var number = 0;

var countUp = function () {
  // increase number by 1
  number += 1;

  // update the UI
  counter.textContent = number;

  // if the number is less than or equal to 100,00, run it again
  if (number <= 100000) {
    window.requestAnimationFrame(countUp);
  }
};

// start the animation
window.requestAnimationFrame(countUp);