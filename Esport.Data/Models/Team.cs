namespace Esport.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using System.Text.Json.Serialization;

    public class Team
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public int Wins { get; set; }

        [JsonIgnore]
        public virtual Match Match { get; set; }

        public double Odd { get; set; }

        public int? MatchID { get; set; }

        public int BettedAmount { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

        public Team()
        {
            Match = new Match();
        }

        public Team(string li)
        {
            string[] split = li.Split('#');
            ID = int.Parse(split[0]);
            Name = split[1];
            Wins = int.Parse(split[2]);
            BettedAmount = int.Parse(split[3]);
            MatchID = int.Parse(split[4]);
        }
    }
}
