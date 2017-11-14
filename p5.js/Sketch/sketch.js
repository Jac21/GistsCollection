// browser-sync start --server --files "*"

const maxCircleSize = 20;
const numRows = 20;
const numCols = 20;
const numStrands = 2;
let phase = 0,
  speed = 0.03;

function setup() {
  createCanvas(750, 750);

  noStroke();
  colorA = color(253, 174, 120);
  colorB = color(226, 129, 161);
}

function draw() {
  background(4, 58, 74);

  phase = frameCount * speed;

  for (let strand = 0; strand < numStrands; strand += 1) {
    let strandPhase = phase + map(strand, 0, numStrands, 0, TWO_PI);

    for (let col = 0; col < numCols; col += 1) {
      let colOffset = map(col, 0, numCols, 0, TWO_PI);
      let x = map(col, 0, numCols, 50, width - 50);

      for (let row = 0; row < numRows; row += 1) {
        let y = height / 2 + row * 10 + sin(strandPhase + colOffset) * 50;
        let sizeOffset =
          (cos(strandPhase - row / numRows + colOffset) + 1) * 0.5;
        let circleSize = sizeOffset * maxCircleSize;

        fill(lerpColor(colorA, colorB, row / numRows));
        ellipse(x, y, circleSize, circleSize);
      }
    }
  }
}
