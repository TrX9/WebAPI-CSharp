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
using pokemonAPI.Models;
using pokemonAPI.Services;

namespace pokemonAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
                return View();
        }
        
    }
}
