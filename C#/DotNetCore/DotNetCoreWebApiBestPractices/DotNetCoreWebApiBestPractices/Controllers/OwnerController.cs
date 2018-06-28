using System;
using DotNetCoreWebApiBestPractices.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWebApiBestPractices.Controllers
{
    /// <summary>
    /// The controllers should always be as clean as possible. We shouldn’t place any business logic inside it.
    /// So, our controllers should be responsible for accepting the service instances through the constructor injection and for organizing HTTP action methods(GET, POST, PUT, DELETE, PATCH…):
    /// </summary>
    [Route("api/owner")]
    public class OwnerController : Controller
    {
        /// <summary>
        /// Logging
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Data Access Layer
        /// </summary>
        private readonly IRepository repository;

        public OwnerController(ILogger<OwnerController> logger, IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllOwners()
        {
            return Ok();
        }

        [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetOwnerById(Guid id)
        {
            return Ok();
        }

        [HttpGet("{id}/account")]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            return Ok();
        }

        /// <summary>
        /// Our actions should always be clean and simple. Their responsibilities include handling HTTP requests, validating models, catching errors and returning responses:
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateOwner([FromBody] Owner owner)
        {
            try
            {
                if (owner.IsObjectNull())
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                repository.Owner.CreateOwner(owner);

                return CreatedAtRoute("OwnerById", new {id = owner.Id}, owner);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside the CreateOwner action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody] Owner owner)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            return Ok();
        }
    }

    public class Owner
    {
        public object Id;

        public bool IsObjectNull()
        {
            throw new NotImplementedException();
        }

        public void CreateOwner(Owner owner)
        {
            throw new NotImplementedException();
        }
    }
}