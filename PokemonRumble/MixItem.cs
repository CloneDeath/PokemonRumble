using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonRumble {
	public class MixItem {
		public string From;
		public string To;
		public float Time;
		public MixItem(string f, string t, float time) {
			From = f;
			To = t;
			Time = time;
		}
	}
}
