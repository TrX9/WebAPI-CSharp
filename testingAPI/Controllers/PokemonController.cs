using System.Collections.Generic;
using System.Web.Http;
using pokemonAPI.Models;
using pokemonAPI.Services;
using PagedList;
using System.Net.Http;
using System.Linq;
using System;

namespace pokemonAPI.Controllers
{

    public class PokemonController : ApiController
    {
        public PokemonController()
        {

        }

        [Route("pokemon")]  //Search by name
        [HttpGet]
        public Pokemon Get(string Name)   //URI will be like this: ..../pokemon?name=xxxx
        {
            var pokemon = PokemonService.Get(Name);

            return pokemon;
        }

        [Route("pokemon")]  //Pagination
        [HttpGet]
        public IPagedList<Pokemon> Get(int page)  //URI will be like this: ..../pokemon?page=x
        {
            var pokemon = PokemonService.Get(page);

            return pokemon;
        }

        [Route("pokemon")]  //Filtering by HP, Attack & Defense
        [HttpGet]
        public List<Pokemon> Get()  //URI will be like this: e.g. ` ..../pokemon?hp[gte]=100&defense[lte]=200&attack[lte]=77`
        {
            var filterList = new List<Tuple<string, string>>();

            IEnumerable<KeyValuePair<string, string>> query = Request.GetQueryNameValuePairs();   //here we take the values from the request and put them in a list to be sent to the method
            foreach (var pair in query)
            {
                filterList.Add(new Tuple<string, string>(pair.Key, pair.Value));
            }

            var pokemon = PokemonService.Get(filterList);

            return pokemon;
        }

    }
}
