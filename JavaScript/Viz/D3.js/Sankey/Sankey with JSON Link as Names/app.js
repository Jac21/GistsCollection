var units = "Widgets";

var margin = {top: 10, right: 10, bottom: 10, left: 10},
		width = 700 - margin.left - margin.right,
		height = 300 - margin.top - margin.bottom;

var formatNumber = d3.format(",.0f"), 	// zero decimal places
		format = function(d) { return formatNumber(d) + " " + units; },
		color = d3.scale.category20();

// append the svg canvas to the page
var svg = d3.select("#chart").append("svg")
			.attr("width", width + margin.left + margin.right)
			.attr("height", height + margin.top + margin.bottom)
		.append("g")
			.attr("transform",
						"translate(" + margin.left + "," + margin.top + ")");

// set the sankey diagram properties
var sankey = d3.sankey()
			.nodeWidth(36)
			.nodePadding(40)
			.size([width, height]);

var path = sankey.link();

// load the data
d3.json("sankey-formatted.json", function(error, graph) {

	// for json with names as links
	// create nodeMap object, for each of the graph.node instances,
	// assign each node name a number
	var nodeMap = {};
	graph.nodes.forEach(function(x) { nodeMap[x.name] = x; });
	// go through all the links, map appropriate number to correct name
	graph.links = graph.links.map(function(x) {
		return {
			source: nodeMap[x.source],
			target: nodeMap[x.target],
			value: x.value
		};
	});

	sankey
		.nodes(graph.nodes)
		.links(graph.links)
		.layout(32);

	// add in the links
	var link = svg.append("g").selectAll(".link")
				.data(graph.links)
			.enter().append("path")
				.attr("class", "link")
				.attr("d", path)
				.style("stroke-width", function(d) { return Math.max(1, d.dy); })
				.sort(function(a, b) { return b.dy - a.dy; });

	// add the link titles
	link.append("title")
				.text(function(d) {
					return d.source.name + " [] " +
					d.target.name + "\n" + format(d.value);
				});

	// add in the nodes
	var node = svg.append("g").selectAll(".node")
				.data(graph.nodes)
			.enter().append("g")
				.attr("class", "node")
				.attr("transform", function(d) {
					return "translate(" + d.x + "," + d.y + ")";
				})
			.call(d3.behavior.drag()
				.origin(function(d) {return d; })
				.on("dragstart", function() {
					this.parentNode.appendChild(this);
				})
				.on("drag", dragmove));

	// add the rectangles for the nodes
  node.append("rect")
      .attr("height", function(d) { return d.dy; })
      .attr("width", sankey.nodeWidth())
      .style("fill", function(d) { 
		  return d.color = color(d.name.replace(/ .*/, "")); })
      .style("stroke", function(d) { 
		  return d3.rgb(d.color).darker(2); })
    .append("title")
      .text(function(d) { 
		  return d.name + "\n" + format(d.value); });

// add in the title for the nodes
  node.append("text")
      .attr("x", -6)
      .attr("y", function(d) { return d.dy / 2; })
      .attr("dy", ".35em")
      .attr("text-anchor", "end")
      .attr("transform", null)
      .text(function(d) { return d.name; })
    .filter(function(d) { return d.x < width / 2; })
      .attr("x", 6 + sankey.nodeWidth())
      .attr("text-anchor", "start");

// the function for moving the nodes (can do x AND y!)
  function dragmove(d) {
    d3.select(this).attr("transform", 
        "translate(" + (
        	d.x = Math.max(0, Math.min(width - d.dx, d3.event.x))
        	)
        	+ "," + (
                d.y = Math.max(0, Math.min(height - d.dy, d3.event.y))
            ) + ")");
    sankey.relayout();
    link.attr("d", path);
  }
});