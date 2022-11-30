using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using Esport.Endpoint.Services;
using Esport.Logic;
using Esport.Data;

using System.Collections.Generic;
using Esport.Logic.Interfaces;

namespace Esport.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        ITeamLogic logic;
        IHubContext<SignalRHub> hub;
        public TeamController(ITeamLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Team> ReadAll()
        {
            return this.logic.GetAllTeams();
        }

        [HttpGet("{id}")]
        public Team Read(int id)
        {
            return this.logic.GetTeamById(id);
        }

        [HttpPost]
        public void Create([FromBody] Team value)
        {
            this.logic.InsertNewTeam(value);
            this.hub.Clients.All.SendAsync("TeamCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Team value)
        {
            this.logic.ChangeTeamName(value);
            this.hub.Clients.All.SendAsync("TeamUpdated", value);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var teamToDelete = logic.GetTeamById(id);
            this.logic.RemoveTeam(id);
            this.hub.Clients.All.SendAsync("TeamDeleted", teamToDelete);
        }
    }
}
