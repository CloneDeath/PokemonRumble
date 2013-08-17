using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using Box2DX.Common;
using GLImp;
using System.Diagnostics;

namespace PokemonRumble {
	public class Projectile : IEntity {
		Body projectile;
		Player creator;
		Fixture fix;

		public Action<Player> OnCollidePlayer;
		public Action OnCollideEarth;

		public float Duration = 1;

		public ushort CollisionMask {
			set {
				fix.Filter.MaskBits = value;
			}
		}

		public float Z {
			set {
				anim.Z = value;
			}
		}

		Animation anim;

		public void SetSkeleton(string name) {
			anim = new Animation(name);
		}

		public void SetAnimation(string animation, bool loop) {
			anim.state.SetAnimation(animation, loop);
		}

		Stopwatch UpdateTimer = new Stopwatch();

		public bool Permanent = false;

		public Projectile(Player creator, float x, float y, float width, float height) {
			this.creator = creator;

			BodyDef def = new BodyDef();
			def.IsBullet = true;
			def.Position = creator.body.GetPosition() + new Vec2(x, y);

			projectile = creator.body.GetWorld().CreateBody(def);
			PolygonDef fixdef = new PolygonDef();
			fixdef.Density = 1.0f;
			fixdef.SetAsBox(width / 2, height / 2);
			fixdef.Filter.GroupIndex = creator.ID;
			fix = projectile.CreateFixture(fixdef);
			fix.Filter.CategoryBits = 0x0004;
			fix.Filter.MaskBits = 0xFFFF;
			//fix.UserData = this;

			fixdef.IsSensor = true;
			Fixture fix2 = projectile.CreateFixture(fixdef);
			fix2.UserData = this;


			projectile.SetMassFromShapes();

			UpdateTimer.Start();
			GraphicsManager.Update += GraphicsManager_Update;
		}

		void GraphicsManager_Update() {
			float dt = UpdateTimer.ElapsedMilliseconds / 1000.0f;
			UpdateTimer.Restart();

			anim.skeleton.X = projectile.GetPosition().X;
			anim.skeleton.Y = projectile.GetPosition().Y;

			Duration -= dt;
			if (Duration <= 0 && !Permanent) {
				Unload();
			}
		}

		public void Unload() {
			projectile.GetWorld().DestroyBody(projectile);
			GraphicsManager.Update -= GraphicsManager_Update;
			anim.Unload();
		}

		public void SetVelocity(float x, float y) {
			projectile.SetLinearVelocity(new Vec2(x, y));
		}

		public void OnCollides(IEntity other) {
			if (other is Player && other != creator) {
				if (OnCollidePlayer != null) {
					OnCollidePlayer((Player)other);
				}
			}

			if (other is BattleArena) {
				if (OnCollideEarth != null) {
					OnCollideEarth();
				}
			}
		}

		public void OnSeperate(IEntity other) {
			
		}
	}
}
