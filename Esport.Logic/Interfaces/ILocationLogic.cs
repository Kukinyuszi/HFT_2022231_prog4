namespace Esport.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    using Esport.Data;
    using Esport.Repository;
    public interface ILocationLogic
    {
        void RemoveLocation(int id);

        void ChangeLocation(Location item);

        void InsertNewLocation(Location item);

        IQueryable<Location> GetAllLocations();

        Location GetLocationById(int id);
    }
}
