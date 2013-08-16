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
	class BattleArena {
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
			groundBody.CreateFixture(groundShapeDef);

			DebugDraw draw = new OpenTKDebugDraw();
			draw.Flags = DebugDraw.DrawFlags.Aabb | DebugDraw.DrawFlags.Shape;
			_world.SetDebugDraw(draw);
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
			GraphicsManager.DrawQuad(new Vector3d(-10, 0.00001, -5),
									 new Vector3d(-10, 0.00001, 5),
									 new Vector3d(10, 0.00001, 5),
									 new Vector3d(10, 0.00001, -5),
									 new Vector2d(2, 4));
		}
	}
}
