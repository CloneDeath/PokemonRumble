using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using GLImp;
using PokemonSmash.Arenas.Data;
using OpenTK;

namespace PokemonSmash.Arenas
{
    /// <summary>
    /// An implementation of BattleArena that is designed to be loaded from a python file.
    /// </summary>
    public class LoadableBattleArena : BattleArena
    {
        // ----------------- STATIC THINGS AND PYTHON THINGS ----------------------
        #region StaticAndPythonThings

        /// <summary>
        /// A list of all arenas loaded into the game.
        /// Each new arena loaded from a file gets added here.
        /// </summary>
        public static List<LoadableBattleArena> Arenas
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes the static stuff.
        /// </summary>
        static LoadableBattleArena()
        {
            Arenas = new List<LoadableBattleArena>();
        }

        /// <summary>
        /// Generates a new arena with default settings.
        /// </summary>
        /// <param name="width">The width of the arena, in game units.</param>
        /// <param name="height">The height of the arena, in game units/</param>
        /// <returns>A new arena object, already added to the managed list.</returns>
        public static LoadableBattleArena Create(float width, float height)
        {
            LoadableBattleArena arena = new LoadableBattleArena(width, height, 40);
            Arenas.Add(arena);
            return arena;
        }

        /// <summary>
        /// Loads a texture and adds it to this arena's list of used/referenced textures.
        /// These textures are then used to draw blocks.
        /// </summary>
        /// <param name="name">The name that will be used to bind a block entity to this texture.</param>
        /// <param name="path">The path at which to locate the texture (GLIMP-convention).</param>
        /// <param name="linearInterpolate">True to enable linear interpolating for this texture, false otherwise.</param>
        /// <returns>true if the operation completed successfully, false otherwise.</returns>
        public bool LoadTexture(string name, string path, bool linearInterpolate)
        {
            Texture tex;
            
            try
            {
                tex = new Texture(path, linearInterpolate);
            }
            catch(Exception ex)
            {
                Console.WriteLine("LoadableBattleArena.LoadTexture: Failed to load Texture \"" + name + "\" from path \"" + path + "\": " + ex.Message);
                return false;
            }

            Textures.Add(name, tex);
            return true;
        }

        /// <summary>
        /// Returns a previously loaded texture from the list of textures this arena uses.
        /// </summary>
        /// <param name="name">The name of the texture to retrieve.</param>
        /// <returns>The texture, or null if none was loaded by that name.</returns>
        public Texture GetTexture(string name)
        {
            if (Textures.ContainsKey(name))
                return Textures[name];
            else
                return null;
        }

        /// <summary>
        /// Adds a new BlockEntity to this arena (intended to be called from Python).
        /// </summary>
        /// <param name="textureName">The name of the texture that will be used to draw this BlockEntity.</param>
        /// <param name="x">The center X coordinate of the BlockEntity</param>
        /// <param name="y">The center Y coordinate of the BlockEntity</param>
        /// <param name="width">The half-width (radius width) of the BlockEntity.</param>
        /// <param name="height">The half-height (radius height) of the BlockEntity.</param>
        /// <param name="background">True to make this a background entity, false to make it foreground.</param>
        /// <param name="collidable">True to enable collision of this entity, false to disable it.</param>
        /// <returns>true if the operation succeeded, false otherwise.</returns>
        public bool AddBlockEntity(string textureName, float x, float y, float z, float width, float height, bool background, bool collidable)
        {
            if (!Textures.ContainsKey(textureName))
            {
                Console.WriteLine("LoadableBattleArena.AddBlockEntity: Failed to add BlockEntity because referenced texture dependency not met: \"" + textureName + "\".");
                return false;
            }
            else
            {
                Texture tex = Textures[textureName];

                BlockEntity entity = new BlockEntity();
                entity.Position = new OpenTK.Vector3(x, y, z);
                entity.Dimensions = new OpenTK.Vector2(width, height);
                entity.CollisionEnabled = collidable;
                entity.Layer = BlockEntity.CollisionLayer.World;
                
                Dictionary<Texture, List<BlockEntity>> mapping;

                if(background)
                    mapping = DepthLayers[(int)DepthLayerTypes.Background].BlockEntriesByTexture;
                else
                    mapping = DepthLayers[(int)DepthLayerTypes.Foreground].BlockEntriesByTexture;

                if(!mapping.ContainsKey(tex))
                    mapping.Add(tex, new List<BlockEntity>());                     

                mapping[tex].Add(entity);
                
                return true;
            }
        }

        /// <summary>
        /// Sets the ground plane image bounds.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        public void SetDisplayAreaRange(float left, float right, float near, float far)
        {
            DisplayAreaLengthRange = new Vector2(left, right);
            DisplayAreaDepthRange = new Vector2(near, far);
        }

        #endregion
        // ---------------------- GLOBAL THINGS ----------------------------------
        #region GlobalThings

        /// <summary>
        /// A list of different indices into the depth layer list.
        /// </summary>
        public enum DepthLayerTypes : int
        {
            Background = 0,
            Foreground = 1
        }

        #endregion
        // ----------------------- INSTANCE PROPERTIES ----------------------------
        #region InstanceProperties

        /// <summary>
        /// The name of this arena, for display purposes.
        /// </summary>
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// A list of all textures loaded for this arena.
        /// </summary>
        public Dictionary<string, Texture> Textures
        {
            get;
            set;
        }
        
        /// <summary>
        /// The list of depth layers upon which objects are drawn.
        /// </summary>
        public List<DepthLayer> DepthLayers
        {
            get;
            set;
        }

        /// <summary>
        /// The root body that contains the entire scope of this arena.
        /// </summary>
        public Body GroundBody
        {
            get
            {
                return groundBody;
            }
            private set
            {
                groundBody = value;
            }
        }

        /// <summary>
        /// The width and height of this arena, in game units.
        /// </summary>
        public Vector2 Dimensions
        {
            get
            {
                return dimensions;
            }
            set
            {
                dimensions = value;
            }
        }

        /// <summary>
        /// The "left-right" range of the arena ground plane image (which is not necessarily the same size as the playable arena area).  Example: (-50,100) to make it run from X=-50 to X=100.
        /// </summary>
        public Vector2 DisplayAreaLengthRange
        {
            get;
            set;
        }

        /// <summary>
        /// The "near-far" range of the arena's ground plane image.  Example: (-80,40), to make the ground plane go from Z=-80 to Z=40.
        /// </summary>
        public Vector2 DisplayAreaDepthRange
        {
            get;
            set;
        }

        /// <summary>
        /// The texture-repeat level of the arena's ground plane image.
        /// </summary>
        public float DisplayAreaTextureScale
        {
            get;
            set;
        }

        /// <summary>
        /// The width (thickness) of the boundary blocks (invisible walls) at the edges of this arena.
        /// </summary>
        public float BoundaryThickness
        {
            get
            {
                return boundaryThickness;
            }
            set
            {
                boundaryThickness = value;
            }
        }

        /// <summary>
        /// The gravity vector used to initialize (bind) this arena.
        /// </summary>
        public Vector2 Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }

        /// <summary>
        /// The texture with which the 
        /// </summary>
        public Texture GroundTexture
        {
            get;
            set;
        }

        #endregion
        // -------------------------- INSTANCE METHODS ---------------------------------
        #region InstanceMethods

        /// <summary>
        /// Creates a new loadable battle arena (but does not add it to the managed list).
        /// </summary>
        /// <param name="width">Width of the arena's playable area, in game units.</param>
        /// <param name="height">Height of the arena's playable area, in game units.</param>
        /// <param name="boundaryThickness">The width (thickness) of the boundary blocks (invisible walls) at the edges of this arena.</param>
        public LoadableBattleArena(float width, float height, float boundaryThickness)
        {
            Dimensions = new Vector2(width, height);
            BoundaryThickness = boundaryThickness;
            DisplayAreaLengthRange = Dimensions;
            DisplayAreaDepthRange = new Vector2(-80, 40);
            DisplayAreaTextureScale = 1.0f;
            GroundTexture = null;

            DepthLayers = new List<DepthLayer>();

            // Add each layer.
            for (int i = 0; i < Enum.GetValues(typeof(DepthLayerTypes)).Length; i++)
                DepthLayers.Add(new DepthLayer());

            Textures = new Dictionary<string, Texture>();
            GroundBody = null;
            Name = "Untitled Arena";
        }

        /// <summary>
        /// Called to draw all of the background elements in the arena.
        /// </summary>
        public override void DrawBehind()
        {
            DrawGround();
            DrawDepthLayer(DepthLayers[(int)DepthLayerTypes.Background]);
        }

        /// <summary>
        /// Called to draw all of the foreground elements in the arena.
        /// </summary>
        public override void DrawFront()
        {
            DrawDepthLayer(DepthLayers[(int)DepthLayerTypes.Foreground]);            
        }

        /// <summary>
        /// Draws a specific depth layer immediately.
        /// </summary>
        /// <param name="layer"></param>
        private void DrawDepthLayer(DepthLayer layer)
        {
            foreach (Texture texture in new List<Texture>(Textures.Values))
            {
                if (!layer.BlockEntriesByTexture.ContainsKey(texture))
                    continue;

                List<BlockEntity> entities = layer.BlockEntriesByTexture[texture];

                GraphicsManager.SetTexture(texture);

                foreach (BlockEntity entity in entities)
                {
                    DrawBlockEntity(entity);
                }
            }
        }

        /// <summary>
        /// Draws a specific BlockEntity with whatever texture is currently bound.
        /// </summary>
        /// <param name="entity"></param>
        private void DrawBlockEntity(BlockEntity entity)
        {
            GraphicsManager.DrawQuad(new Vector3d(entity.Position.X - (entity.Width / 2.0), entity.Position.Y + (entity.Height / 2.0), entity.Position.Z),
                                     new Vector3d(entity.Position.X + (entity.Width / 2.0), entity.Position.Y + (entity.Height / 2.0), entity.Position.Z),
                                     new Vector3d(entity.Position.X + (entity.Width / 2.0), entity.Position.Y - (entity.Height / 2.0), entity.Position.Z),
                                     new Vector3d(entity.Position.X - (entity.Width / 2.0), entity.Position.Y - (entity.Height / 2.0), entity.Position.Z));
        }

        /// <summary>
        /// Draws the ground plane.
        /// </summary>
        private void DrawGround()
        {
            if(GroundTexture != null)
            {
                GraphicsManager.SetTexture(GroundTexture);
                GraphicsManager.DrawQuad(new Vector3d(DisplayAreaLengthRange.X, 0, DisplayAreaDepthRange.X),
                                         new Vector3d(DisplayAreaLengthRange.X, 0, DisplayAreaDepthRange.Y),
                                         new Vector3d(DisplayAreaLengthRange.Y, 0, DisplayAreaDepthRange.Y),
                                         new Vector3d(DisplayAreaLengthRange.Y, 0, DisplayAreaDepthRange.X),
                                         new Vector2d(DisplayAreaTextureScale, DisplayAreaTextureScale));
            }
            else
            {
                GraphicsManager.DrawQuad(new Vector3d(DisplayAreaLengthRange.X, 0, DisplayAreaDepthRange.X),
                                         new Vector3d(DisplayAreaLengthRange.X, 0, DisplayAreaDepthRange.Y),
                                         new Vector3d(DisplayAreaLengthRange.Y, 0, DisplayAreaDepthRange.Y),
                                         new Vector3d(DisplayAreaLengthRange.Y, 0, DisplayAreaDepthRange.X),
                                         System.Drawing.Color.Yellow);
            }
        }

        /// <summary>
        /// Performs all of the binding steps (attaching to physics, etc.)
        /// </summary>
        public void Bind()
        {
            base.Bind();

            foreach (DepthLayer layer in DepthLayers)
            {
                foreach (BlockEntity entity in layer.BlockEntities)
                {
                    entity.Bind(this);
                }
            }
        }

        #endregion
    }
}
