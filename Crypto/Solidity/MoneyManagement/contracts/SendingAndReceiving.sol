// SPDX-License-Identifier: MIT
pragma solidity ^0.8.13;

contract SendingAndReceiving {
    // owner of this contract
    address public contractOwner;

    constructor() {
        contractOwner = msg.sender;
    }

    function sendMoneyToContract() public payable {}

    function getBalance() public view returns (uint256) {
        return address(this).balance;
    }

    function withdrawAll(address payable _to) public {
        require(contractOwner == _to);
        _to.transfer(address(this).balance);
    }
}
