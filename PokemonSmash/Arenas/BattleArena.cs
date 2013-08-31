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
	abstract public class BattleArena : ContactListener, IEntity
    {
		protected AABB worldAABB = default(AABB);
        public World world = null;
        protected Body groundBody = null;
        protected Vector2 gravity = new Vector2(0, -10.0f);
        protected Vector2 dimensions = new Vector2(60, 40);
        protected float boundaryThickness = 10;


        // The AABB will go outside of the "playable area" by this amount.
        protected const float edgeTolerance = 20.0f;

        public BattleArena() { }

        /// <summary>
        /// Called to setup the arena boundaries and bind to all of the physics stuff.
        /// </summary>
        public virtual void Bind()
        {
            float width = dimensions.X, height = dimensions.Y;

            worldAABB = new AABB();
            worldAABB.LowerBound.Set(-width / 2 - edgeTolerance, -edgeTolerance);
            worldAABB.UpperBound.Set(width / 2 + edgeTolerance, height + edgeTolerance);

            Vec2 gravity = new Vec2(this.gravity.X, this.gravity.Y);
            bool doSleep = true;
            world = new World(worldAABB, gravity, doSleep);
            world.SetContactFilter(new ContactFilter());

            BodyDef groundBodyDef = new BodyDef();
            groundBodyDef.Position.Set(0.0f, 0.0f);

            Body groundBody = world.CreateBody(groundBodyDef);

            // Bottom
            AddBoundaryBlock(groundBody, 0, -(boundaryThickness / 2), width + boundaryThickness * 2, boundaryThickness);
            // Top
            AddBoundaryBlock(groundBody, 0, height + boundaryThickness / 2, width + boundaryThickness * 2, boundaryThickness);
            // Left
            AddBoundaryBlock(groundBody, -(width / 2) - boundaryThickness / 2, height / 2, boundaryThickness, height + boundaryThickness * 2);
            // Right
            AddBoundaryBlock(groundBody, +(width / 2) + boundaryThickness / 2, height / 2, boundaryThickness, height + boundaryThickness * 2);

            DebugDraw draw = new OpenTKDebugDraw();
            draw.Flags = DebugDraw.DrawFlags.Shape;
            if (Program.DebugDraw)
            {
                world.SetDebugDraw(draw);
            }

            world.SetContactListener(this);


            // Old code for reference

            /*
             * 
             
             * _worldAABB = new AABB();
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
             */
        }

		private void AddBoundaryBlock(Body groundBody, float x, float y, float width, float height) {
			// Define the ground box shape.
			PolygonDef groundShapeDef = new PolygonDef();
			groundShapeDef.SetAsBox(width/2, height/2, new Vec2(x, y), 0);

			// Add the ground shape to the ground body.
			Fixture fix = groundBody.CreateFixture(groundShapeDef);
            
			fix.UserData = this;
			fix.Filter.CategoryBits = 0x0001;
		}


		internal void Update(float dt)
        {
			world.Step(dt, 10, 10);
			world.Validate();
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

        // -------------------------- EVENTS IMPLEMENTED BY SUBCLASSES --------------------------
        #region ChildEvents

        public abstract void DrawBehind();

        public abstract void DrawFront();

        #endregion

        // -------------------------- UNIMPLEMENTED THINGS ----------------------------------
        #region Unimplemented

        public void OnCollides(IEntity other)
        {
            //eh
        }

        public void OnSeperate(IEntity other)
        {
            //eh
        }

		public void PostSolve(Contact contact, ContactImpulse impulse) {
			//eh
		}

		public void PreSolve(Contact contact, Manifold oldManifold) {
			//eh
        }

        #endregion
    }
}
