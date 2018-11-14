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
  canvas.width = window.innerWidth;
  canvas.height = window.innerHeight;

  init();
}));

const circlesCount = 800;

const colorArray = ['#f9ed69', '#f08a5d', '#b83b5e', '#6a2c70'];

const init = () => {
  for (let i = 0; i < circlesCount; i++) {
    const radius = Math.random() * 20 + 1
    const x = Math.random() * (innerWidth - radius  * 2) + radius
    const y = Math.random() * (innerHeight - radius  * 2) + radius
    const dx = (Math.random() - 0.5) * 2
    const dy = (Math.random() - 0.5) * 2

    const circle = new Circle(x, y, dx, dy, radius)
    circle.draw()
  }
};

const Circle = function (x, y, dx, dy, radius) {
  this.x = x;
  this.y = y;
  this.dx = dx;
  this.dy = dy;
  this.radius = radius;
  this.minRadius = radius;
  this.color = colorArray[Math.floor(Math.random() * colorArray.length)];

  this.draw = function () {
    c.beginPath();
    c.arc(this.x, this.y, this.radius, 0, Math.PI * 2, false);
    c.strokeStyle = 'black';
    c.stroke();
    c.fillStyle = this.color;
    c.fill();
  };
};

init();