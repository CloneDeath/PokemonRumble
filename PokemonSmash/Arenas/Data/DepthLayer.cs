using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;

namespace PokemonSmash.Arenas.Data
{
    /// <summary>
    /// Represents one depth at which objects can be drawn (such as foreground, background, etc.)
    /// </summary>
    public class DepthLayer
    {
        /// <summary>
        /// A lookup table of each texture used in this layer, and the corresponding BlockEntities that are drawn with that texture.
        /// </summary>
        public Dictionary<Texture, List<BlockEntity>> BlockEntriesByTexture
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a compiled list of all BlockEntities in this DepthLayer.
        /// Do not add to this list, as it is simply a copy for convenience during looping (however, the references in the list are not copies).
        /// </summary>
        public List<BlockEntity> BlockEntities
        {
            get
            {
                List<List<BlockEntity>> lists = new List<List<BlockEntity>>(BlockEntriesByTexture.Values);

                List<BlockEntity> list = new List<BlockEntity>();

                foreach (List<BlockEntity> piece in lists)
                {
                    list.AddRange(piece);
                }

                return list;
            }
        }

        public DepthLayer()
        {
            BlockEntriesByTexture = new Dictionary<Texture, List<BlockEntity>>();
        }
    }
}
