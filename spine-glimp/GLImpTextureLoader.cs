using System;
using System.IO;
using GLImp;

namespace Spine {
	public class GLImpTextureLoader : TextureLoader {
		public GLImpTextureLoader() {
		}

		public void Load (AtlasPage page, String path) {
			Texture texture = new Texture(path, false);
			page.rendererObject = texture;
			page.width = texture.Width;
			page.height = texture.Height;
		}

		public void Unload (Object texture) {
			(texture as Texture).Free();
		}
	}
}
