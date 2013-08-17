using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using Box2DX.Common;

namespace PokemonRumble {
	public class DamageBox : IEntity {
		Fixture fix;
		Player creator;
		public float Duration = 1.0f;

		public Action<Player> OnDamage;

		public DamageBox(Player creator, float x, float y, float width, float height) {
			this.creator = creator;

			PolygonDef def = new PolygonDef();
			def.SetAsBox(width / 2, height / 2, new Vec2(x, y), 0);
			def.IsSensor = true;
			def.UserData = this;

			fix = creator.body.CreateFixture(def);
			fix.UserData = this;
		}

		public float Damage = 0;
		public bool IsAlive {
			get {
				return Duration > 0;
			}
		}

		public void OnCollides(IEntity other) {
			if (other is Player && other != creator) {
				((Player)other).TakeDamage(this.Damage);

				if (OnDamage != null) {
					OnDamage((Player)other);
				}
			}
		}

		public void OnSeperate(IEntity other) {
		}

		internal void Update(float dt) {
			Duration -= dt;
		}

		internal void Unload() {
			fix.Body.DestroyFixture(fix);
		}
	}
}
