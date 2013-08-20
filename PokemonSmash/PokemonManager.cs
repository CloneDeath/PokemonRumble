using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonSmash {
	class PokemonManager {
		public static Dictionary<string, Pokemon> Pokemons = new Dictionary<string, Pokemon>();

		internal static Pokemon Find(string PokemonName) {
			return Pokemons[PokemonName];
		}
	}
}
