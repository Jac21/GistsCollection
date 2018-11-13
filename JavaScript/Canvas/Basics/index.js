'use strict';

// grab canvas from DOM
const canvas = document.querySelector('canvas');

// to fill the entire page...
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

// get canvas context in a 2d fashion
const c = canvas.getContext('2d');

// prevent canvas resizing event firing off too much
const debounce = (func) => {
  let timer;
  return (event) => {
    if (timer) {
      clearTimeout(timer);
    }
    timer = setTimeout(func, 100, event);
  };
};

window.addEventListener('resize', debounce(() => {
  canvas.width = window.innerWidth
  canvas.height = window.innerHeight

  init();
}));

const init = () => {
  // // draw a rectangle
  // c.fillStyle = 'white';
  // c.fillRect(100, 100, 100, 100);

  // // get creative
  // for (let i = 0; i < 60; i++) {
  //   for (let j = 0; j < 60; j++) {
  //     c.fillStyle = `rgb(${i * 5}, ${j * 5}, ${(i+j) * 50})`
  //     c.fillRect(j * 20, i * 20, 10, 10)
  //   }
  // }

  // for (let i = 0; i < 60; i++) {
  //   for (let j = 0; j < 60; j++) {
  //     c.fillStyle = `rgb(${i * 5}, ${j * 5}, ${(i+j) * 50})`
  //     c.fillRect(j * 20, i * 20, 20, 20)
  //   }
  // }

  for (let i = 0; i < 61; i++) {
    for (let j = 0; j < 61; j++) {
      c.strokeStyle = `rgb(${i * 5}, ${j * 5}, ${(i + j) * 50})`
      c.strokeRect(j * 20, i * 20, 20, 20)
    }
  }

  // text
  c.font = '148px Courier New';
  c.fillText('AMAZING', 100, 100);
};

init();