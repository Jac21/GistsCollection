// set screen values
var margin = {top: 5, right: 40, bottom: 20, left: 120},
    width = 800 - margin.right - margin.left,
    height = 50 - margin.top - margin.bottom;

// set bullet chart from bullet.js
var chart = d3.bullet()
      .width(width)
      .height(height);

// load JSON data
d3.json("bullet-data.json", function(error, data) {

  // draw the char
  var svg = d3.select("body").selectAll("svg")  // select where chart will go
      .data(data) // load data
    .enter().append("svg")  // append svg
      .attr("class", "bullet")  // load style
      .attr("width", width + margin.left + margin.right)
      .attr("height", height + margin.top + margin.bottom)
    .append("g")
      .attr("transform", "translate(" + margin.left + "," + margin.top + ")")
      .call(chart); // magic

  var title = svg.append("g")
        .style("text-anchor", "end")
        .attr("transform", "translate(-6," + height / 2 + ")");

  title.append("text")
      .attr("class", "title")
      .text(function(d) { return d.title; });

  title.append("text")
      .attr("class", "subtitle")
      .attr("dy", "1em")
      .text(function(d) { return d.subtitle; });

  d3.selectAll("button").on("click", function() {
    svg.datum(randomize).call(chart.duration(1000));
  });
});

function randomize(d) {
  if (!d.randomizer) d.randomizer = randomizer(d);
  d.markers = d.markers.map(d.randomizer);
  d.measures = d.measures.map(d.randomizer);
  return d;
}

function randomizer(d) {
  var k = d3.max(d.ranges) * .2;
  return function(d) {
    return Math.max(0, d + k * (Math.random() - .5));
  };
}