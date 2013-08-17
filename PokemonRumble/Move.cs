using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonRumble {
	public class Move {
		private string name;

		public Action<Player> OnUse;

		public Move(string name) {
			this.name = name;
		}
	}
}
