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
			//_world.SetWarmStarting(settings.enableWarmStarting > 0);
			//_world.SetContinuousPhysics(settings.enableTOI > 0);

			//_pointCount = 0;

			_world.Step(dt, 10, 10);

			_world.Validate();

			//if (_bomb != null && _bomb.IsFrozen()) {
			//    _world.DestroyBody(_bomb);
			//    _bomb = null;
			//}

			//if (settings.drawStats != 0) {
			//    OpenGLDebugDraw.DrawString(5, _textLine, String.Format("proxies(max) = {0}({1}), pairs(max) = {2}({3})",
			//        new object[]{_world.GetProxyCount(), Box2DX.Common.Settings.MaxProxies,
			//            _world.GetPairCount(), Box2DX.Common.Settings.MaxProxies}));
			//    _textLine += 15;

			//    OpenGLDebugDraw.DrawString(5, _textLine, String.Format("bodies/contacts/joints = {0}/{1}/{2}",
			//        new object[] { _world.GetBodyCount(), _world.GetContactCount(), _world.GetJointCount() }));
			//    _textLine += 15;
			//}

			//if (_mouseJoint != null) {
			//    Body body = _mouseJoint.GetBody2();
			//    Vec2 p1 = body.GetWorldPoint(_mouseJoint._localAnchor);
			//    Vec2 p2 = _mouseJoint._target;

			//    Gl.glPointSize(4.0f);
			//    Gl.glColor3f(0.0f, 1.0f, 0.0f);
			//    Gl.glBegin(Gl.GL_POINTS);
			//    Gl.glVertex2f(p1.X, p1.Y);
			//    Gl.glVertex2f(p2.X, p2.Y);
			//    Gl.glEnd();
			//    Gl.glPointSize(1.0f);

			//    Gl.glColor3f(0.8f, 0.8f, 0.8f);
			//    Gl.glBegin(Gl.GL_LINES);
			//    Gl.glVertex2f(p1.X, p1.Y);
			//    Gl.glVertex2f(p2.X, p2.Y);
			//    Gl.glEnd();
			//}

			//if (settings.drawContactPoints != 0) {
			//    //float k_forceScale = 0.01f;
			//    float k_axisScale = 0.3f;

			//    for (int i = 0; i < _pointCount; ++i) {
			//        MyContactPoint point = _points[i];

			//        if (point.state == ContactState.ContactAdded) {
			//            // Add
			//            OpenGLDebugDraw.DrawPoint(point.position, 10.0f, new Color(0.3f, 0.95f, 0.3f));
			//        } else if (point.state == ContactState.ContactPersisted) {
			//            // Persist
			//            OpenGLDebugDraw.DrawPoint(point.position, 5.0f, new Color(0.3f, 0.3f, 0.95f));
			//        } else {
			//            // Remove
			//            OpenGLDebugDraw.DrawPoint(point.position, 10.0f, new Color(0.95f, 0.3f, 0.3f));
			//        }

			//        if (settings.drawContactNormals == 1) {
			//            Vec2 p1 = point.position;
			//            Vec2 p2 = p1 + k_axisScale * point.normal;
			//            OpenGLDebugDraw.DrawSegment(p1, p2, new Color(0.4f, 0.9f, 0.4f));
			//        } else if (settings.drawContactForces == 1) {
			//            /*Vector2 p1 = point.position;
			//            Vector2 p2 = p1 + k_forceScale * point.normalForce * point.normal;
			//            OpenGLDebugDraw.DrawSegment(p1, p2, new Color(0.9f, 0.9f, 0.3f));*/
			//        }

			//        if (settings.drawFrictionForces == 1) {
			//            /*Vector2 tangent = Vector2.Cross(point.normal, 1.0f);
			//            Vector2 p1 = point.position;
			//            Vector2 p2 = p1 + k_forceScale * point.tangentForce * tangent;
			//            OpenGLDebugDraw.DrawSegment(p1, p2, new Color(0.9f, 0.9f, 0.3f));*/
			//        }
			//    }
			//}
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
