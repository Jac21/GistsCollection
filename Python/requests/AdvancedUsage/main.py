from requests_toolbelt import sessions
import requests
from requests.adapters import HTTPAdapter
from requests.packages.urllib3.util.retry import Retry

DEFAULT_TIMEOUT = 5  # seconds

""" Request hooks """
response = requests.get('https://api.github.com/users/Jac21/repos?page=1')

# assert that there were no errors
response.raise_for_status()

# the above can get repetitive, create a custom requests object
http = requests.Session()

assert_status_hook = lambda response, * \
    args, **kwargs: response.raise_for_status()
http.hooks["response"] = [assert_status_hook]

http.get('https://api.github.com/users/nonexistent-user/repos?page=1')

# > HTTPError: 404 Client Error: Not Found for url

""" Setting base URLs """
httpWithBaseUrl = sessions.BaseUrlSession(base_url="https://api.github.comg")
httpWithBaseUrl.get("/users/Jac21")

""" Setting default timeouts """

# override constructor to provide a default timeout
# when constructing the http client and send() method


class TimeoutHTTPAdapter(HTTPAdapter):
    def __init__(self, *args, **kwargs):
        self.timeout = DEFAULT_TIMEOUT
        if "timeout" in kwargs:
            self.timeout = kwargs["timeout"]
            del kwargs["timeout"]
        super().__init__(*args, **kwargs)

    def send(self, request, **kwargs):
        timeout = kwargs.get("timeout")
        if timeout is None:
            kwargs["timeout"] = self.timeout
        return super().send(request, **kwargs)


httpWithTimeout = requests.Session()

adapter = TimeoutHTTPAdapter(timeout=2.5)
http.mount("https://", adapter)
http.mount("http://", adapter)

response = httpWithTimeout.get(
    'https://api.github.com/users/Jac21/repos?page=1')

""" Retry on failure """
retry_strategy = Retry(
    total=3,
    status_forcelist=[429, 500, 502, 503, 504],
    method_whitelist=["HEAD", "GET", "OPTIONS"],
    # {backoff factor} * (2 ** ({number of total retries} - 1))
    backoff_factor=1
)

adapter = HTTPAdapter(max_retries=retry_strategy)

httpWithRetry = requests.Session()

httpWithRetry.headers.update({
    "User-Agent": "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:68.0) Gecko/20100101 Firefox/68.0"
})  # Mimicking browser behaviors

http.mount("https://", TimeoutHTTPAdapter(max_retries=retry_strategy))
http.mount("http://", TimeoutHTTPAdapter(max_retries=retry_strategy))

response = httpWithRetry.get("https://en.wikipedia.org/w/api.php")

""" Debugging HTTP requests """
httpWithRetry.client.HTTPConnection.debuglevel = 1
httpWithRetry.get("https://www.google.com/")
