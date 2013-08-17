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

namespace PokemonRumble {
	class Player {
		Body body;
		Animation anim;
		string PokemonName;
		BattleArena World;

		public Pokemon Pokemon {
			get {
				return PokemonManager.Find(PokemonName);
			}
		}

		public Player(BattleArena arena) {
			this.World = arena;
			InitPhysics(arena);
			PokemonName = "bulbasaur";
			anim = new Animation("Pokemon/" + PokemonName);
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
			shapeDef.SetAsBox(0.4f, 0.25f);

			// Set the box density to be non-zero, so it will be dynamic.
			shapeDef.Density = 1.0f;

			// Override the default friction.
			//shapeDef.Friction = 2f;

			// Add the shape to the body.
			body.CreateFixture(shapeDef);

			// Now tell the dynamic body to compute it's mass properties base
			// on its shape.
			body.SetMassFromShapes();

			body.SetFixedRotation(true);
		}

		internal void Update(float dt) {
			float speed = Pokemon.Speed;
			if (Controls.IsDown(Control.Left)) {
				if (anim.state.Animation.Name != "walk") {
					anim.state.SetAnimation("walk", true);
				}
				anim.skeleton.FlipX = false;
				body.ApplyForce(new Vec2(-speed - body.GetLinearVelocity().X, 0), new Vec2(.1f, .1f));
			} else if (Controls.IsDown(Control.Right)) {
				if (anim.state.Animation.Name != "walk") {
					anim.state.SetAnimation("walk", true);
				}
				anim.skeleton.FlipX = true;
				body.ApplyForce(new Vec2(speed - body.GetLinearVelocity().X, 0), new Vec2(.1f, .1f));
			} else {
				if (anim.state.Animation.Name != "idle") {
					anim.state.SetAnimation("idle", true);
				}
				body.ApplyForce(new Vec2(-body.GetLinearVelocity().X * speed, 0), new Vec2(.1f, .1f));
			}

			if (Controls.IsPressed(Control.Jump) && System.Math.Abs(Accel.Y) == 0){
				body.ApplyForce(new Vec2(0, 100), new Vec2(.1f, .1f));
			}
		}

		Vec2 Accel {
			get {
				return CurrentVelocity - PreviousVelocity;
			}
		}

		Vec2 PreviousVelocity = new Vec2();
		Vec2 CurrentVelocity = new Vec2();
		public void Draw(float dt) {
			anim.state.Update(dt);

			anim.skeleton.X = (float)this.X;
			anim.skeleton.Y = (float)this.Y - 0.15f;

			anim.state.Apply(anim.skeleton);
			anim.skeleton.RootBone.ScaleX = 1 / 50f;
			anim.skeleton.RootBone.ScaleY = 1 / 50f;
			anim.skeleton.UpdateWorldTransform();
			anim.skeletonRenderer.Draw(anim.skeleton);

			GraphicsManager.DrawQuad(new Vector3d(anim.skeleton.X - 0.4, 0.002, -0.3),
					new Vector3d(anim.skeleton.X + 0.4, 0.002, -0.3),
					new Vector3d(anim.skeleton.X + 0.4, 0.002, 0.3),
					new Vector3d(anim.skeleton.X - 0.4, 0.002, 0.3),
					ResourceManager.Shadow);

			PreviousVelocity = CurrentVelocity;
			CurrentVelocity = body.GetLinearVelocity();
		}

		
	}
}
