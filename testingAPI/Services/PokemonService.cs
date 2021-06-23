using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using pokemonAPI.Models;
using CsvHelper;
using System.Text.RegularExpressions;
using PagedList;

namespace pokemonAPI.Services
{
    //Used Csvhelper to read and apply the conditions to the csv file
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

        public static Pokemon Get(string Name)           //Gets the pokemon by name
        {
            return Pokemons.FirstOrDefault(p => p.Name == Name);
        }

        public static PagedList<Pokemon> Get(int page)       //Gets a maximum of 10 pokemons based on the page requested 
        {                                                    //Used pagedList package for quick paging.
            int pageSize = 10;
            IPagedList<Pokemon> PokePagedList = null;
            PokePagedList = Pokemons.ToPagedList(page, pageSize);
            return (PagedList<Pokemon>)PokePagedList;
        }

        public static List<Pokemon> Get(List<Tuple<string, string>> keysList)
        {
            string hpO = "g"; //HP operator(greater or less than)
            string attkO = "g";                                            //we will iterate on the contents of the request and check what is the user requesting
            string defO = "g";                                             // then we will save each value and key to its declared variable to make it easier to work with and filter the data
            int hp = 0;
            int attk = 0; 
            int def = 0;
            List<Pokemon> PokemonsFiltered = null;

            for (int i = 0; i < keysList.Count; i++)
            {
                if (keysList[i].Item1.Substring(0, 1) == "h")
                {
                    hp = int.Parse(keysList[i].Item2);
                    hpO = keysList[i].Item1.Substring(3, 1);
                } 
                else if (keysList[i].Item1.Substring(0, 1) == "a")
                {
                    attk = int.Parse(keysList[i].Item2);
                    attkO = keysList[i].Item1.Substring(7, 1);
                }
                else if (keysList[i].Item1.Substring(0, 1) == "d")
                {
                    def = int.Parse(keysList[i].Item2);
                    defO = keysList[i].Item1.Substring(8, 1);
                }

            }

            if (hpO == "g" && attkO == "g" && defO == "g")
            {
                PokemonsFiltered = (Pokemons.Where(p => p.HP >= hp).Where(p => p.Attack >= attk).Where(p => p.Defense >= def)).ToList();
            }
            else if (hpO == "g" && attkO == "l" && defO == "g")
            {
                PokemonsFiltered = (Pokemons.Where(p => p.HP >= hp).Where(p => p.Attack <= attk).Where(p => p.Defense >= def)).ToList();
            }
            else if (hpO == "g" && attkO == "g" && defO == "l")
            {
                PokemonsFiltered = (Pokemons.Where(p => p.HP >= hp).Where(p => p.Attack >= attk).Where(p => p.Defense <= def)).ToList();
            }
            else if (hpO == "l" && attkO == "g" && defO == "g")
            {
                PokemonsFiltered = (Pokemons.Where(p => p.HP <= hp).Where(p => p.Attack >= attk).Where(p => p.Defense >= def)).ToList();
            }
            else if (hpO == "l" && attkO == "l" && defO == "g")
            {
                PokemonsFiltered = (Pokemons.Where(p => p.HP <= hp).Where(p => p.Attack <= attk).Where(p => p.Defense >= def)).ToList();
            }
            else if  (hpO == "g" && attkO == "l" && defO == "l")
            {
                PokemonsFiltered = (Pokemons.Where(p => p.HP >= hp).Where(p => p.Attack <= attk).Where(p => p.Defense <= def)).ToList();
            }
            else if  (hpO == "l" && attkO == "l" && defO == "l")
            {
                PokemonsFiltered = (Pokemons.Where(p => p.HP <= hp).Where(p => p.Attack <= attk).Where(p => p.Defense <= def)).ToList();
            }

                return PokemonsFiltered;
        }
    }
}