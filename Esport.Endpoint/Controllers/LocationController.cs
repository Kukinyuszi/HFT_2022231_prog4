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
    public class LocationController : ControllerBase
    {
        ILocationLogic logic;
        IHubContext<SignalRHub> hub;
        public LocationController(ILocationLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Location> ReadAll()
        {
            return this.logic.GetAllLocations();
        }

        [HttpGet("{id}")]
        public Location Read(int id)
        {
            return this.logic.GetLocationById(id);
        }

        [HttpPost]
        public void Create([FromBody] Location value)
        {
            this.logic.InsertNewLocation(value);
            this.hub.Clients.All.SendAsync("LocationCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Location value)
        {
            this.logic.ChangeLocation(value);
            this.hub.Clients.All.SendAsync("LocationUpdated", value);
        }

        [HttpDelete("{id}")]
        public void DeleteLocation(int id)
        {
            var locationToDelete = this.logic.GetLocationById(id);
            this.logic.RemoveLocation(id);
            this.hub.Clients.All.SendAsync("LocationDeleted", locationToDelete);
        }
    }
}
