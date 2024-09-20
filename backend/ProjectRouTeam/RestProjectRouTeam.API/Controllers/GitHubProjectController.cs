using Microsoft.AspNetCore.Mvc;
using RestProjectRouTeam.Core.Abstractions;
using RestProjectRouTeam.Core.Models;

namespace RestProjectRouTeam.API.Controllers
{
    [ApiController()]
    [Route("api")]
    public class GitHubProjectController : ControllerBase
    {
        private IGitHubService _service;

        public GitHubProjectController(IGitHubService s)
        {
            _service = s;
        }

        [Route("find")]
        [HttpGet]
        public async Task<ActionResult<List<GitHubSubject>>> Find(string subject)
        {
            var response = await _service.Search(subject);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<GitHubSubject>>> Get()
        {
            var response = await _service.Get();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<GitHubSubject>>> Get(int id)
        {
            var entity = await _service.Get(id);
            return Ok(entity);
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] GitHubSubject entity)
        {
            var requestId = await _service.Create(entity);
            return Ok(requestId);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody] GitHubSubject entity)
        {
            var requestId = await _service.Update(entity);
            return Ok(requestId);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var requestId = await _service.Delete(id);
            return Ok(requestId);
        }
    }
}
