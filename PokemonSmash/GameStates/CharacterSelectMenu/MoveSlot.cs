using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace PokemonSmash.GameStates.CharacterSelectMenu
{
	class MoveSlot
	{
		public Move move;

		public MoveSlot(Move m)
		{
			this.move = m;
		}

		internal void Draw(float x, float y, float scale = 1.0f)
		{
			Texture panel = move.Type.Panel;
			panel.Draw(x, y, panel.Width * scale, panel.Height * scale);
			Text.DrawString(x + (10 * scale), y + (10 * scale), move.DisplayName, 0.9f * scale);
			GraphicsManager.SetColor(Color.White);
		}
	}
}
