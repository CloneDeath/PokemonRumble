using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonSmash.PokemonTypesData;

namespace PokemonSmash {
	public class Move {
		private string name;

		public string DisplayName;
		public Action<Player> OnUse;
		public PokemonTypeInfo Type = PokemonType.Unknown;
		public PokemonMoveInfo Category = PokemonType.Status;

		public Move(string name) {
			this.name = name;
			this.DisplayName = name;
		}
	}
}
