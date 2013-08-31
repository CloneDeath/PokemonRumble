using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using OpenTK;

namespace PokemonSmash.Arenas.Data
{
    /// <summary>
    /// Represents a simple textured block that may or may have collision.
    /// </summary>
    public class BlockEntity
    {
        /// <summary>
        /// A list of collision layers and masks.
        /// </summary>
        public enum CollisionLayer : ushort
        {
            World = 1 << 0,
            Player = 1 << 1,
            Projectile = 1 << 2
        }

        /// <summary>
        /// The fixture used for collision.
        /// </summary>
        public Fixture Fixture
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not collision is enabled for this entity.
        /// </summary>
        public bool CollisionEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// The position of this block's center.
        /// </summary>
        public Vector3 Position
        {
            get;
            set;
        }

        /// <summary>
        /// The total width and height of this block.
        /// </summary>
        public Vector2 Dimensions
        {
            get;
            set;
        }

        /// <summary>
        /// The total width of this block.
        /// </summary>
        public float Width
        {
            get { return Dimensions.X; }
            set
            {
                Dimensions = new Vector2(value, Dimensions.Y);
            }
        }

        /// <summary>
        /// The total height of this block.
        /// </summary>
        public float Height
        {
            get { return Dimensions.Y; }
            set
            {
                Dimensions = new Vector2(Dimensions.X, value);
            }
        }

        /// <summary>
        /// The layer on which this block's collision takes place.
        /// </summary>
        public CollisionLayer Layer
        {
            get;
            set;
        }


        public BlockEntity()
        {
            Dimensions = new Vector2(0, 0);
            Position = new Vector3(0, 0, 0);
            CollisionEnabled = false;
            Layer = CollisionLayer.World;
        }

        /// <summary>
        /// Called to register this block with the collision contexts, etc.
        /// </summary>
        /// <param name="arena"></param>
        public void Bind(LoadableBattleArena arena)
        {
            // Define the ground box shape.
            PolygonDef groundShapeDef = new PolygonDef();
            groundShapeDef.SetAsBox(Width / 2, Height / 2, new Box2DX.Common.Vec2(Position.X, Position.Y), 0);

            // Add the ground shape to the ground body.
            Fixture fix = arena.GroundBody.CreateFixture(groundShapeDef);

            fix.UserData = arena;
            fix.Filter.CategoryBits = (ushort)Layer;
        }
    }
}
