// <copyright file="Match.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Esport.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using System.Text.Json.Serialization;

    public class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonIgnore]
        public virtual Location Location  { get; set; }

        //[Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Location> Locations { get; }

        [JsonIgnore]
        public virtual ICollection<Team> Teams { get; }

        [Required]
        public int Team1ID { get; set; }

        [Required]
        public int Team2ID { get; set; }

        public override string ToString()
        {
            return $"ID: {this.ID} \t Location: {this.Location} \t Name: {this.Name}";
        }

        public Match()
        {
            Locations = new HashSet<Location>();
            Teams = new HashSet<Team>();
        }

        public Match(string li)
        {
            string[] split = li.Split('#');
            ID = int.Parse(split[0]);
            Name = split[2];
            Team1ID = int.Parse(split[3]);
            Team2ID = int.Parse(split[4]);
            Teams = new HashSet<Team>();
        }
    }
}
