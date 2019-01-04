/* 
 * original code
 */
function renderList(list, ordered, color, bgColor) {
  // set defaults for optional parameters
  if (typeof ordered === undefined) ordered = true
  if (typeof color === undefined) color = '#1e2a2d'
  if (typeof bgColor === undefined) color = 'transparent'

  /* code to render list would go here 😉 */
};

// Calling the Function
// with all arguments
renderList(['one', 'two'], true, '#c0ffee', '#5ad');

// with only one "optional" argument (bgColor)
renderList(['one', 'two'], undefined, undefined, '#5ad');

// 👎

/* 
 * Naming Our Parameters with Destructuring
 */

// with named parames
var renderListNamedParams = function ({
  list,
  ordered,
  color,
  bgColor
}) {
  // some things
};

// call
renderListNamedParams({
  list: ['one, two'],
  ordered: true,
  color: '#c0ffee',
  bgColor: '#5ad'
});

/* 
 * Dealing with our new optional settings parameters
 */

var renderListWithoutNeededParameterNamed = function (list, {
  ordered,
  color,
  bgColor
} = {}) {
  // some things 
};

// call
renderListWithoutNeededParameterNamed(['one', 'two']);
renderListWithoutNeededParameterNamed(['one', 'two'], {
  ordered: true,
  color: '#c0ffee'
});


/* 
 * Setting defaults for our options 
 */

var renderListDefaultParams = function (list, {
  ordered = true,
  color = '#1e2a2d',
  bgColor = 'transparent'
} = {}) {
  // some things
};