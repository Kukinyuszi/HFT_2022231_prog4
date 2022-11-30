namespace Esport.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    using Esport.Data;

    public interface IMatchLogic
    {

        void RemoveMatch(int id);

        void UpdateMatch(Match item);

        void InsertNewMatch(Match item);

        IQueryable<Match> GetAllMatches();

        Match GetMatchById(int id);
    }
}