using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using System.Drawing;

namespace PokemonSmash.GameStates.CharacterSelectMenu
{
	class MoveList
	{
		List<MoveSlot> MoveSlots = new List<MoveSlot>();
		PlayerCursor pc;
		int onslot = 0;

		public void Reload(PlayerCursor pc)
		{
			MoveSlots.Clear();
			foreach (Move m in pc.Pokemon.Moves) {
				MoveSlots.Add(new MoveSlot(m));
			}

			pc.MovePos = new CursorPosition(0, 0, 0, MoveSlots.Count() - 1);
			this.pc = pc;
		}

		public void Draw(float x, float y)
		{
			if (pc == null) return;

			float Height = 60;
			if (pc.State == CursorState.MoveSelect) {
				float yat = 0;
				for (int i = -2; i <= 2; i++) {
					float scale = 1.5f - (Math.Abs(i) / 2f);
					DrawSlot(pc.MovePos.Y + i, x + (scale * 30), y + yat, scale);
					yat += Height * scale;
				}
			}

			for (int i = 0; i < 4; i++) {
				if (pc.Moves[i] != null) {
					pc.Moves[i].Type.Panel.Draw(x + 10, y + 300 + (i * Height));
					Text.DrawString(x + 20, y + 310 + (i * Height), pc.Moves[i].DisplayName);
				}
			}
		}

		private void DrawSlot(int slotnumber, float x, float y, float scale)
		{
			if (slotnumber < 0) {
				return;
			}

			if (slotnumber >= MoveSlots.Count()) {
				return;
			}

			if (MoveSlots.Count() == 0) {
				return;
			}

			MoveSlots[slotnumber].Draw(x, y, scale);
		}

		public void Update()
		{
			if (pc.control.IsPressed(Input.Control.Down)) {
				pc.MovePos.MoveDown();
			}

			if (pc.control.IsPressed(Input.Control.Up)) {
				pc.MovePos.MoveUp();
			}

			if (pc.control.IsPressed(Input.Control.Pause)) {
				pc.State = CursorState.Ready;
			}

			if (pc.control.IsPressed(Input.Control.Accept)) {
				if (onslot <= 3) {
					pc.Moves[onslot] = MoveSlots[pc.MovePos.Y].move;
					onslot++;
				}
				if (onslot == 4) {
					pc.State = CursorState.Ready;
				}
			}

			if (pc.control.IsPressed(Input.Control.Cancel)) {
				if (onslot == 0) {
					pc.Moves[0] = null;
					pc.Moves[1] = null;
					pc.Moves[2] = null;
					pc.Moves[3] = null;
					onslot = 0;
					pc.State = CursorState.PokemonSelect;
				} else {
					onslot--;
					pc.Moves[onslot] = null;
				}
			}
		}
	}
}
