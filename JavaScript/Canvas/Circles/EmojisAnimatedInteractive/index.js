'use strict';

// grab canvas from DOM
const canvas = document.querySelector('canvas');

// to fill the entire page...
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

// initialize mouse coordinate variables
let mouseX = undefined;
let mouseY = undefined;

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

const circlesCount = 100;
const maxRadius = 140;
const circleArray = [];

const emojiArray = [
  '💩',
]

const init = () => {
  circleArray.length = 0;
  for (let i = 0; i < circlesCount; i++) {
    const radius = Math.random() * 20 + 1;

    const x = Math.random() * (innerWidth - radius * 2) + radius;
    const y = Math.random() * (innerHeight - radius * 2) + radius;

    const dx = (Math.random() - 0.5) * 2;
    const dy = (Math.random() - 0.5) * 2;

    circleArray.push(new Circle(x, y, dx, dy, radius));
  }
};

const Circle = function (x, y, dx, dy, radius) {
  this.x = x;
  this.y = y;

  this.dx = dx;
  this.dy = dy;

  this.radius = radius;
  this.minRadius = radius;
  this.text = emojiArray[Math.floor(Math.random() * emojiArray.length)];

  this.draw = function () {
    c.font = `${this.radius}px Courier New`;
    c.fillText(this.text, this.x, this.y);
  };

  this.update = function () {
    if (this.x + this.radius > innerWidth || this.x - this.radius < 0) {
      this.dx = -this.dx;
    }

    if (this.y + this.radius > innerHeight || this.y - this.radius < 0) {
      this.dy = -this.dy;
    }

    this.x += this.dx;
    this.y += this.dy;

    //interactivity
    if (mouseX - this.x < 50 &&
      mouseX - this.x > -50 &&
      mouseY - this.y < 50 &&
      mouseY - this.y > -50) {
      if (this.radius < maxRadius) {
        this.radius += 1;
        c.font = `${this.radius}px Courier New`;
      }
    } else {
      if (this.radius > this.minRadius) {
        this.radius -= 1;
        c.font = `${this.radius}px Courier New`;
      }
    }
    //end interactivity

    this.draw();
  }
};

const animate = () => {
  requestAnimationFrame(animate);
  c.clearRect(0, 0, innerWidth, innerHeight);

  for (let i = 0; i < circleArray.length; i++) {
    circleArray[i].update();
  }
};

init();
animate();

window.addEventListener('mousemove', (e) => {
  mouseX = e.x;
  mouseY = e.y;
});