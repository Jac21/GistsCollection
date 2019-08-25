"use strict";

const logger = function (objectToBeLogged, logType) {
  switch (logType) {
    case 'log':
      console.log(objectToBeLogged);
      break;
    case 'warn':
      console.warn(objectToBeLogged);
      break;
    case 'error':
      console.error(objectToBeLogged);
      break;
    default:
      console.log(objectToBeLogged);
      break;
  }
};

// ISO 8601 Extended format
const iso8601 = `YYYY-MM-DDTHH:mm:ss:sssZ`

// # Creating a date

// The date-string method
let stringDate = new Date('1993-05-21');
logger(stringDate, 'warn'); // date created will be in UTC time

let stringDateLocal = new Date('1993-05-21T00:00');
logger(stringDateLocal);

// Creating dates with arguments
var argumentDate = new Date(1993, 4, 21, 6, 21, 30);
logger(argumentDate);

// Creating dates with timestamps
var timestampDate = new Date(1560211200000); // 11th June 2019, 8am (in Local Time, Singapore)
logger(timestampDate);

// With no arguments
var noArgumentDate = new Date();
logger(noArgumentDate, 'warn');

// # Formatting a date

const date = new Date(2019, 0, 23, 17, 23, 42)
logger(date.toString());

// Writing a custom date format
const d = new Date(2019, 0, 23);
const year = d.getFullYear(); // 2019
const newDate = d.getDate(); // 23

const months = [
  'January',
  'February',
  'March',
  'April',
  'May',
  'June',
  'July',
  'August',
  'September',
  'October',
  'November',
  'December'
];

const monthName = months[d.getMonth()];
logger(monthName);

const days = [
  'Sun',
  'Mon',
  'Tue',
  'Wed',
  'Thu',
  'Fri',
  'Sat'
]

const dayName = days[d.getDay()] // Thu

const formatted = `${dayName}, ${newDate} ${monthName} ${year}`
logger(formatted) // Thu, 23 January 2019

// # Comparing dates

const earlier = new Date(2019, 0, 26);
const later = new Date(2019, 0, 27);

logger(earlier < later); // true

const isSameTime = (a, b) => {
  return a.getTime() === b.getTime()
};

const a = new Date(2019, 0, 26);
const b = new Date(2019, 0, 26);

logger(isSameTime(a, b)); // true

// # Automatic date correction

// 33rd March => 2nd April
new Date(2019, 2, 33)