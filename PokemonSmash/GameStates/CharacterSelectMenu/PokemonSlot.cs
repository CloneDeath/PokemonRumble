using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using GLImp;
using System.Drawing;

namespace PokemonSmash{
	class PokemonSlot {
		public PokemonSlot(){
			this.Position = new Vector2(0, 0);
			this.Size = new Vector2(32, 32);
			Pokemon = null;
			Animation = null;
		}
		public Pokemon Pokemon;
		public Animation2D Animation;
		public Vector2 Position;
		public Vector2 Size;

		internal void Draw()
		{
			if (Pokemon != null) {
				DrawBackgroundColor(Color.FromArgb(100, Pokemon.Color.R, Pokemon.Color.G, Pokemon.Color.B));
				if (Pokemon.PrimaryType != PokemonType.None) {
					Pokemon.PrimaryType.Image.Draw(Position.X + 1, Position.Y + 1);
				}
				if (Pokemon.SecondaryType != PokemonType.None) {
					Pokemon.SecondaryType.Image.Draw(Position.X + 34, Position.Y + 1);
				}
				Text.DrawString(Position.X + 1, Position.Y + 14, Pokemon.DisplayName, 0.9);
				Animation.Draw2D(Position.X + (Size.X / 2), Position.Y + Size.Y - 10);
				
			} else {
				DrawBackgroundColor(Color.Gray);
			}
		}

		private Color DrawBackgroundColor(Color temp)
		{
			GraphicsManager.DrawRectangle((Vector2d)Position, (Vector2d)(Position + Size), temp);
			return temp;
		}
	}
}
