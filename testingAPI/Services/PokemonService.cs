using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using pokemonAPI.Models;
using CsvHelper;
using System.Text.RegularExpressions;
using PagedList;

namespace pokemonAPI.Services
{
    public static class PokemonService
    {
        static List<Pokemon> Pokemons { get; }
        static PokemonService()
        {
            string fileName = "pokemon.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            using (var streamReader = new StreamReader(path))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    Pokemons = csvReader.GetRecords<Pokemon>().ToList();
                    foreach (var Pokemon in Pokemons)
                    {
                        if (Pokemon.Legendary == true || Pokemon.Type_1 == "Ghost" || Pokemon.Type_2 == "Ghost") { Pokemon.Name = "ToDelete"; }
                        if (Pokemon.Type_1 == "Steel" || Pokemon.Type_2 == "Steel") { Pokemon.HP *= 2; }
                        if (Pokemon.Type_1 == "Fire" || Pokemon.Type_2 == "Fire") { Pokemon.Attack = (int)(Pokemon.Attack - Math.Floor(Pokemon.Attack * 0.1)); }
                        if ((Pokemon.Type_1 == "Bug" || Pokemon.Type_1 == "Flying") && (Pokemon.Type_2 == "Bug" || Pokemon.Type_2 == "Flying")) { Pokemon.Speed = (int)(Pokemon.Speed + Math.Floor(Pokemon.Speed * 0.1)); }
                        if (Regex.IsMatch(Pokemon.Name[0].ToString(), "G"))
                        {
                            int length = (Pokemon.Name.Length) - 1;
                            Pokemon.Defense += (5 * length);
                        }
                    }
                    Pokemons.RemoveAll(p => p.Name == "ToDelete");
                }

            }
        }

        public static List<Pokemon> GetAll()
        {
            return Pokemons;
        }

        public static Pokemon Get(string Name)
        {
            return Pokemons.FirstOrDefault(p => p.Name == Name);
        }

        public static PagedList<Pokemon> Get(int page)
        {
            int pageSize = 10;
            IPagedList<Pokemon> PokePagedList = null;
            PokePagedList = Pokemons.ToPagedList(page, pageSize);
            return (PagedList<Pokemon>)PokePagedList;
        }
    }
}