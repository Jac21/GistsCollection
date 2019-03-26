'use strict';

// Calculating lightness from RGB
const rgbToLightness = (r, g, b) =>
  1 / 2 * (Math.max(r, g, b) + Math.min(r, g, b));

// Calculating saturation from RGB
const rgbToSaturation = (r, g, b) => {
  const L = rgbToLightness(r, g, b);
  const max = Math.max(r, g, b);
  const min = Math.min(r, g, b);
  return (L === 0 || L === 1) ?
    0 :
    (max - min) / (1 - Math.abs(2 * L - 1));
};

// Calculating hue from RGB
const rgbToHue = (r, g, b) => Math.round(
  Math.atan2(
    Math.sqrt(3) * (g - b),
    2 * r - g - b,
  ) * 180 / Math.PI
);

// Calculating HSL
const rgbToHsl = (r, g, b) => {
  const lightness = rgbToLightness(r, g, b);
  const saturation = rgbToSaturation(r, g, b);
  const hue = rgbToHue(r, g, b);
  return [hue, saturation, lightness];
}

// Calculating RGB from HSL
const hslToRgb = (h, s, l) => {
  const C = (1 - Math.abs(2 * l - 1)) * s;
  const hPrime = h / 60;
  const X = C * (1 - Math.abs(hPrime % 2 - 1));
  const m = l - C / 2;

  const withLight = (r, g, b) => [r + m, g + m, b + m];
  if (hPrime <= 1) {
    return withLight(C, X, 0);
  } else
  if (hPrime <= 2) {
    return withLight(X, C, 0);
  } else
  if (hPrime <= 3) {
    return withLight(0, C, X);
  } else
  if (hPrime <= 4) {
    return withLight(0, X, C);
  } else
  if (hPrime <= 5) {
    return withLight(X, 0, C);
  } else
  if (hPrime <= 6) {
    return withLight(C, 0, X);
  }
};

// Creating a color object
const rgbToObject = (red, green, blue) => {
  const [hue, saturation, lightness] = rgbToHsl(red, green, blue);
  return {
    red,
    green,
    blue,
    hue,
    saturation,
    lightness
  };
}
const hslToObject = (hue, saturation, lightness) => {
  const [red, green, blue] = hslToRgb(hue, saturation, lightness);
  return {
    red,
    green,
    blue,
    hue,
    saturation,
    lightness
  };
}

/* Dealing with color arrays */

// Filters
const colors = [ /* ... an array of color objects ... */ ];
const isLight = ({
  lightness
}) => lightness > .5;
const lightColors = colors.filter(isLight);

// Sorting
const compareLightness = (a, b) => a.lightness - b.lightness;
const compareSaturation = (a, b) => a.saturation - b.saturation;

const compareAttribute = attribute =>
  (a, b) => a[attribute] - b[attribute];
const compareLightness = compareAttribute('lightness');
const compareSaturation = compareAttribute('saturation');
const compareHue = compareAttribute('hue');

// Averaging attributes
const colors = [ /* ... an array of color objects ... */ ];
const toSum = (a, b) => a + b;
const toAttribute = attribute => element => element[attribute];
const averageOfAttribute = attribute => array =>
  array.map(toAttribute(attribute)).reduce(toSum) / array.length;

/* ... continuing */
const normalizeAttribute = attribute => array => {
  const averageValue = averageOfAttribute(attribute)(array);
  const normalize = overwriteAttribute(attribute)(averageValue);
  return normalize(array);
}
const normalizeSaturation = normalizeAttribute('saturation');
const normalizeLightness = normalizeAttribute('lightness');
const normalizeHue = normalizeAttribute('hue');