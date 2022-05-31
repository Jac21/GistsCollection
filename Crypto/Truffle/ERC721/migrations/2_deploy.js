// migrations/2_deploy.js
// SPDX-License-Identifier: MIT
const Gists = artifacts.require("Gists");

module.exports = function (deployer) {
  deployer.deploy(Gists);
};

/*
truffle(development)> const gists = await Gists.deployed()
undefined
truffle(development)> const address = await gists.address
undefined
truffle(development)> address
'0xfA0AbCb50B223BbF07b0E02897034F2F50525906'
truffle(development)> await gists.name()
'Gists'
truffle(development)> await gists.symbol()
'GIST'
truffle(development)> await gists.safeMint("0xf44c28612Aa3d67A3b89BfB1cf2FaFb2cD697e85", "QmXvQd5sCKHDpAn52yyiQdqqrGj4dqGFrSDSdSgiDmdjoU")
truffle(development)> await gists.ownerOf(0)
'0xf44c28612Aa3d67A3b89BfB1cf2FaFb2cD697e85'
truffle(development)> await gists.tokenURI(0)
'https://ipfs.infura.io/ipfs/QmXvQd5sCKHDpAn52yyiQdqqrGj4dqGFrSDSdSgiDmdjoU'
*/
