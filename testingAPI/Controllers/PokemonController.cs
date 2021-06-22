using System.Collections.Generic;
using System.Web.Http;
using testingAPI.Models;
using testingAPI.Services;

namespace testingAPI.Controllers
{

    public class PokemonController : ApiController
    {
        public PokemonController()
        {

        }

        [Route("pokemon")]
        [HttpGet]
        public List<Pokemon> GetAll()
        {
            return PokemonService.GetAll();
        }

        [Route("pokemon/{name}")]
        [HttpGet]
        public Pokemon Get(string Name)
        {
            var pokemon = PokemonService.Get(Name);

            return pokemon;
        }
    }
}
