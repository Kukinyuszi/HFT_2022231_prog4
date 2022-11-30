// <copyright file="TeamLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Esport.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using Esport.Data;
    using Esport.Logic.Interfaces;
    using Esport.Repository;
    using Esport.Repository.Interfaces;

    /// <summary>
    /// Teamlogic.
    /// </summary>
    public class TeamLogic : ITeamLogic, IBetting, IOddsGenerator
    {
        IRepository<Team> teamrepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamLogic"/> class.
        /// The constructor of the TeamLogic object.
        /// </summary>
        /// 
        /*
        public TeamLogic()
        {
        }
        */
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamLogic"/> class.
        /// TeamLogic constructor.
        /// </summary>
        /// <param name="rep">Gets the repository parameter.</param>
        public TeamLogic(IRepository<Team> rep)
        {
            this.teamrepo = rep;
        }

        /// <summary>
        /// Retuns a task.
        /// </summary>
        /// <returns>A task.<see cref="System.Threading.Tasks.Task"/> representing the result of the asynchronous operation.</returns>
        public Task<Team> Tasks()
        {
            Task t = new Task(() => this.WhereF());
            return Task.Run(this.WhereF);
        }

        /// <summary>
        /// Where the Team name contains F Task.
        /// </summary>
        /// <returns>A team.</returns>
        public Team WhereF()
        {
            List<Team> teams = new List<Team>();
            foreach (Team item in this.teamrepo.GetAll().ToList())
            {
                teams.Add(item);
            }

            var q = teams.Where(x => x.Name.Contains('F', StringComparison.CurrentCulture)).ToList().FirstOrDefault();
            return q;
        }

        /// <summary>
        /// ITeamrepository.
        /// </summary>
        /// <returns>The Iteamrepository.</returns>
        public IRepository<Team> Getrepo()
        {
            return this.teamrepo;
        }
        /// <summary>
        /// Betting method.
        /// </summary>
        /// <param name="teamid">Gets a team by id.</param>
        /// <param name="amount">Gets the amount of the betting.</param>
        public void Bet(int teamid, int amount)
        {
            Team t = this.teamrepo.GetOne(teamid);
            t.BettedAmount += amount;
        }

        /// <summary>
        /// Lists all the bettings for the teams.
        /// </summary>
        /// <returns>List.</returns>
        public ICollection<Team> ListAllBettings()
        {
            List<Team> betted = new List<Team>();
            betted = this.teamrepo.GetAll().ToList().Where(x => x.BettedAmount > 0).ToList();
            return betted;
        }

        /// <summary>
        /// Returns the average wins.
        /// </summary>
        /// <returns>Double.</returns>
        public double GetAverageWins()
        {
            int avg = 0;
            int num = 0;
            foreach (var item in this.teamrepo.GetAll())
            {
                avg += item.Wins;
                num++;
            }

            double act = avg / num;
            return act;
        }

        /// <summary>
        /// Where wins are greater then 200.
        /// </summary>
        /// <returns>Task.</returns>
        public Team Elemez()
        {
            List<Team> teams = new List<Team>();
            foreach (Team item in this.teamrepo.GetAll().ToList())
            {
                teams.Add(item);
            }

            var q = teams.Where(x => x.Wins > 200).ToList().FirstOrDefault();
            return q;
        }

        /// <summary>
        /// Where wins are greater then 200.E.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<Team> ElemezTask()
        {
            Task t = new Task(() => this.Elemez());
            return Task.Run(this.Elemez);
        }

        /// <summary>
        /// Where the odd is less than 3.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Team OddLessThan3()
        {
            List<Team> teams = new List<Team>();
            foreach (var item in this.teamrepo.GetAll().ToList())
            {
                teams.Add(item);
            }

            var q = teams.Where(x => x.Odd < 3).ToList().FirstOrDefault();
            return q;
        }

        /// <summary>
        /// Where the odd is less than 3 task.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<Team> OddLessThan3Task()
        {
            Task t = new Task(() => this.OddLessThan3());
            return Task.Run(this.OddLessThan3);
        }

        /// <inheritdoc/>
        public void OddGenerator(int teamid)
        {
            foreach (Team item in this.teamrepo.GetAll())
            {
                item.Odd = RandomNumberGenerator.GetInt32(10);
            }
        }
        public TeamLogic ReturnThisLogic()
        {
            return this;
        }

        public void RemoveTeam(int id)
        {
            this.teamrepo.Remove(id);
        }

        public void ChangeTeamName(Team item)
        {
            this.teamrepo.Update(item);
        }

        public void InsertNewTeam(Team item)
        {
            this.teamrepo.Insert(item);
        }

        public IQueryable<Team> GetAllTeams()
        {
            return this.teamrepo.GetAll();
        }

        public Team GetTeamById(int id)
        {
            Team t = null;
            foreach (var item in this.teamrepo.GetAll())
            {
                if (id == item.ID)
                {
                    t = item;
                }
            }
            return t;
        }
    }
}
