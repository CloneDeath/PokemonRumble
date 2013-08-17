using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonRumble {
	public class IronPokemon {
		public Pokemon Add(string Name) {
			Pokemon pkmn = new Pokemon(Name);
			PokemonManager.Pokemons.Add(Name, pkmn);
			return pkmn;
		}
	}
}
