// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Esport.Program
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Text;
    using System.Threading.Tasks;
    using ConsoleTools;
    using Esport.Data;
    using Esport.Logic;
    using Esport.Logic.Test;
    using Esport.Repository;
    using Esport.Repository.ModelRepositories;
    using Esport.Data;
    using Esport.Logic;
    using Esport.Repository;
    using Messages;
    using Esport.Repository.Database;

    /// <summary>
    /// The main program.
    /// </summary>
    internal class Program
    {
        /*
        private static void Main()
        {
            
            using DataContext ctx = new DataContext();
            MatchRepository repo = new MatchRepository(ctx);
            TeamRepository teamrepo = new TeamRepository(ctx);
            LocationRepository locationrepo = new LocationRepository(ctx);
            MatchRepository matchrepo = new MatchRepository(ctx);
            TeamLogic teamlogic = new TeamLogic(teamrepo);
            LocationLogic locationlogic = new LocationLogic(locationrepo);
            MatchLogic matchrepomatchlogic = new MatchLogic(matchrepo);
            Factory fcs;
            fcs = new Factory();

            //f1.Dispose();

            var crudMenu = new ConsoleMenu()
                .Add(">> LIST ALL TEAMS", () => ListAll(teamlogic))
                .Add(">> LIST ALL LOCATIONS", () => ListAll2(locationlogic))
                .Add(">> LIST ALL MATCHES", () => ListAll3(matchrepomatchlogic))
                .Add(">> GET TEAM BY ID", () => GetById(teamlogic))
                .Add(">> CHANGE TEAM WINS", () => ChangeTeamWins(teamlogic))
                .Add(">> ADD TEAM ", () => AddTeam(teamlogic, matchrepomatchlogic))
                .Add(">> ADD LOCATION ", () => AddLocation(locationlogic))
                .Add(">> ADD MATCH ", () => AddMatch(matchrepomatchlogic))
                .Add(">> DELETE TEAM", () => DeleteTeam(teamlogic))
                .Add(">> DELETE LOCATION", () => DeleteLocation(locationlogic))
                .Add(">> DELETE MATCH", () => DeleteMatch(matchrepomatchlogic, locationlogic, teamlogic))
                .Add(">> EDIT TEAM", () => EditTeam(teamlogic))
                .Add(">> EDIT LOCATION", () => EditLocation(locationlogic))
                .Add(">> EDIT MATCH", () => EditMatch(matchrepomatchlogic))
                .Add(">> EXIT", ConsoleMenu.Close);
            var menu = new ConsoleMenu()
                .Add(">> CRUD MENU", () => crudMenu.Show())
                .Add(">> BET", () => BetForTeam(teamlogic))
                .Add(">> LIST ALL BETTINGS", () => ListAllBettings(teamlogic))
                .Add(">> WHERE LOC CAP > 200", () => Le2(locationlogic))
                .Add(">> MATCHES, LOCATIONS, TEAMS", () => Le3(matchrepomatchlogic))
                .Add(">> WHERE MATCH ID IS GREATER THAN 5", () => Le4(ctx))
                .Add(">> WHERE THE LOCATION CAP IS LESS THAN THE MAX WIN", () => Le5(ctx))
                .Add(">> HOW MANY TEAMS COMPETES AT DIFFERENT LOCATIONS", () => Le6(ctx))
                .Add(">> GROUP LOCATIONS BY CAP AND LIST THEM WITH THE MATCHES", () => Le7(ctx))
                .Add(">> LOCATIONS WHERE MATCH ID IS GREATER THAN 2", () => Le8(ctx))
                .Add(">> TASK1", () => Task1(teamlogic))
                .Add(">> TASK2", () => Task2(teamlogic))
                .Add(">> TASK3", () => Task3(teamlogic))
                .Add(">> EXIT", ConsoleMenu.Close);

            menu.Show();
        }
                                                                
        private static void ListAll(TeamLogic logic)
        {
            Console.WriteLine(Message.Allteams());
            logic.GetAllTeams().ToList()

                .ForEach(x => Console.WriteLine(x.Name + "  Wins: " + x.Wins + "    Match location: " + x.Match.Location + "    Match name: " + x.Match.Name));

            Console.ReadLine();
        }

        private static List<Location> ListAll2(LocationLogic logic)
        {
            List<Location> locs = new List<Location>();
            Console.WriteLine(Message.AllLocations());
            logic.GetAllLocations().ToList()

                .ForEach(x => Console.WriteLine("Location: " + x.Name + " Capacity: " + x.Capacity + " Match name: " + x.Match.Name));

            foreach (var item in logic.GetAllLocations())
            {
                locs.Add(item);
            }

            Console.ReadLine();
            return locs;
        }

        private static void ListAll3(MatchLogic logic)
        {
            Console.WriteLine(Message.AllMatches());
            logic.GetAllMatches().ToList()

                .ForEach(x => Console.WriteLine(x.Location + " Match name: " + x.Name + " loc: " + x.Location));

            Console.ReadLine();
        }

        private static void GetById(TeamLogic logic)
        {
            Console.Write(Message.EnterID());
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                Console.WriteLine(Message.WrongFormat());
            }

            var q = logic.GetTeamById(id);
            Console.WriteLine(Message.SelectedTeam());
            Console.WriteLine(q);

            Console.ReadLine();
        }

        private static void ChangeTeamWins(TeamLogic logic)
        {
            Console.Write(Message.EnterID());
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                Console.WriteLine(Message.WrongFormat());
            }

            var q = logic.GetTeamById(id);
            if (q == null)
            {
                Console.WriteLine(Message.NotFound());
                Console.ReadLine();
            }
            else
            {
                int newWins = 0;
                do
                {
                    Console.Write(Message.EnterNewWins());
                    try
                    {
                        newWins = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine(Message.WrongFormat());
                    }
                }
                while (newWins < 0);

                //logic.ChangeTeamWins(id, newWins);
                Console.WriteLine(Message.NewTeamWins());
                Console.WriteLine(q.Wins);

                Console.ReadLine();
            }
        }

        private static void AddTeam(TeamLogic logic, MatchLogic matchlog)
        {
            string name;
            do
            {
                Console.WriteLine(Message.TeamName());
                name = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(name));

            int wins = -1;
            do
            {
                Console.WriteLine(Message.TeamWins());
                try
                {
                    wins = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine(Message.WrongFormat());
                }
            }
            while (wins < 0);

            int mid = -1;

            do
            {
                Console.WriteLine(Message.MatchID());
                try
                {
                    mid = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine(Message.WrongFormat());
                }
            }
            while (mid < 0 || mid > matchlog.GetAllMatches().Count());

            Team t = new Team() { Name = name, Wins = wins, MatchID = 4 };
            //logic.Addteam(t);
        }

        private static void AddMatch(MatchLogic logic)
        {
            string name;
            do
            {
                Console.WriteLine(Message.MatchName());
                name = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(name));
            string location;
            do
            {
                Console.WriteLine(Message.MatchLocation());
                location = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(location));

            int cap = -1;
            do
            {
                Console.WriteLine(Message.LocationCap());
                try
                {
                    cap = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine(Message.WrongFormat());
                }
            }
            while (cap <= 0);
            Location l = new Location() { Name = location, Capacity = cap };
            Match m = new Match() { Name = name, Location = location };
            m.Locations.Add(l);
            logic.InsertNewMatch(m);
        }

        private static void AddLocation(LocationLogic logic)
        {
            string name;
            do
            {
                Console.WriteLine(Message.LocationName());
                name = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(name));

            int cap = -1;
            do
            {
                Console.WriteLine(Message.LocationCap());
                try
                {
                    cap = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine(Message.WrongFormat());
                }
            }
            while (cap <= 0);

            Location l = new Location() { Name = name, Capacity = cap, MatchID = 2 };
            logic.InsertNewLocation(l);
        }

        private static void DeleteTeam(TeamLogic logic)
        {
            Console.WriteLine(Message.TeamID());
            int id = -1;
            try
            {
                id = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                Console.WriteLine(Message.WrongFormat());
            }

            Team t = logic.GetTeamById(id);
            logic.RemoveTeam(id);
        }

        private static void DeleteLocation(LocationLogic logic)
        {
            int id = -1;
            do
            {
                Console.WriteLine(Message.LocationID());
                try
                {
                    id = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine(Message.WrongFormat());
                }
            }
            while (id <= 0 || id > logic.GetAllLocations().Count());

            Location l = logic.GetLocationById(id);
            if (l != null)
            {
                logic.RemoveLocation(id);
            }
        }

        private static void DeleteMatch(MatchLogic logic, LocationLogic loclo, TeamLogic teamlo)
        {
            int id = -1;
            do
            {
                Console.WriteLine(Message.MatchID());
                try
                {
                    id = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine(Message.WrongFormat());
                }
            }
            while (id <= 0 || id > logic.GetAllMatches().Count());

            Match m = logic.GetMatchById(id);
            Team t1 = new Team();
            Team t2 = new Team();
            Location l = new Location();
            if (m != null)
            {
                foreach (var item in loclo.GetAllLocations())
                {
                    if (item.Match.Name == m.Name)
                    {
                        l = item;
                    }
                }

                foreach (var item in teamlo.GetAllTeams())
                {
                    if (item.Match.Name == m.Name)
                    {
                        t1 = item;
                    }
                }

                foreach (var item in teamlo.GetAllTeams())
                {
                    if (item.Match.Name == m.Name && item.Name != t1.Name)
                    {
                        t2 = item;
                    }
                }

                teamlo.RemoveTeam(t1.ID);
                teamlo.RemoveTeam(t2.ID);
                loclo.RemoveLocation(l.ID);
                logic.RemoveMatch(id);
            }
        }

        private static void EditTeam(TeamLogic logic)
        {
            Console.WriteLine(Message.TeamID());
            int id = -1;
            try
            {
                id = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                Console.WriteLine(Message.WrongFormat());
            }

            Team t = logic.GetTeamById(id);
            if (t != null)
            {
                Console.WriteLine(Message.TeamName());
                t.Name = Console.ReadLine();
                Console.WriteLine(Message.TeamWins());
                t.Wins = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
            }
        }

        private static void EditLocation(LocationLogic logic)
        {
            Console.WriteLine(Message.LocationID());
            int id = -1;
            try
            {
                id = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                Console.WriteLine(Message.WrongFormat());
            }

            Location l = logic.GetLocationById(id);
            if (l != null)
            {
                Console.WriteLine(Message.LocationName());
                l.Name = Console.ReadLine();
                Console.WriteLine(Message.LocationCap());
                l.Capacity = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
            }
        }

        private static void EditMatch(MatchLogic logic)
        {
            Console.WriteLine(Message.MatchID());
            int id = -1;
            try
            {
                id = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                Console.WriteLine(Message.WrongFormat());
            }

            Match m = logic.GetMatchById(id);
            if (m != null)
            {
                Console.WriteLine(Message.MatchName());
                m.Name = Console.ReadLine();
                Console.WriteLine(Message.MatchLocation());
                m.Location = Console.ReadLine();
            }
        }

        private static void BetForTeam(TeamLogic logic)
        {
            int id = 0;
            do
            {
                Console.WriteLine(Message.TeamID());
                try
                {
                    id = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine(Message.WrongFormat());
                }
            }
            while (id > logic.GetAllTeams().Count() || id <= 0);

            Team t = logic.GetTeamById(id);
            int betted = 0;
            do
            {
                Console.WriteLine(Message.BetAmount());
                try
                {
                    betted = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine(Message.WrongFormat());
                }
            }
            while (betted < 0);
            t.BettedAmount += betted;
        }

        private static void ListAllBettings(TeamLogic logic)
        {
            if (!logic.ListAllBettings().Any())
            {
                Console.WriteLine(Message.NoBetsYet());
            }

            logic.ListAllBettings().ToList()

                .ForEach(x => Console.WriteLine(x.Name + " BETTED AMOUNT: " + x.BettedAmount));

            Console.ReadLine();
        }

        private static void Le2(LocationLogic log)
        {
            var q = log.GetAllLocations().ToList().Where(x => x.Capacity > 200).Select(x => x.Match);

            foreach (var item in q)
            {
                Console.WriteLine("MATCH NAME: " + item.Name + "\tMATCH LOCATION: " + item.Location);
            }

            Console.ReadLine();
        }

        private static void Le3(MatchLogic log)
        {
            var q = log.GetAllMatches().Where(x => x.Locations.ToList().FirstOrDefault().Capacity > 2);

            foreach (var item in q)
            {
                Console.WriteLine("MATCH NAME: " + item.Name + " \tTEAMS NAME: " + item.Teams.FirstOrDefault() + " And " + item.Teams.LastOrDefault() + " \tMatch location: " + item.Location);
            }

            Console.ReadLine();
        }

        private static void Le4(DataContext ctx)
        {
            var qx_sub = from x in ctx.Match
                         where x.ID > 5
                         group x by x.Location into g
                         select new
                         {
                             LOC_NAME = g.Key,
                         };

            var qx = from x in ctx.Location
                     join z in qx_sub on x.Name equals z.LOC_NAME
                     let joinedItem = new { x.ID, x.Match }
                     group joinedItem by joinedItem.Match.Name into grp
                     select new
                     {
                         Key = grp.Key,
                     };
            foreach (var item in qx)
            {
                Console.WriteLine(item.Key);
            }

            Console.ReadLine();
        }

        private static void Le5(DataContext ctx)
        {
            var maxwin = ctx.Team
                .OrderByDescending(x => x.Wins)
                .FirstOrDefault().Wins;

            var q7 = from x in ctx.Location
                     where x.Capacity < maxwin
                     select new
                     {
                         Cap = x.Capacity,
                         ID = x.ID,
                     };

            foreach (var item in q7)
            {
                Console.WriteLine(item.Cap);
                Console.WriteLine(item.ID);
            }

            Console.ReadLine();
        }

        private static void Le6(DataContext ctx)
        {
            var qx_sub = from x in ctx.Match
                         group x by x.Location into g
                         select new
                         {
                             LOC_NAME = g.Key,
                             COUNT = g.Count(),
                         };

            var qx = from x in ctx.Team
                     join z in qx_sub on x.Match.Location equals z.LOC_NAME
                     let joinedItem = new { x.ID, x.Match, z.COUNT, }
                     group joinedItem by joinedItem.Match.Location into grp
                     select new
                     {
                         Key = grp.Key,
                         Sum = grp.Sum(x => x.COUNT),
                     };

            foreach (var item in qx)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Sum);
            }

            Console.ReadLine();
        }

        private static void Le7(DataContext ctx)
        {
            var qx_sub = from x in ctx.Location
                         group x by x.Capacity into g
                         select new
                         {
                             LOC_ID = g.Key,
                             LOC_COUNT = g.Count(),
                         };
            var qx = from x in ctx.Match
                     join z in qx_sub on x.ID equals z.LOC_COUNT
                     let joinedItem = new { x.ID, x.Location, z.LOC_COUNT }
                     group joinedItem by joinedItem.ID into grp
                     select new
                     {
                         Key = grp.Key,
                         Sum = grp.Sum(x => x.LOC_COUNT),
                     };

            foreach (var item in qx)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Sum);
            }

            Console.ReadLine();
        }

        private static void Le8(DataContext ctx)
        {
            var qx_sub = from x in ctx.Location
                         where x.Match.ID > 2
                         group x by x.Match.Name into g
                         select new
                         {
                             MATCH = g.Key,
                         };

            var qx = from x in ctx.Match
                     join z in qx_sub on x.Name equals z.MATCH
                     let joinedItem = new { x.ID, z.MATCH, x.Location }
                     group joinedItem by joinedItem.Location into grp
                     select new
                     {
                         Key = grp.Key,
                     };
            foreach (var item in qx)
            {
                Console.WriteLine(item.Key);
            }

            Console.ReadLine();
        }

        private static void Task1(TeamLogic tl)
        {
            Task<Team> t = tl.ElemezTask();
            Console.WriteLine(t.Result);
            Console.ReadLine();
        }

        private static void Task2(TeamLogic tl)
        {
            Task<Team> t = tl.OddLessThan3Task();
            Console.WriteLine(t.Result);
            Console.ReadLine();
        }

        private static void Task3(TeamLogic tl)
        {
            Task<Team> t = tl.Tasks();
            Console.WriteLine(t.Result);
            Console.ReadLine();
        }
        */
    }
        
}
