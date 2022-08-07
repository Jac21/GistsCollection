// migrations/2_deploy_contract.js
// SPDX-License-Identifier: MIT
const SendingAndReceiving = artifacts.require("SendingAndReceiving");

module.exports = function (deployer) {
  deployer.deploy(SendingAndReceiving);
};
