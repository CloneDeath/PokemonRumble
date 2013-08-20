using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using OpenTK;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;

namespace PokemonSmash {
	abstract public class BattleArena : ContactListener, IEntity {
		protected AABB _worldAABB;
		public World _world;

		public BattleArena() {
			_worldAABB = new AABB();
			_worldAABB.LowerBound.Set(-30.0f, -20.0f);
			_worldAABB.UpperBound.Set(30.0f, 40.0f);
			Vec2 gravity = new Vec2();
			gravity.Set(0.0f, -10.0f);
			bool doSleep = true;
			_world = new World(_worldAABB, gravity, doSleep);
			_world.SetContactFilter(new ContactFilter());

			BodyDef groundBodyDef = new BodyDef();
			groundBodyDef.Position.Set(0.0f, 0.0f);

			Body groundBody = _world.CreateBody(groundBodyDef);

			AddBlock(groundBody, 0, -5, 40, 10);
			AddBlock(groundBody, -20, 20, 1, 40);
			AddBlock(groundBody, 20, 20, 1, 40);
			AddBlock(groundBody, 0, 30, 40, 10);

			DebugDraw draw = new OpenTKDebugDraw();
			draw.Flags = DebugDraw.DrawFlags.Shape;
			if (Program.DebugDraw) {
				_world.SetDebugDraw(draw);
			}

			_world.SetContactListener(this);
		}

		private void AddBlock(Body groundBody, float x, float y, float width, float height) {
			// Define the ground box shape.
			PolygonDef groundShapeDef = new PolygonDef();
			groundShapeDef.SetAsBox(width/2, height/2, new Vec2(x, y), 0);

			// Add the ground shape to the ground body.
			Fixture fix = groundBody.CreateFixture(groundShapeDef);
			fix.UserData = this;
			fix.Filter.CategoryBits = 0x0001;
		}


		internal void Update(float dt) {
			_world.Step(dt, 10, 10);
			_world.Validate();
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

		public abstract void DrawBehind();

		public abstract void DrawFront();
	}
}
