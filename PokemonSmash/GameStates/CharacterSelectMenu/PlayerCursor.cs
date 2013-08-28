using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using PokemonSmash.Input;
using OpenTK;
using PokemonSmash.GameStates.CharacterSelectMenu;

namespace PokemonSmash {
	enum CursorState
	{
		PokemonSelect,
		MoveSelect,
		Ready
	}
	class PlayerCursor {
		public PlayerCursor(int playernumber, CursorPosition pos, ControlSet contr, string tokenloc, string slotloc, Vector2 corner)
		{
			this.Position = pos;
			this.control = contr;
			Token = new Texture(tokenloc);
			Slot = new Texture(slotloc, false);
			this.Corner = corner;
			this.PlayerNumber = playernumber;
			State = CursorState.PokemonSelect;
		}
		public CursorPosition Position;
		public ControlSet control;
		public CursorState State;
		public Texture Token;
		public Texture Slot;
		public Vector2 Corner;
		public int PlayerNumber;
	}
}
