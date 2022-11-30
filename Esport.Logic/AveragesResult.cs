// <copyright file="AveragesResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Esport.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Esport.Data;

    /// <summary>
    /// Average result class.
    /// </summary>
    public class AveragesResult
    {
        /// <summary>
        /// Gets or sets the Name of the element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Wins of the element.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Gets or sets betted amount.
        /// </summary>
        public int BettedAmount { get; set; }

        /// <summary>
        ///  Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Match.
        /// </summary>
        public virtual Match Match { get; set; }

        /// <summary>
        /// Gets or sets the odd.
        /// </summary>
        public double Odd { get; set; }

        /// <summary>
        /// Gets or sets the Match id.
        /// </summary>
        public int? MatchID { get; set; }
        /*
        public override bool Equals(object obj)
        {
            if (obj is AveragesResult)
            {
                AveragesResult other = obj as AveragesResult;
                return this.Name == other.Name && this.Wins == other.Wins;
            }
            else
            {
                return false;
            }
        }
         */

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Name = {this.Name}, Wins = {this.Wins}";
        }
    }
}
