using Eventbus.Core.Domain.Entity;
using Eventbus.Core.Infrastructure.MesssageBroker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eventbus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {

        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<TopicController> _logger;
        private readonly Counter _addTopicCounter;

        public TopicController(IMessageBroker messageBroker, ILogger<TopicController> logger) {
            _messageBroker = messageBroker;
            _logger = logger;
            _addTopicCounter = Metrics.CreateCounter("topic_addtopic_count","add topic hits");
        }

        // GET: api/<TopicController>
        [HttpGet]
        public async IAsyncEnumerable<string> Get()
        {

            await Task.Delay(100);

            yield return "hola";

        }

        // GET api/<TopicController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TopicController>
        [HttpPost]
        public async Task Post([FromBody] Topic topic)
        {
            _addTopicCounter.Inc();
        }

        // PUT api/<TopicController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TopicController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
        }
    }
}
