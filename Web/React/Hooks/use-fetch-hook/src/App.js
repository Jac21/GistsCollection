import React from 'react';
import './App.css';

// Custom useFetch hook implementation
const useFetch = (url, options) => {
  const [response, setResponse] = React.useState(null);
  const [error, setError] = React.useState(null);
  const [isLoading, setIsLoading] = React.useState(false);

  React.useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);

      try {
        const res = await fetch(url, options);
        const json = await res.json();
        setResponse(json);

        setIsLoading(false);
      } catch (error) {
        setError(error);
      }
    };
    fetchData();
  }, []); // avoiding reference loops

  return { response, error, isLoading };
};

function App() {
  // useFetch usage
  const res = useFetch("https://api.github.com/users/Jac21", {});

  if (!res.response) {
    return <div>Loading...</div>
  }

  const userPhoto = res.response.avatar_url;

  return (
    <div className="App">
      <header className="App-header">
        <img src={userPhoto} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
