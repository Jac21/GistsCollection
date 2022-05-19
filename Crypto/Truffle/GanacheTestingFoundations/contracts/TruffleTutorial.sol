// SPDX-License-Identifier: MIT

pragma solidity >=0.4.22 <0.9.0;

contract TruffleTutorial {
    address public owner = msg.sender;
    string public message;

    // this function runs when the contract is deployed
    constructor() {
        // set initial message
        message = "Hello, Ganache!";
    }

    modifier ownerOnly() {
        require(
            msg.sender == owner,
            "This function is restricted to the contract's owner"
        );
        _;
    }

    function setMessage(string memory _message)
        public
        ownerOnly
        returns (string memory)
    {
        require(bytes(_message).length > 0);

        message = _message;
        return message;
    }
}
