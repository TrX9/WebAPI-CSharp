﻿using System.Collections.Generic;
using System.Web.Http;
using pokemonAPI.Models;
using pokemonAPI.Services;
using PagedList;

namespace pokemonAPI.Controllers
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

        [Route("pokemon")]
        [HttpGet]
        public IPagedList<Pokemon> Get(int page)
        {
            var pokemon = PokemonService.Get(page);

            return pokemon;
        }

    }
}
