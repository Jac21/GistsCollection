'use strict';

// Update attributes
const rotateHue = rotation => ({
    hue,
    ...rest
}) => {
    const modulo = (x, n) => (x % n + n) % n;
    const newHue = modulo(hue + rotation, 360);
    return {
        ...rest,
        hue: newHue
    };
}

const rotate30 = rotateHue(30);
const getComplementary = rotateHue(180);

const getTriadic = color => {
    const first = rotateHue(120);
    const second = rotateHue(-120);
    return [first(color), second(color)];
}

const saturate = x => ({
    saturation,
    ...rest
}) => ({
    ...rest,
    saturation: Math.min(1, saturation + x),
});
const desaturate = x => ({
    saturation,
    ...rest
}) => ({
    ...rest,
    saturation: Math.max(0, saturation - x),
});
const lighten = x => ({
    lightness,
    ...rest
}) => ({
    ...rest,
    lightness: Math.min(1, lightness + x)
});
const darken = x => ({
    lightness,
    ...rest
}) => ({
    ...rest,
    lightness: Math.max(0, lightness - x)
});

// Color predicates
const isGrayscale = ({
    saturation
}) => saturation === 0;

const isDark = ({
    lightness
}) => lightness < .5;