using Microsoft.AspNetCore.Mvc;

using Esport.Logic;
using Esport.Data;

using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Esport.Endpoint.Services;

namespace Esport.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        IMatchLogic logic;
        IHubContext<SignalRHub> hub;
        public MatchController(IMatchLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Match> ReadAll()
        {
           return this.logic.GetAllMatches();
        }

        [HttpGet("{id}")]
        public Match Read(int id)
        {
            return this.logic.GetMatchById(id);
        }

        [HttpPost]
        public void Create([FromBody] Match value)
        {
            this.logic.InsertNewMatch(value);
            this.hub.Clients.All.SendAsync("MatchCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Match value)
        {
            this.logic.UpdateMatch(value);
            this.hub.Clients.All.SendAsync("MatchUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var matchToDelete = this.logic.GetMatchById(id);
            this.logic.RemoveMatch(id);
            this.hub.Clients.All.SendAsync("MatchDeleted", matchToDelete);
        }
    }
}
