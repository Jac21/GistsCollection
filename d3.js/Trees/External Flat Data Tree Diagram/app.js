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

d3.json("treeData.json", function(error, treeData) {
  root = treeData[0];
  update(root);
});

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

  nodeEnter.append("circle")
          .attr("r", 10)
          .style("fill", "#fff");

  nodeEnter.append("text")
          .attr("x", function(d) {
            return d.children || d._children ? -13 : 13; 
          })
          .attr("dy", ".35em")
          .attr("text-anchor", function(d) {
            return d.children || d._children ? "end" : "start";
          })
          .text(function(d) { return d.name; })
          .style("fill-opacity", 1);

  // declare the links
  var link = svg.selectAll("path.link")
          .data(links, function(d) { return d.target.id; });

  // enter the links
  link.enter().insert("path", "g")
        .attr("class", "link")
        .attr("d", diagonal);
}
