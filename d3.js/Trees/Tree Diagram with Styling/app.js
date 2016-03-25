var treeData = [
  {
    "name": "Top Level",
    "parent": "null",
    "value": 10,
    "type": "black",
    "level": "red",
    "icon": "js.png",
    "children": [
      {
        "name": "Level 2: A",
        "parent": "Top Level",
        "value": 10,
        "type": "grey",
        "level": "red",
        "icon": "js.png",
        "children": [
          {
            "name": "Son of A",
            "parent": "Level 2: A",
            "value": 5,
            "type": "steelblue",
            "level": "orange",
            "icon": "js.png"
          },
          {
            "name": "Daughter of A",
            "parent": "Level 2: A",
            "value": 8,
            "type": "steelblue",
            "level": "red",
            "icon": "js.png"
          }
        ]
      },
      {
        "name": "Level 2: B",
        "parent": "Top Level",
        "value": 10,
        "type": "grey",
        "level": "green",
        "icon": "js.png"
      }
    ]
  }
];

// **************** Generate the tree diagram *******************

// standard features of the diagram, size and shape of svg
var margin = {top: 20, right: 120, bottom: 20, left: 120},
    width = 960 - margin.right - margin.left,
    height = 500 - margin.top - margin.bottom;

var i = 0;

var tree = d3.layout.tree()
          .size([height, width]);

// for path curves
var diagonal = d3.svg.diagonal()
      .projection(function(d) { return [d.y, d.x]; });

// creates group elements and binds to page
var svg = d3.select("body").append("svg")
        .attr("width", width + margin.right + margin.left)
        .attr("height", height + margin.top + margin.bottom)
    .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

root = treeData[0];

update(root);

function update(source) {
  // compute the new tree layout
  var nodes = tree.nodes(root).reverse(),
      links = tree.links(nodes);

  // normalize for fixed-depth, expansion factor can change depth level
  nodes.forEach(function(d) {d.y = d.depth * 280; });

  // declare the nodes
  var node = svg.selectAll("g.node")
            .data(nodes, function(d) { return d.id || (d.id = ++i); });

  // enter the nodes
  var nodeEnter = node.enter().append("g")
            .attr("class", "node")
            .attr("transform", function(d) {
              return "translate(" + d.y + "," + d.x + ")";
            });

  // styling starts here (radius from value key, color from type,
    // fill color with level)
  nodeEnter.append("circle")
          .attr("r", function(d) {return d.value; })
          .style("stroke", function(d) { return d.type; })
          .style("fill", function(d) {return d.level; });

  nodeEnter.append("text")
          .attr("x", function(d) {
            return d.children || d._children ?
            (d.value + 4) * -1 : d.value + 4
          })
          .attr("dy", ".35em")
          .attr("text-anchor", function(d) {
            return d.children || d._children ? "end" : "start";
          })
          .text(function(d) { return d.name; })
          .style("fill-opacity", 1);

  nodeEnter.append("image")
    .attr("xlink:href", function(d) { return d.icon; })
    .attr("x", "-12px")
    .attr("y", "-12px")
    .attr("width", "24px")
    .attr("height", "24px");

  // declare the links
  var link = svg.selectAll("path.link")
          .data(links, function(d) { return d.target.id; });

  // enter the links
  link.enter().insert("path", "g")
        .attr("class", "link")
        .style("stroke", function(d) {return d.target.level; })
        .attr("d", diagonal);
}
