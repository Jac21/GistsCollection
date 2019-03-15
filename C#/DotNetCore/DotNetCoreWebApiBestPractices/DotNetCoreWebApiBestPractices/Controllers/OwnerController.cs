using System;
using System.Linq;
using DotNetCoreWebApiBestPractices.Contexts;
using DotNetCoreWebApiBestPractices.Factories;
using DotNetCoreWebApiBestPractices.Models;
using DotNetCoreWebApiBestPractices.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly OwnerContext context;

        private const string EtagHeader = "ETag";
        private const string MatchHeader = "If-Match";

        public OwnerController(ILogger<OwnerController> logger, IRepository repository, OwnerContext context)
        {
            this.logger = logger;
            this.repository = repository;
            this.context = context;
        }

        [ResponseCache(Duration = 1800)]
        [HttpGet]
        public IActionResult GetAllOwners()
        {
            return Ok();
        }

        [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetOwnerById(Guid id)
        {
            var item = context.Owners.FirstOrDefaultAsync(t => t.Id == id).Result;

            if (item == null)
                return NotFound();

            // Controller always returns the hash in the ETag header value
            var eTag = HashFactory.GetHash(item);
            HttpContext.Request.Headers.Add(EtagHeader, eTag);

            // When the client sends an existing value using the If-Match header and the hash code hasn’t changed, 
            // the controller returns the “not modified” response code with no body.
            if (HttpContext.Request.Headers.ContainsKey(MatchHeader) &&
                HttpContext.Request.Headers[MatchHeader].Contains(eTag))
            {
                return new StatusCodeResult(304);
            }

            return new ObjectResult(item);
        }

        [HttpGet("{id}/account")]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            return Ok();
        }

        /// <summary>
        /// Our actions should always be clean and simple. 
        /// Their responsibilities include handling HTTP requests, validating models, catching errors and returning responses:
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

        [HttpPut("{id}", Name = "PutOwner")]
        public IActionResult UpdateOwner(Guid id, [FromBody] Owner owner)
        {
            if (owner == null || owner.Id != id)
            {
                return BadRequest();
            }

            var item = context.Owners.FirstOrDefaultAsync(t => t.Id == id).Result;

            if (item == null)
                return NotFound();

            // the update operation expects an If-Match header to exist. If the header doesn’t exist, 
            // or if it doesn’t match the current hash code, it returns the error status.
            var dbTag = HashFactory.GetHash(owner);

            if (!HttpContext.Request.Headers.ContainsKey(MatchHeader) ||
                !HttpContext.Request.Headers[MatchHeader].Contains(dbTag))
            {
                return new StatusCodeResult(412);
            }

            owner.IsActive = item.IsActive;
            owner.Name = item.Name;

            context.Owners.Update(owner);
            context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            return Ok();
        }
    }
}