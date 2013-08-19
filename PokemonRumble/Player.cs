using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using PokemonRumble.Input;
using Box2DX.Common;
using OpenTK;
using GLImp;
using Box2DX.Collision;
using PokemonRumble.HitBoxes;

namespace PokemonRumble {
	public class Player : IEntity {
		public Body body;
		Animation anim;
		string PokemonName;
		BattleArena World;
		bool Dead = false;

		private float _HP = 100;
		public float HP {
			get { return _HP; }
			set {
				if (value > Pokemon.HP) {
					value = Pokemon.HP;
				}
				_HP = value;
			}
		}
		public float Attack;
		public float Defense;
		public float SpecialAttack;
		public float SpecialDefense;
		public float Speed;

		float DisableTime = 0;

		private int _direction = -1;

		public int Direction {
			get {
				return _direction;
			}
			set {
				_direction = value;
				if (_direction < 0) {
					anim.skeleton.FlipX = false;
				} else {
					anim.skeleton.FlipX = true;
				}
			}
		}

		public Pokemon Pokemon {
			get {
				return PokemonManager.Find(PokemonName);
			}
		}

		MoveInstance[] Move = new MoveInstance[4];

		ControlSet Controls;

		public short ID;

		public Player(BattleArena arena, ControlSet Controls, int ID, string pokemon) {
			this.World = arena;
			PokemonName = pokemon;
			anim = new Animation(Pokemon.Animation);
			anim.state.SetAnimation("idle", true);
			foreach (MixItem item in Pokemon.MixQueue) {
				anim.stateData.SetMix(item.From, item.To, item.Time);
			}
			this.Controls = Controls;
			this.HP = Pokemon.HP;
			this.Attack = Pokemon.Attack;
			this.Defense = Pokemon.Defense;
			this.SpecialAttack = Pokemon.SpecialAttack;
			this.SpecialDefense = Pokemon.SpecialDefense;
			this.Speed = Pokemon.Speed;
			this.ID = (short)ID;

			for (int i = 0; i < 4; i++) {
				Move[i] = new MoveInstance(Pokemon.Move[i]);
			}

			InitPhysics(arena);
		}

		public double X {
			get {
				return body.GetPosition().X;
			}
		}

		public double Y {
			get {
				return body.GetPosition().Y;
			}
		}

		private void InitPhysics(BattleArena arena) {
			// Define the dynamic body. We set its position and call the body factory.
			BodyDef bodyDef = new BodyDef();
			bodyDef.Position.Set(0.0f, 4.0f);
			body = arena._world.CreateBody(bodyDef);

			// Define another box shape for our dynamic body.
			PolygonDef shapeDef = new PolygonDef();
			shapeDef.SetAsBox(Pokemon.Width / 2, Pokemon.Height / 2);

			// Set the box density to be non-zero, so it will be dynamic.
			shapeDef.Density = 1.0f;

			// Override the default friction.
			//shapeDef.Friction = 2f;

			// Add the shape to the body.
			Fixture fix = body.CreateFixture(shapeDef);
			fix.UserData = this;
			fix.Filter.GroupIndex = this.ID;
			fix.Filter.CategoryBits = 0x0002;
			fix.Filter.MaskBits = 0xFFFF;

			// Now tell the dynamic body to compute it's mass properties base
			// on its shape.
			body.SetMassFromShapes();
			var mass = body.GetMassData();
			mass.Mass = Pokemon.Weight / 10;
			body.SetMass(mass);

			body.SetFixedRotation(true);
		}

		internal void SetPosition(float X, float Y) {
			body.SetPosition(new Vec2(X, Y));
		}

		public bool OnGround {
			get {
				return Accel.Y == 0;
			}
		}

		public bool Disabled {
			get {
				return DisableTime > 0;
			}
		}

		int CurrentMove = 0;

		public float Cooldown {
			set {
				Move[CurrentMove].Cooldown = value;
			}
		}

		internal void Update(float dt) {
			if (Dead) {
				if (anim.state.Animation.Name != "dead") {
					anim.state.SetAnimation("dead", false);
				}
				this.HP = 0;
				body.ApplyForce(new Vec2(-body.GetLinearVelocity().X, 0), new Vec2(.1f, .1f));
				return;
			}

			DisableTime -= dt;

			if (OnGround) {
				if (Controls.IsDown(Control.Left) && !Disabled) {
					if (anim.state.Animation.Name != "walk") {
						anim.state.SetAnimation("walk", true);
					}
					body.ApplyForce(new Vec2(-(Speed/10) - body.GetLinearVelocity().X, 0), new Vec2(.1f, .1f));
					Direction = -1;
				} else if (Controls.IsDown(Control.Right) && !Disabled) {
					if (anim.state.Animation.Name != "walk") {
						anim.state.SetAnimation("walk", true);
					}
					body.ApplyForce(new Vec2((Speed/10) - body.GetLinearVelocity().X, 0), new Vec2(.1f, .1f));
					Direction = 1;
				} else {
					if (!Disabled) {
						if (anim.state.Animation.Name != "idle") {
							anim.state.SetAnimation("idle", true);
						}
					}
					body.ApplyForce(new Vec2(-body.GetLinearVelocity().X * (Speed), 0), new Vec2(.1f, .1f));
				}

				if (Controls.IsPressed(Control.Jump) && !Disabled) {
					if (anim.state.Animation.Name != "jump") {
						anim.state.SetAnimation("jump", false);
					}
					Vec2 Vel = body.GetLinearVelocity();
					body.ApplyForce(new Vec2(Vel.X, Speed * 5), new Vec2(.1f, .1f));
				}
			}


			if (!Disabled) {
				if (Controls.IsPressed(Control.Move0)) {
					CurrentMove = 0;
					Move[0].OnUse(this);
				}

				if (Controls.IsPressed(Control.Move1)) {
					CurrentMove = 1;
					Move[1].OnUse(this);
				}

				if (Controls.IsPressed(Control.Move2)) {
					CurrentMove = 2;
					Move[2].OnUse(this);
				}

				if (Controls.IsPressed(Control.Move3)) {
					CurrentMove = 3;
					Move[3].OnUse(this);
				}
			}
		}

		public void SetAnimation(string AnimationName, bool Loop) {
			anim.state.SetAnimation(Pokemon.FilterAnimationAlias(AnimationName), Loop);
		}

		public void TakeDamage(float Amount, Player other) {
			this.HP -= Amount * (other.Attack / this.Defense);
		}

		public void TakeSpecialDamage(float Amount, Player other) {
			this.HP -= Amount * (other.SpecialAttack / this.SpecialDefense);
		}

		public void Disable(float time) {
			DisableTime = time;
		}

		public void SetVelocity(float XVel, float YVel) {
			body.SetLinearVelocity(new Vec2(XVel, YVel));
		}

		public DamageBox AddDamageBox(float x, float y, float width, float height) {
			return new DamageBox(this, x, y, width, height);
		}

		public Projectile AddProjectile(float x, float y, float width, float height) {
			return new Projectile(this, x, y, width, height);
		}

		Vec2 Accel {
			get {
				return CurrentVelocity - PreviousVelocity;
			}
		}

		Vec2 PreviousVelocity = new Vec2();
		Vec2 CurrentVelocity = new Vec2();
		public void Draw(float dt) {
			anim.skeleton.X = (float)this.X;
			anim.skeleton.Y = (float)this.Y - (Pokemon.Height / 2);

			GraphicsManager.DrawQuad(new Vector3d(anim.skeleton.X - 0.4, 0.002, -0.3),
					new Vector3d(anim.skeleton.X + 0.4, 0.002, -0.3),
					new Vector3d(anim.skeleton.X + 0.4, 0.002, 0.3),
					new Vector3d(anim.skeleton.X - 0.4, 0.002, 0.3),
					ResourceManager.Shadow);

			anim.Draw();

			PreviousVelocity = CurrentVelocity;
			CurrentVelocity = body.GetLinearVelocity();
		}

		public void OnCollides(IEntity other) {
			
		}

		public void OnSeperate(IEntity other) {
			
		}

		internal void Kill() {
			this.Dead = true;
		}
	}
}
