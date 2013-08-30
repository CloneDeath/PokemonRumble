using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace PokemonSmash
{
	class Text
	{
		static Texture Font = new Texture(@"Data\font.png");
		//Draw Strings
		public static void DrawString(double x, double y, string msg, double Scale = 1.0)
		{
			GraphicsManager.SetColor(Color.Black);
			GraphicsManager.PushMatrix();
			GL.Translate(x, y, 0);
			GL.Scale(16 * Scale, 16 * Scale, 1);
			int offset = 0;
			float CharWidth = 0.5f;
			if (msg != null) {
				GL.BindTexture(TextureTarget.Texture2D, Texture.Font);
				for (int i = 0; i < msg.Length; i++) {
					if (msg.ToCharArray()[i] == '\n') {
						GL.Translate(-offset * CharWidth, 1, 0);
						offset = 0;
					} else {
						DrawChar(msg.ToCharArray()[i]);
						GL.Translate((16.0f / 16.0f) * CharWidth, 0.0f, 0.0f);
						offset += 1;
					}
				}
			}
			GraphicsManager.PopMatrix();
			GraphicsManager.SetColor(Color.White);
		}

		private static void DrawChar(int charAt)
		{
			int xSize = 16;
			int ySize = 16;
			int c = charAt;

			int cx = c / xSize;
			int cy = c % xSize;
			double top = (cy) * (1.0f / ySize);
			double bottom = (cy + 1) * (1.0f / ySize);
			double right = (cx + 1) * (1.0f / xSize);
			double left = (cx) * (1.0f / xSize);
			GL.Begin(BeginMode.Quads);
				GL.TexCoord2(top, left); GL.Vertex2(0.0f, 0.0f);
				GL.TexCoord2(top, right); GL.Vertex2(0.0f, 1.0f);
				GL.TexCoord2(bottom, right); GL.Vertex2(1.0f, 1.0f);
				GL.TexCoord2(bottom, left); GL.Vertex2(1.0f, 0.0f);
			GL.End();
		}
	}
}
