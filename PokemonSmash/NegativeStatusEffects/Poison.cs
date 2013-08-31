using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonSmash.NegativeStatusEffects
{
	public class Poison : StatusEffect
	{
		float DPS = 0;

		public Poison(float Damage, Player Owner, float Duration) : base(Owner, Duration)
		{
			DPS = Damage / Duration;
		}

		public override void Initialize(Player p)
		{	
		}

		public override void Update(Player p, float dt)
		{
			base.Update(p, dt);
			p.TakeSpecialDamage(DPS * dt, Owner);
		}

		public override void Uninitialize(Player p)
		{
		}
	}
}
