using Esport.Data;
using Esport.Repository.Database;
using Esport.Repository.GenericRespository;
using Esport.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esport.Repository.ModelRepositories
{
    public class TeamRepository : MainRepository<Team>, IRepository<Team>
    {
        public TeamRepository(DataContext ctx)
            : base(ctx)
        {
        }

        public override void Update(Team item)
        {
            var old = GetOne(item.ID);
            old.Name = item.Name;
            old.Wins = item.Wins;
            old.Match = item.Match;
            old.Odd = item.Odd;
            old.BettedAmount = item.BettedAmount;
            old.MatchID = item.MatchID;
            ctx.SaveChanges();
        }

        public override Team GetOne(int id)
        {
            return ctx.Team.FirstOrDefault(t => t.ID == id);
        }
    }
}
