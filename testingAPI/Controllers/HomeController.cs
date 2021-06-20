using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using testingAPI.Models;

namespace testingAPI.Controllers
{
    public class HomeController : Controller
    {
        List<Pokemon> records;
        public ActionResult Index()
        {
            string fileName = "pokemon.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            using (var streamReader = new StreamReader(path))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    records = csvReader.GetRecords<Pokemon>().ToList();
                    foreach(var Pokemon in records)
                    {
                        if (Pokemon.Legendary == true || Pokemon.Type_1 == "Ghost" || Pokemon.Type_2 == "Ghost" ) { Pokemon.Name = "ToDelete"; }
                        if (Pokemon.Type_1 == "Steel" || Pokemon.Type_2 == "Steel") { Pokemon.HP *= 2; }
                        if (Pokemon.Type_1 == "Fire" || Pokemon.Type_2 == "Fire") { Pokemon.Attack = (int)(Pokemon.Attack-Math.Floor(Pokemon.Attack*0.1)); }
                        if ((Pokemon.Type_1 == "Bug" || Pokemon.Type_1 == "Flying") && (Pokemon.Type_2 == "Bug" || Pokemon.Type_2 == "Flying")) { Pokemon.Speed = (int)(Pokemon.Speed + Math.Floor(Pokemon.Speed * 0.1)); }
                        if (Regex.IsMatch(Pokemon.Name[0].ToString(), "G"))
                            {
                                int length = (Pokemon.Name.Length) - 1;
                                Pokemon.Defense += (5 * length);
                            }
                        Debug.Write(message: Pokemon.ToString());
                    }
                    records.RemoveAll(p => p.Name == "ToDelete");
                    Debug.Write(records.ToString());
                }
                return View();
                               
            }

            //ViewBag.Title = "Home Page";

            //return View();
        }
    }
}
