'use strict';

var triadRows = document.querySelectorAll('.layout-item.triad');

var rgbToHslObject = rgbToHsl(212, 106, 106);
var hslObject = hslToObject(rgbToHslObject[0], rgbToHslObject[1], rgbToHslObject[2]);

var triad = getTriadic(hslObject);

console.log(triad);

triadRows[0].style.backgroundColor =
    `hsl(${triad[0].hue}, ${triad[0].saturation}%, ${triad[0].lightness}%)`;
triadRows[1].style.backgroundColor =
    `hsl(${triad[1].hue}, ${triad[1].saturation}%, ${triad[1].lightness}%)`;
