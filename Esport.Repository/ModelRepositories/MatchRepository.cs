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
    public class MatchRepository : MainRepository<Match>, IRepository<Match>
    {
        public MatchRepository(DataContext ctx)
            : base(ctx)
        {
        }

        public override void Update(Match item)
        {

            var old = GetOne(item.ID);
            old.Name = item.Name;
            old.ID = item.ID;
            old.Location = item.Location;
            old.Team1ID = item.Team1ID;
            old.Team2ID = item.Team2ID;
            ctx.SaveChanges();

        }

        public override Match GetOne(int id)
        {
            return ctx.Match.FirstOrDefault(t => t.ID == id);
        }
    }
}
