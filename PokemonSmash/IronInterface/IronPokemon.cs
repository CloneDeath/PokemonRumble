using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonSmash.IronInterface {
	public class IronPokemon {
		public static Pokemon Add(string Name) {
			Pokemon pkmn = new Pokemon(Name);
			PokemonManager.Pokemons.Add(Name, pkmn);
			return pkmn;
		}
	}
}
