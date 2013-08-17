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
		public string DisplayName;

		public Move[] Move = new Move[4];
		public List<MixItem> MixQueue = new List<MixItem>();
		Dictionary<string, string> AnimationAlias = new Dictionary<string, string>();
		public float HP;


		public Pokemon(string Name) {
			this.Name = Name;
		}

		
		public void SetMix(string from, string to, float time){
			MixQueue.Add(new MixItem(from, to, time));
		}

		

		public void AddAnimationAlias(string from, string to) {
			AnimationAlias[from] = to;
		}

		public string FilterAnimationAlias(string from) {
			if (AnimationAlias.ContainsKey(from)) {
				return AnimationAlias[from];
			} else {
				return from;
			}
		}
	}
}
