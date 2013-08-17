using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using Box2DX.Common;
using GLImp;
using System.Diagnostics;
using Box2DX.Collision;

namespace PokemonRumble {
	public class Projectile : IEntity {
		Body projectile;
		Player creator;
		Fixture fix;

		public Action<Projectile, Player> OnCollidePlayer;
		public Action<Projectile> OnCollideEarth;
		public Action<Projectile, float> OnUpdate;

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
			anim.skeleton.X = fix.Body.GetPosition().X;
			anim.skeleton.Y = fix.Body.GetPosition().Y;
		}

		public void SetAnimation(string animation, bool loop) {
			anim.state.SetAnimation(animation, loop);
		}

		public float Scale {
			set {
				anim.Scale = value;
			}
		}

		Stopwatch UpdateTimer = new Stopwatch();

		public bool Permanent = false;
		float time = 0;
		Fixture fix2;

		public Projectile(Player creator, float x, float y, float width, float height) {
			this.creator = creator;

			this.Dead = false;

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
			fix2 = projectile.CreateFixture(fixdef);
			fix2.UserData = this;


			projectile.SetMassFromShapes();

			UpdateTimer.Start();
			GraphicsManager.Update += GraphicsManager_Update;
		}

		void GraphicsManager_Update() {
			float dt = UpdateTimer.ElapsedMilliseconds / 1000.0f;
			UpdateTimer.Restart();

			time += dt;
			if (OnUpdate != null) {
				OnUpdate(this, time);
			}

			anim.skeleton.X = projectile.GetPosition().X;
			anim.skeleton.Y = projectile.GetPosition().Y;
			//Console.WriteLine(anim.skeleton.Y);

			Duration -= dt;
			if ((Duration <= 0 && !Permanent) || Dead) {
				GraphicsManager.Update -= GraphicsManager_Update;
				projectile.DestroyFixture(fix);
				projectile.DestroyFixture(fix2);
				projectile.GetWorld().DestroyBody(projectile);
				anim.Unload();
			}
		}

		public void ApplyForce(float x, float y) {
			projectile.ApplyForce(new Vec2(x, y), projectile.GetMassData().Center);
		}

		public float Mass {
			set {
				MassData data = projectile.GetMassData();
				data.Mass = value;
				projectile.SetMass(data);
			}
		}

		bool Dead;

		public void Unload() {
			Dead = true;
		}

		public void SetVelocity(float x, float y) {
			projectile.SetLinearVelocity(new Vec2(x, y));
		}

		public void OnCollides(IEntity other) {
			if (other is Player && other != creator) {
				if (OnCollidePlayer != null) {
					OnCollidePlayer(this, (Player)other);
				}
			}

			if (other is BattleArena) {
				if (OnCollideEarth != null) {
					OnCollideEarth(this);
				}
			}
		}

		public void OnSeperate(IEntity other) {
			
		}
	}
}
