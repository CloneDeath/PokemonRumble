using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonSmash.NegativeStatusEffects
{
	public class Sleep : StatusEffect
	{
		public Sleep(Player p, float d) : base(p, d) { }

		public override void Initialize(Player p)
		{
			p.Sleep();
		}

		public override void Uninitialize(Player p)
		{
			p.Awake();
		}
	}
}
