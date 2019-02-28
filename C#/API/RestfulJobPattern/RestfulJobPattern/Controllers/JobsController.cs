using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestfulJobPattern.Data.Repositories;
using RestfulJobPattern.Models;

namespace RestfulJobPattern.Controllers
{
    [Route("job")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly JobRepository jobRepository;
        private readonly IMapper mapper;

        public JobsController(JobRepository jobRepository, IMapper mapper)
        {
            this.jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET job
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDto>>> Get()
        {
            var jobs = await jobRepository.GetAllJobsAsync();

            return jobs.Select(p => mapper.Map<JobDto>(p)).ToList();
        }

        // GET job/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDto>> Get(long id)
        {
            var result = await jobRepository.GetJobAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return mapper.Map<JobDto>(result);
        }
    }
}