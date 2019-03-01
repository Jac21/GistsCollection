using BasicApiThrottler.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BasicApiThrottler.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // Basic usage for throttling requests:
        // GET api/values
        [ThrottleFilter]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello, Throttler, basic");
        }

        // Allow more requests through, say 50 every 5 seconds:
        // GET api/values
        [ThrottleFilter(50, 5)]
        [HttpGet]
        [Route("~/allow-more")]
        public IActionResult GetAllowMore()
        {
            return Ok("Hello, Throttler, allow-more");
        }

        // Throttling based on IP address:
        [ThrottleFilter(throttleGroup: @"ipaddress")]
        [HttpGet]
        [Route("~/name-ip")]
        public IActionResult GetNameThrottleByIpAddress(int id)
        {
            return Ok("Hello, Throttler, throttle by IP");
        }

        // Throttling based on Identity:
        [ThrottleFilter(throttleGroup: @"identity")]
        [HttpGet]
        [Route("~/name-identity")]
        public IActionResult GetNameThrottleByIdentity(int id)
        {
            return Ok("Hello, Throttler, throttle by Identity");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // Throttling a group of requests together:
        // POST api/values
        [ThrottleFilter(throttleGroup: @"updates")]
        [HttpPost]
        [Route("~/name")]
        public IActionResult PostUpdatesName([FromBody] string value)
        {
            return Ok("Hello Throttler, updated name");
        }

        // POST api/values
        [ThrottleFilter(throttleGroup: @"updates")]
        [HttpPost]
        [Route("~/address")]
        public IActionResult PostUpdatesAddress([FromBody] string value)
        {
            return Ok("Hello Throttler, updated address");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}