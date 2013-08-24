using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonSmash{
	class PokemonSlot {
		public PokemonSlot(Pokemon pkmn, Animation2D anim){
			this.pokemon = pkmn;
			this.animation = anim;
		}
		public Pokemon pokemon;
		public Animation2D animation;
	}
}
