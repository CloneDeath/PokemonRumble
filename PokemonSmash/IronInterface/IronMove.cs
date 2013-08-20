using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonSmash.IronInterface {
	public class IronMove {
		public static Dictionary<string, Move> Moves = new Dictionary<string, Move>();

		public Move Add(string name) {
			if (Moves.ContainsKey(name)) {
				return Moves[name];
			} else {
				Move tm = new Move(name);
				Moves.Add(name, tm);
				return tm;
			}
		}

		public Move Find(string name) {
			return Add(name);
		}
	}
}
