using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using Box2DX.Common;
using GLImp;
using System.Diagnostics;

namespace PokemonRumble {
	public class DamageBox : IEntity {
		Fixture fix;
		Player creator;
		public float Duration = 1.0f;

		public Action<Player> OnCollidePlayer;

		Stopwatch Update = new Stopwatch();

		public DamageBox(Player creator, float x, float y, float width, float height) {
			this.creator = creator;

			PolygonDef def = new PolygonDef();
			def.SetAsBox(width / 2, height / 2, new Vec2(x, y), 0);
			def.IsSensor = true;
			def.UserData = this;

			fix = creator.body.CreateFixture(def);
			fix.UserData = this;

			Update.Start();
			GraphicsManager.Update += GraphicsManager_Update;
		}

		void GraphicsManager_Update() {
			float dt = Update.ElapsedMilliseconds / 1000.0f;
			Update.Restart();
			Duration -= dt;
			if (!IsAlive) {
				Unload();
			}
		}

		public bool IsAlive {
			get {
				return Duration > 0;
			}
		}

		public void OnCollides(IEntity other) {
			if (other is Player && other != creator) {
				((Player)other).TakeDamage(this.Damage);

				if (OnCollidePlayer != null) {
					OnCollidePlayer((Player)other);
				}
			}
		}

		public void OnSeperate(IEntity other) {
		}

		internal void Unload() {
			fix.Body.DestroyFixture(fix);
			GraphicsManager.Update -= GraphicsManager_Update;
		}
	}
}
