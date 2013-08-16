using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using PokemonRumble.Input;
using Box2DX.Common;

namespace PokemonRumble {
	class Player {
		Body body;
		public Player(BattleArena arena) {
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
			float speed = 2.0f;
			if (Controls.IsDown(Control.Left)) {
				if (ResourceManager.state.Animation.Name != "walk") {
					ResourceManager.state.SetAnimation("walk", true);
				}
				ResourceManager.skeleton.X -= speed * dt;
				ResourceManager.skeleton.FlipX = false;
				body.ApplyForce(new Vec2(-speed - body.GetLinearVelocity().X, 0), new Vec2(.1f, .1f));
			} else if (Controls.IsDown(Control.Right)) {
				if (ResourceManager.state.Animation.Name != "walk") {
					ResourceManager.state.SetAnimation("walk", true);
				}
				ResourceManager.skeleton.X += speed * dt;
				ResourceManager.skeleton.FlipX = true;
				body.ApplyForce(new Vec2(speed - body.GetLinearVelocity().X, 0), new Vec2(.1f, .1f));
			} else {
				if (ResourceManager.state.Animation.Name != "idle") {
					ResourceManager.state.SetAnimation("idle", true);
				}
				body.ApplyForce(new Vec2(-body.GetLinearVelocity().X * speed, 0), new Vec2(.1f, .1f));
			}

			if (Controls.IsPressed(Control.Up)){
				body.ApplyForce(new Vec2(0, 100), new Vec2(.1f, .1f));
			}

		}
	}
}
