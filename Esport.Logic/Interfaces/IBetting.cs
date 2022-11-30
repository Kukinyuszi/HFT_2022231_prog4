// <copyright file="IBetting.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Esport.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Esport.Data;

    /// <summary>
    /// The IBetting interface.
    /// </summary>
    public interface IBetting
    {
        /// <summary>
        /// Bet.
        /// </summary>
        /// <param name="teamid">Bets to a team by id.</param>
        /// <param name="amount">Bets an amount for the team.</param>
        void Bet(int teamid, int amount);

        /// <summary>
        /// Lists the all bettings.
        /// </summary>
        /// <returns>List.</returns>
        ICollection<Team> ListAllBettings();
    }

    /// <summary>
    /// Generates the odds.
    /// </summary>
    public interface IOddsGenerator
    {
        /// <summary>
        /// Generates the odds.
        /// </summary>
        /// <param name="teamid">Generates an odd to a team.</param>
        void OddGenerator(int teamid);
    }
}
