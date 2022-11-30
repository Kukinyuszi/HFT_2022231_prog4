// <copyright file="Location.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Esport.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("Location")]
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        //[Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Match Match { get; set; }

        public int? MatchID { get; set; }

        public override string ToString()
        {
            return $"ID: {this.ID} \t Name: {this.Name}\t Capacity: {this.Capacity}";
        }

        public Location()
        {
            Match = new Match();
        }

        public Location(string li)
        {
            string[] split = li.Split('#');
            ID = int.Parse(split[0]);
            Name = split[1];
            Capacity = int.Parse(split[2]);
            MatchID = int.Parse(split[3]);
        }
    }
}
