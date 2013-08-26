using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using GLImp;
using System.Drawing;

namespace PokemonSmash{
	class PokemonSlot {
		public PokemonSlot(Pokemon pkmn, Animation2D anim){
			this.pokemon = pkmn;
			this.animation = anim;
			this.Position = new Vector2(0, 0);
			this.Size = new Vector2(32, 32);
		}
		public Pokemon pokemon;
		public Animation2D animation;
		public Vector2 Position;
		public Vector2 Size;

		internal void Draw()
		{
			GraphicsManager.DrawRectangle((Vector2d)Position, (Vector2d)(Position + Size), pokemon.Color);
			animation.Draw2D(Position.X + (Size.X / 2), Position.Y + Size.Y - 10);
		}
	}
}
