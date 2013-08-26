using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonSmash.GameStates.CharacterSelectMenu
{
	class CursorPosition
	{
		public int X;
		public int Y;
		public int MaxX;
		public int MaxY;

		public CursorPosition(int X, int Y, int MaxX, int MaxY)
		{
			this.X = X;
			this.Y = Y;
			this.MaxX = MaxX;
			this.MaxY = MaxY;
		}

		public void MoveRight()
		{
			X += 1;
			if (X > MaxX) {
				X = 0;
			}
		}

		public void MoveLeft()
		{
			X -= 1;
			if (X < 0) {
				X = MaxX;
			}
		}

		public void MoveUp()
		{
			Y -= 1;
			if (Y < 0) {
				Y = MaxY;
			}
		}

		public void MoveDown()
		{
			Y += 1;
			if (Y > MaxY) {
				Y = 0;
			}
		}
	}
}
