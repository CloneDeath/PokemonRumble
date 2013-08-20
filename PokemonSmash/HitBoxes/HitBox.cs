using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using GLImp;
using Box2DX.Dynamics;

namespace PokemonSmash.HitBoxes {
	abstract public class HitBox : IEntity {
		public Player Creator;
		public float Duration;
		public bool Permanent;
		public float AnimationXOffset;
		public float AnimationYOffset;
		public float Z {
			set {
				anim.Z = value;
			}
		}
		public float Scale {
			set {
				anim.Scale = value;
			}
		}

		protected bool Dead;
		protected Animation anim;
		protected Fixture fixture;

		private Stopwatch Update = new Stopwatch();
		private float time = 0;

		public Action<HitBox, Player> OnCollidePlayer;
		public Action<HitBox> OnCollideEarth;
		public Action<HitBox, float> OnUpdate;
		public Action<HitBox> OnDestroy;

		public HitBox(Player Creator, float x, float y) {
			this.AnimationXOffset = x;
			this.AnimationYOffset = y;

			this.Creator = Creator;
			this.Duration = 1.0f;
			this.Dead = false;
			this.Permanent = false;

			Update.Start();
			GraphicsManager.Update += GraphicsManager_Update;
		}

		public bool IsAlive {
			get {
				if (Dead) return false; //Dead flag is king.
				if (Permanent) return true; //And permanency his wife
				return Duration > 0; //Finally we just ask the peasants.
			}
		}

		public void Unload() {
			this.Dead = true;
		}

		void GraphicsManager_Update() {
			float dt = Update.ElapsedMilliseconds / 1000.0f;
			Update.Restart();

			time += dt;
			if (OnUpdate != null) {
				OnUpdate(this, time);
			}

			if (anim != null) {
				anim.skeleton.X = fixture.Body.GetPosition().X + AnimationXOffset;
				anim.skeleton.Y = fixture.Body.GetPosition().Y + AnimationYOffset;
			}

			Duration -= dt;
			if (!IsAlive) {
				fixture.Body.DestroyFixture(fixture);
				if (anim != null) {
					anim.Unload();
				}
				GraphicsManager.Update -= GraphicsManager_Update;

				if (OnDestroy != null) {
					OnDestroy(this);
				}
			}
		}

		public void SetSkeleton(string name) {
			anim = new Animation(name);
			anim.skeleton.X = fixture.Body.GetPosition().X;
			anim.skeleton.Y = fixture.Body.GetPosition().Y;
		}

		public void SetAnimation(string animation, bool loop) {
			anim.state.SetAnimation(animation, loop);
		}

		public void FlipAnimation(bool Flip = true) {
			anim.skeleton.FlipX = Flip;
		}

		public ushort CollisionMask {
			get {
				return fixture.Filter.MaskBits;
			}
			set {
				fixture.Filter.MaskBits = value;
			}
		}

		#region IEntity
		public void OnCollides(IEntity other) {
			if (other is Player && other != Creator) {
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
		#endregion
	}
}
