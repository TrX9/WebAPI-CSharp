using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace pokemonAPI.Models
{


    public class Pokemon
    {
        [Name("#")]
        public int Id { get; set; }
        [Name("Name")]
        public string Name { get; set; }
        [Name("Type 1")]
        public string Type_1 { get; set; }
        [Name("Type 2")]
        public string Type_2 { get; set; }
        [Name("Total")]
        public int Total { get; set; }
        [Name("HP")]
        public int HP { get; set; }
        [Name("Attack")]
        public int Attack { get; set; }
        [Name("Defense")]
        public int Defense { get; set; }
        [Name("Sp. Atk")]
        public int Sp_Atk { get; set; }
        [Name("Sp. Def")]
        public int Sp_Def { get; set; }
        [Name("Speed")]
        public int Speed { get; set; }
        [Name("Generation")]
        public int Generation { get; set; }
        [Name("Legendary")]
        public bool Legendary { get; set; }

        
    }
}
   