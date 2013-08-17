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

		public Action<DamageBox, Player> OnCollidePlayer;

		Stopwatch Update = new Stopwatch();
		Animation anim;

		public void SetSkeleton(string name) {
			anim = new Animation(name);
			anim.skeleton.X = fix.Body.GetPosition().X + xoff;
			anim.skeleton.Y = fix.Body.GetPosition().Y + yoff;
		}

		public void SetAnimation(string animation, bool loop) {
			anim.state.SetAnimation(animation, loop);
		}

		public void FlipAnimation() {
			anim.skeleton.FlipX = true;
		}

		float xoff;
		float yoff;

		public DamageBox(Player creator, float x, float y, float width, float height) {
			this.creator = creator;
			xoff = x;
			yoff = y;

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

			if (anim != null) {
				anim.skeleton.X = fix.Body.GetPosition().X + xoff;
				anim.skeleton.Y = fix.Body.GetPosition().Y + yoff;
			}

			Duration -= dt;
			if (!IsAlive) {
				fix.Body.DestroyFixture(fix);
				if (anim != null) {
					anim.Unload();
				}
				GraphicsManager.Update -= GraphicsManager_Update;
			}
		}

		bool Dead = false;

		public bool IsAlive {
			get {
				return Duration > 0 || Dead;
			}
		}

		public void OnCollides(IEntity other) {
			if (other is Player && other != creator) {
				if (OnCollidePlayer != null) {
					OnCollidePlayer(this, (Player)other);
				}
			}
		}

		public void OnSeperate(IEntity other) {
		}

		public void Unload() {
			this.Dead = true;
		}
	}
}
