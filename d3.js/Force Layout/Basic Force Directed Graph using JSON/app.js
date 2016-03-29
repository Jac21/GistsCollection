// set svg area
var width = 960,
    height = 500;

// declare colo(u)r range
color = d3.scale.category20c();

// use d3 force function
var force = d3.layout.force()
    .size([width, height])      // sets using svg area vars
    .linkDistance(30)           // sets target distance between nodes
    .charge(-120)               // sets the force between nodes

// set up the svg container
var svg = d3.select("body").append("svg")
    .attr("width", width)
    .attr("height", height);

// get the data
d3.json("force.json", function(error, graph) {
    if (error) throw error;

    var nodes = graph.nodes.slice(),
      links = [],
      bilinks = [];

    graph.links.forEach(function(link) {
    var s = nodes[link.source],
        t = nodes[link.target],
        i = {}; // intermediate node
    nodes.push(i);
    links.push({source: s, target: i}, {source: i, target: t});
    bilinks.push([s, i, t]);
    });

    force
      .nodes(nodes)
      .links(links)
      .start();

    var link = svg.selectAll(".link")
      .data(bilinks)
    .enter().append("path")
      .attr("class", "link");

    var node = svg.selectAll(".node")
      .data(graph.nodes)
    .enter().append("circle")
      .attr("class", "node")
      .attr("r", 5)
      .style("fill", function(d) { return color(d.group); })
      .call(force.drag);

    node.append("title")
      .text(function(d) { return d.name; });

    // add the text 
    node.append("text")
        .attr("x", 12)
        .attr("dy", ".35em")
        .text(function(d) { return d.name; });

    force.on("tick", function() {
        link.attr("d", function(d) {
          return "M" + d[0].x + "," + d[0].y
              + "S" + d[1].x + "," + d[1].y
              + " " + d[2].x + "," + d[2].y;
        });
        node.attr("transform", function(d) {
          return "translate(" + d.x + "," + d.y + ")";
        });

    });


});