using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using OpenTK;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;

namespace PokemonRumble {
	public class BattleArena : ContactListener, IEntity {
		protected AABB _worldAABB;
		public World _world;

		private static Texture Dirt = new Texture(@"Data\Dirt.JPG");
		private static Texture Grass = new Texture(@"Data\Grass 00 seamless.jpg");

		public BattleArena() {
			_worldAABB = new AABB();
			_worldAABB.LowerBound.Set(-200.0f, -100.0f);
			_worldAABB.UpperBound.Set(200.0f, 200.0f);
			Vec2 gravity = new Vec2();
			gravity.Set(0.0f, -10.0f);
			bool doSleep = true;
			_world = new World(_worldAABB, gravity, doSleep);

			BodyDef groundBodyDef = new BodyDef();
			groundBodyDef.Position.Set(0.0f, -10.0f);

			Body groundBody = _world.CreateBody(groundBodyDef);

			// Define the ground box shape.
			PolygonDef groundShapeDef = new PolygonDef();

			// The extents are the half-widths of the box.
			groundShapeDef.SetAsBox(50.0f, 10.0f);

			// Add the ground shape to the ground body.
			Fixture fix = groundBody.CreateFixture(groundShapeDef);
			fix.UserData = this;

			DebugDraw draw = new OpenTKDebugDraw();
			draw.Flags = DebugDraw.DrawFlags.Shape;
			if (Program.DebugDraw) {
				_world.SetDebugDraw(draw);
			}

			_world.SetContactListener(this);
		}

		internal void Update(float dt) {
			_world.Step(dt, 10, 10);
			_world.Validate();
		}

		public void Draw() {
			for (int z = -4; z <= 1; z++) {
				for (int x = -6; x < 6; x++) {
					int scale = 10;
					int left = x * scale;
					int right = (x + 1) * scale;
					int bottom = z * scale;
					int top = (z + 1) * scale;
					GraphicsManager.SetTexture(Grass);
					GraphicsManager.DrawQuad(new Vector3d(left, 0, bottom),
											 new Vector3d(left, 0, top),
											 new Vector3d(right, 0, top),
											 new Vector3d(right, 0, bottom),
											 new Vector2d(5, 5));
				}
			}

			GraphicsManager.SetTexture(Dirt);
			GraphicsManager.DrawQuad(new Vector3d(-10, 0.0001, -5),
									 new Vector3d(-10, 0.0001, 5),
									 new Vector3d(10, 0.0001, 5),
									 new Vector3d(10, 0.0001, -5),
									 new Vector2d(2, 4));
		}

		public void OnCollides(IEntity other) {
			//eh
		}

		public void OnSeperate(IEntity other) {
			//eh
		}

		public void BeginContact(Contact contact) {
			if (contact.FixtureA.UserData is IEntity && contact.FixtureB.UserData is IEntity) {
				IEntity A = (IEntity)contact.FixtureA.UserData;
				IEntity B = (IEntity)contact.FixtureB.UserData;

				A.OnCollides(B);
				B.OnCollides(A);
			}
		}

		public void EndContact(Contact contact) {
			if (contact.FixtureA.UserData is IEntity && contact.FixtureB.UserData is IEntity) {
				IEntity A = (IEntity)contact.FixtureA.UserData;
				IEntity B = (IEntity)contact.FixtureB.UserData;

				A.OnSeperate(B);
				B.OnSeperate(A);
			}
		}

		public void PostSolve(Contact contact, ContactImpulse impulse) {
			//eh
		}

		public void PreSolve(Contact contact, Manifold oldManifold) {
			//eh
		}
	}
}
