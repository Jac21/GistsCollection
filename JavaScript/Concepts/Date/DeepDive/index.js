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