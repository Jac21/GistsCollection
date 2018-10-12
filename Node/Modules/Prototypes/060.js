// -> Let's create an object called 'machine'
var machine = {}

// -> Use Object.create to create another object called 'robot' with 'machine' 
//    set as the prototype
var robot = Object.create(machine);

// -> Use Object.create to create another object called 'robby' with 'robot' 
//    as the prototype
var robby = Object.create(robot);

// -> What is the result of `machine.isPrototypeOf(robby)`?
claim(machine.isPrototypeOf(robby), true);

// -> What is the result of `robot.isPrototypeOf(robby)`?
claim(robot.isPrototypeOf(robby), true);

// -> Which object is the direct prototype of robby?
claim.same(Object.getPrototypeOf(robby), robot);


// ------------------------------------------------
// Common JS exports for verification, don't modify
module.exports = {
	machine:  machine,
	robot:    robot,
	robby:    robby
}
