const TruffleTutorial = artifacts.require("TruffleTutorial");

// > truffle migrate --reset
module.exports = function (deployer) {
  deployer.deploy(TruffleTutorial);
};

/*
> truffle console
truffle(development)> const truffleTutorial = await TruffleTutorial.deployed()
undefined
truffle(development)> const address = await truffleTutorial.address
undefined
truffle(development)> address
'0x46C00D73bF785000B3c3F93569E84415AB2381f2'

truffle(development)> const message = await truffleTutorial.message()
undefined
truffle(development)> message
'Hello, Ganache!'

truffle(development)> await truffleTutorial.setMessage('Hi there!')
truffle(development)> await truffleTutorial.message()
'Hi there!'
*/
