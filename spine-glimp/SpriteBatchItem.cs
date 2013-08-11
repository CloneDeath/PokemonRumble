using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;

namespace Spine {
	public class SpriteBatchItem {
		public Texture Texture;
		public VertexPositionColorTexture vertexTL = new VertexPositionColorTexture();
		public VertexPositionColorTexture vertexTR = new VertexPositionColorTexture();
		public VertexPositionColorTexture vertexBL = new VertexPositionColorTexture();
		public VertexPositionColorTexture vertexBR = new VertexPositionColorTexture();
	}
}
