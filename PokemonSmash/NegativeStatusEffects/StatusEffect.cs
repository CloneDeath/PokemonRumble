using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonSmash
{
	public abstract class StatusEffect
	{
		public float Duration = 1.0f;
		public Player Owner;
		public StatusEffect(Player Owner, float Duration)
		{
			this.Owner = Owner;
			this.Duration = Duration;
		}

		abstract public void Initialize(Player p);
		public virtual void Update(Player p, float dt)
		{
			Duration -= dt;
			if (Duration <= 0) {
				p.RemoveEffect(this);
			}
		}
		public abstract void Uninitialize(Player p);
	}
}
