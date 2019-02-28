using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestfulJobPattern.Data.Documents;
using RestfulJobPattern.Data.Repositories;
using RestfulJobPattern.Models;
using RestfulJobPattern.Services;

namespace RestfulJobPattern.Controllers
{
    [Route("star")]
    [ApiController]
    public class StarsController : ControllerBase
    {
        private readonly StarRepository starRepository;
        private readonly IMapper mapper;
        private readonly JobService jobService;

        public StarsController(StarRepository starRepository, IMapper mapper, JobService jobService)
        {
            this.starRepository = starRepository;
            this.mapper = mapper;
            this.jobService = jobService;
        }

        // GET star
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StarDto>>> Get()
        {
            var stars = await starRepository.GetAllStarsAsync();

            return stars.Select(p => mapper.Map<StarDto>(p)).ToList();
        }

        // GET star/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StarDto>> Get(long id)
        {
            var result = await starRepository.GetStarAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return mapper.Map<StarDto>(result);
        }

        // POST star
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StarDto star)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            star.Id = 0;
            var job = await jobService.CreateStarJobAsync(mapper.Map<Star>(star));

            return AcceptedAtAction("Get", "Jobs", new {id = job.Id});
        }

        // DELETE star/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await starRepository.DeleteStarAsync(id);

            return NoContent();
        }
    }
}