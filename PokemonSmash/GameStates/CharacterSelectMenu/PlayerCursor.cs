using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using PokemonSmash.Input;
using OpenTK;

namespace PokemonSmash {
	class PlayerCursor {
		public PlayerCursor(int playernumber, int pos, ControlSet contr, string tokenloc, string slotloc, Vector2 corner) {
			this.Position = pos;
			this.control = contr;
			Token = new Texture(tokenloc);
			Slot = new Texture(slotloc, false);
			this.Corner = corner;
			this.PlayerNumber = playernumber;
		}
		public int Position;
		public ControlSet control;
		public bool ready = false;
		public Texture Token;
		public Texture Slot;
		public Vector2 Corner;
		public int PlayerNumber;
	}
}
