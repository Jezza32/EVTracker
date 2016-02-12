﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Pokemon.EVTracker.PokemonService.Models;
using Pokemon.EVTracker.PokemonService.Repositories;

namespace Pokemon.EVTracker.PokemonService.Controllers
{
    [Route("api/v0/[controller]")]
    public class PokemonController : Controller
    {
        private readonly PokemonRepository _pokemonRepository;

        public PokemonController([FromServices] PokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        public ComputedPokemon Get()
        {
            return _pokemonRepository.Get().Result;
        }
    }
}