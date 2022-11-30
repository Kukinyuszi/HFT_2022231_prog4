using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esport.Program
{
    using Esport.Data;
    using Esport.Logic;
    using Esport.Repository;
    using Esport.Repository.Database;
    using Esport.Repository.ModelRepositories;

    /// <summary>
    /// Factory class for creating the logic and repo parts for the program.
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Factory"/> class.
        /// </summary>
        public TeamLogic TeamLogic { get; set; }
        public MatchLogic MatchLogic { get; set; }
        public Factory()
        {
            DataContext ctx = new DataContext();

            TeamRepository TeamRepo = new TeamRepository(ctx);
            MatchRepository MatchRepo = new MatchRepository(ctx);
            LocationRepository LocationRepo = new LocationRepository(ctx);
            
            this.TeamLogic = new TeamLogic(TeamRepo);
            //this.MatchLogic = new MatchLogic(LocationRepo, TeamRepo);
            
        }
        
    }
}
