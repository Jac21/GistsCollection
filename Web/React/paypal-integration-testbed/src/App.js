import logo from "./logo.svg";
import "./App.css";
import Checkout from "./Checkout";
import { PayPalScriptProvider } from "@paypal/react-paypal-js";

const initialOptions = {
  "client-id":
    "AfHR6b8qvEzzppbQ8pl4JkaMrZnMGJYlgimtJE-fG15tOM1vDS8c_ZZRfvNDfx6suCbIkHHoVggef1xs",
  currency: "USD",
  intent: "capture",
};

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <PayPalScriptProvider options={initialOptions}>
          <Checkout />
        </PayPalScriptProvider>
      </header>
    </div>
  );
}

export default App;
