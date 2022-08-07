<template>
  <div>
    <!-- "connect" click event is registered -->
    <button v-if="!connected" @click="connect">Connect wallet</button>
    <!-- call-contract button is visible if the wallet is connected -->
    <button v-if="connected" @click="callContract">Call contract</button>
    {{ contractResult }}
  </div>
</template>

<script>
import Web3 from "web3";
export default {
  name: "App",

  data() {
    return {
      connected: false,
      contractResult: "",
    };
  },
  methods: {
    connect: function () {
      // this connects to the wallet
      if (window.ethereum) {
        // first we check if metamask is installed
        window.ethereum.request({ method: "eth_requestAccounts" }).then(() => {
          this.connected = true; // If users successfully connected their wallet
        });
      }
    },
    callContract: function () {
      // method for calling the contract method
      let web3 = new Web3(window.ethereum);
      let contractAddress = "0xC0B2D76aB95B7E31E241ce713ea1C72d0a50588e";

      let abi = JSON.parse(
        `[{"inputs": [],"stateMutability": "nonpayable","type": "constructor"},{"inputs": [],"name": "greet","outputs": [{"internalType": "string","name": "","type": "string"}],"stateMutability": "view","type": "function"}]`
      );

      let contract = new web3.eth.Contract(abi, contractAddress);

      contract.methods
        .greet()
        .call()
        .then((result) => (this.contractResult = result));
    },
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
