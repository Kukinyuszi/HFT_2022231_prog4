using Esport.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esport.Logic.Interfaces
{


    public interface ITeamLogic
    {
        void RemoveTeam(int id);

        void ChangeTeamName(Team item);

        void InsertNewTeam(Team item);

        IQueryable<Team> GetAllTeams();

        Team GetTeamById(int id);
    }

}
