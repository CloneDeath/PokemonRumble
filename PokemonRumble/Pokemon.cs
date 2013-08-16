using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonRumble {
	public class Pokemon {
		public string Name {
			get;
			private set;
		}
		public int Speed;

		public Pokemon(string Name) {
			this.Name = Name;
		}
	}
}
