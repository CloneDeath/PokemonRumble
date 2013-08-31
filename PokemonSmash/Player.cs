using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using PokemonSmash.Input;
using Box2DX.Common;
using OpenTK;
using GLImp;
using Box2DX.Collision;
using PokemonSmash.HitBoxes;

namespace PokemonSmash {
	public class Player : IEntity {
		public Body body;
		Animation anim;
		string PokemonName;
		BattleArena World;
		bool Dead = false;
		List<StatusEffect> Effects = new List<StatusEffect>();
		bool Asleep = false;

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

		public Player(BattleArena arena, ControlSet Controls, int ID, PlayerDef definition) {
			this.World = arena;
			PokemonName = definition.Pokemon.Name;
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
				Move[i] = new MoveInstance(definition.Moves[i]);
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
			body = arena.world.CreateBody(bodyDef);

			// Define another box shape for our dynamic body.
			PolygonDef shapeDef = new PolygonDef();
			shapeDef.SetAsBox(Pokemon.Width / 2, Pokemon.Height / 2);

			// Set the box density to be non-zero, so it will be dynamic.
			shapeDef.Density = 1.0f;

			// Override the default friction.
			//shapeDef.Friction = 2f;

			// Add the shape to the body.
			Fixture fix = body.CreateFixture(shapeDef);
			fix.Density = 1;
			fix.Friction = 0.3f;
			fix.Restitution = 0.1f;
			fix.UserData = this;
			fix.Filter.GroupIndex = this.ID;
			fix.Filter.CategoryBits = 0x0002;
			if (Pokemon.Types.Contains(PokemonType.Ghost)){
				fix.Filter.MaskBits = 0xFFFF - 0x0002;
			} else {
				fix.Filter.MaskBits = 0xFFFF;
			}

			// Now tell the dynamic body to compute it's mass properties base
			// on its shape.
			var mass = body.GetMassData();
			mass.Mass = Pokemon.Weight;
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

			if (Asleep) {
				if (anim.state.Animation.Name != "dead") {
					anim.state.SetAnimation("dead", false);
				}
				body.ApplyForce(new Vec2(-body.GetLinearVelocity().X, 0), new Vec2(.1f, .1f));
				return;
			}

			DisableTime -= dt;
			float MoveSpeed = (float)System.Math.Sqrt(Speed + 50);

			if (Pokemon.Hovers) {
				body.ApplyForce(-body.GetWorld().Gravity * Pokemon.Weight);
			}
			if (Pokemon.Hovers && !Disabled) {
				Vec2 Desire = new Vec2();
				if (Controls.IsDown(Control.Left)) {
					Desire.X -= 1;
					Direction = -1;
				}

				if (Controls.IsDown(Control.Right)) {
					Desire.X += 1;
					Direction = 1;
				}

				if (Controls.IsDown(Control.Up)) {
					Desire.Y += 1;
				}

				if (Controls.IsDown(Control.Down)) {
					Desire.Y -= 1;
				}

				var speed = body.GetLinearVelocity();

				if (Desire.X == 0 && Desire.Y == 0) {
					body.ApplyForce(-speed * 0.4f);
				} else {
					body.ApplyForce((Desire * MoveSpeed) - speed);
				}
			}

			if (!Pokemon.Hovers) {
				float FrictionModifier = (OnGround || Pokemon.CanFly) ? 1 : 0.5f;
				if (Controls.IsDown(Control.Left) && !Disabled) {
					if (anim.state.Animation.Name != "walk") {
						anim.state.SetAnimation("walk", true);
					}
					//body.SetLinearVelocity(new Vec2(vel.X * 1.1f, vel.Y));
					body.ApplyForce(new Vec2(-(MoveSpeed * FrictionModifier) - (body.GetLinearVelocity().X * 2), 0));
					Direction = -1;
				} else if (Controls.IsDown(Control.Right) && !Disabled) {
					if (anim.state.Animation.Name != "walk") {
						anim.state.SetAnimation("walk", true);
					}
					//body.SetLinearVelocity(new Vec2(vel.X * 0.9f, vel.Y));
					body.ApplyForce(new Vec2((MoveSpeed * FrictionModifier) - (body.GetLinearVelocity().X * 2), 0));
					Direction = 1;
				} else if (OnGround) {
					if (!Disabled) {
						if (anim.state.Animation.Name != "idle") {
							anim.state.SetAnimation("idle", true);
						}
					}

					body.ApplyForce(new Vec2(-body.GetLinearVelocity().X * MoveSpeed * 3, 0), new Vec2(.1f, .1f));
				}

				if (Controls.IsPressed(Control.Jump) && !Disabled && Pokemon.CanJump && (OnGround || Pokemon.CanFly)) {
					if (anim.state.Animation.Name != "jump") {
						anim.state.SetAnimation("jump", false);
					}
					Vec2 Vel = body.GetLinearVelocity();
					if (OnGround) {
						body.ApplyImpulse(new Vec2(Vel.X, 3 * MoveSpeed));
					} else {
						body.ApplyImpulse(new Vec2(Vel.X, MoveSpeed));
					}
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

			foreach (StatusEffect effect in Effects) {
				effect.Update(this, dt);
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

			if (Pokemon.CastsShadow) {
				GraphicsManager.DrawQuad(new Vector3d(anim.skeleton.X - 0.4, 0.002, -0.3),
						new Vector3d(anim.skeleton.X + 0.4, 0.002, -0.3),
						new Vector3d(anim.skeleton.X + 0.4, 0.002, 0.3),
						new Vector3d(anim.skeleton.X - 0.4, 0.002, 0.3),
						ResourceManager.Shadow);
			}

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

		internal void Unload() {
			anim.Unload();
		}

		

		public void AddEffect(StatusEffect effect)
		{
			Effects.Add(effect);
			effect.Initialize(this);
		}

		internal void RemoveEffect(StatusEffect effect)
		{
			Effects.Remove(effect);
			effect.Uninitialize(this);
		}

		internal void Sleep()
		{
			Asleep = true;
		}

		internal void Awake()
		{
			Asleep = false;
		}
	}
}
