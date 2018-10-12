const ora = require("ora");
const getWeather = require("../utils/weather");
const getLocation = require("../utils/location");

module.exports = async args => {
  const spinner = ora().start();

  try {
    const location = args.location || args.l || (await getLocation());
    const weather = await getWeather(location);

    console.log(
      `Current conditions in ${location} as of ${weather.condition.date}:`
    );
    console.log(`\t${weather.condition.temp}° ${weather.condition.text}`);
  } catch (err) {
    console.error(err);
  } finally {
    spinner.stop();
  }
};
