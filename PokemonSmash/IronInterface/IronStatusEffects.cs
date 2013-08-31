using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonSmash.NegativeStatusEffects;

namespace PokemonSmash.IronInterface
{
	static class IronStatusEffects
	{
		public static Poison Poison(float damage, Player owner, float duration){
			return new Poison(damage, owner, duration);
		}

		public static Sleep Sleep(Player owner, float duration)
		{
			return new Sleep(owner, duration);
		}
	}
}
