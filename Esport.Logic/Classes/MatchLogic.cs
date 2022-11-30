// <copyright file="MatchLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Esport.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using Esport.Data;
    using Esport.Repository;
    using Esport.Repository.Interfaces;

    /// <summary>
    /// Match logic.
    /// </summary>
    public class MatchLogic : IMatchLogic
    {
        IRepository<Match> matchrepo;
        IRepository<Team> teamrepo;
        IRepository<Location> locrepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchLogic"/> class.
        /// MatchLogic Constructor.
        /// </summary>
        /// <param name="matchrepo">The Repository parameter of the Matchlogic entity.</param>
        public MatchLogic(IRepository<Match> matchrepo)
        {
            this.matchrepo = matchrepo;
        }

        public void RemoveMatch(int id)
        {
            this.matchrepo.Remove(id);
        }

        public void UpdateMatch(Match item)
        {
            this.matchrepo.Update(item);
        }

        public void InsertNewMatch(Match item)
        {
            this.matchrepo.Insert(item);
        }

        public IQueryable<Match> GetAllMatches()
        {
            return this.matchrepo.GetAll();
        }

        public Match GetMatchById(int id)
        {
            return this.matchrepo.GetOne(id);
        }
    }
}
