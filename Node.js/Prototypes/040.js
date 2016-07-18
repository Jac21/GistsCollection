// -> Define three objects: 'machine', 'robot' and 'vehicle'
//    In the definition of machine add a property 'motors' set to null.
var machine = {
	motors: null
};

var robot = {};

var vehicle = {};

// -> Let's make machine the prototype of robot and vehicle
vehicle.__proto__ = machine;
robot.__proto__ = machine;

// -> What are `machine.motors`, `robot.motors` and `vehicle.motors`?
claim(machine.motors, null);
claim(robot.motors, null);
claim(vehicle.motors, null);

// -> Set `robot.motors` to 4 by direct assignment
robot.motors = 4;

// -> What are `machine.motors`, `robot.motors` and `vehicle.motors` now?
claim(machine.motors, null);
claim(robot.motors, 4);
claim(vehicle.motors, null);


// ------------------------------------------------
// Common JS exports for verification, don't modify
module.exports = {
	machine:  machine,
	vehicle:  vehicle,
	robot:    robot
}
