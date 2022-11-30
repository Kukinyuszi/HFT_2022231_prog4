using Esport.Data;
using Esport.Repository.Database;
using Esport.Repository.GenericRespository;
using Esport.Repository.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esport.Repository.ModelRepositories
{
    public class LocationRepository : MainRepository<Location>, IRepository<Location>
    {
        public LocationRepository(DataContext ctx)
            : base(ctx)
        {
        }

        public override void Update(Location item)
        {
            var old = GetOne(item.ID);
            old.Name = item.Name;
            old.Capacity = item.Capacity;
            old.ID = item.ID;
            old.Match = item.Match;
            old.MatchID = item.MatchID;
            ctx.SaveChanges();
        }

        public override Location GetOne(int id)
        {
            return ctx.Location.FirstOrDefault(t => t.ID == id);
        }
    }
}
