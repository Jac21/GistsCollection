// Create a new Three.js scene
var scene = new THREE.Scene();

// Create a new camera to actually view the scene
/*
	// @Params
	// Integer - Field of View
	// Integer - Aspect Ratio (always want to use the width of the element divided by the height)
	// Integer - Near Clipping Plane - For performance, objects away from camera closer than 
	// value of near is not rendered
	// Integer - Far Clipping Plane - For performance, objects away from camera farther than 
	// value of far is not rendered
*/
var camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);

// Create a new WebGL Renderer, set size as width and height of the browser, add to DOM
var renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// Da shape, contains all points (vertices), and fill (faces)
// http://threejs.org/docs/index.html#Reference/Extras.Geometries/PolyhedronGeometry
var verticesOfCube = [
    -1,-1,-1,    1,-1,-1,    1, 1,-1,    -1, 1,-1,
    -1,-1, 1,    1,-1, 1,    1, 1, 1,    -1, 1, 1,
];

var indicesOfFaces = [
    2,1,0,    0,3,2,
    0,4,7,    7,3,0,
    0,1,5,    5,4,0,
    1,2,6,    6,5,1,
    2,3,7,    7,6,2,
    4,5,6,    6,7,4
];

var geometry = new THREE.PolyhedronGeometry( verticesOfCube, indicesOfFaces, 6, 2 );
var material = new THREE.MeshBasicMaterial({
	color: 0x00ff00,
	wireframe: true,
	vertexColors: THREE.VertexColors,
	morphTargets: true
});
var cube = new THREE.Mesh(geometry, material);
scene.add(cube);

/*
By default, when we call scene.add(), the thing we add will be added to the coordinates (0,0,0). 
This would cause both the camera and the cube to be inside each other. To avoid this, 
we simply move the camera out a bit.
*/
camera.position.z = 30;

// Render loop
function render() {
	requestAnimationFrame( render );

	// animate the cube
	cube.rotation.x += 0.01;
	cube.rotation.y += 0.01;

	renderer.render( scene, camera );
}
render();