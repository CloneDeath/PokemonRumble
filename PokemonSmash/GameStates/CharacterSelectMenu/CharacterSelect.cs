using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gwen.Control;
using GLImp;
using PokemonSmash.Input;
using OpenTK;
using System.Drawing;
using PokemonSmash.GameStates.CharacterSelectMenu;

namespace PokemonSmash {
	class CharacterSelect : GameState {
		PlayerCursor[] cursors;
		Camera2D Camera;

		static Texture CheckMark = new Texture(@"Data\check_mark.png");

		public int SlotXCount = 5;
		public int SlotYCount = 5;
		public Vector2 SlotSize = new Vector2(75, 75);
		public Vector2 SlotSeperation = new Vector2(5, 5);
		public Vector2 SlotOffset = new Vector2(20, 20);
		PokemonSlot[,] PokemonSlots;

		bool preselect = false;
		string player1pre = "";
		string player2pre = "";
		public CharacterSelect(string p1, string p2) {
			preselect = true;
			player1pre = p1;
			player2pre = p2;
		}

		public CharacterSelect() {
			preselect = false;
		}


		ListBox P1Moves;
		public void Initialize() {
			PokemonSlots = new PokemonSlot[SlotXCount, SlotYCount];
			for (int x = 0; x < SlotXCount; x++) {
				for (int y = 0; y < SlotYCount; y++) {
					PokemonSlots[x, y] = new PokemonSlot();
					PokemonSlots[x, y].Size = SlotSize;
					PokemonSlots[x, y].Position = new Vector2(((SlotSize.X + SlotSeperation.X) * x) + SlotOffset.X,
															  ((SlotSize.Y + SlotSeperation.Y) * y) + SlotOffset.Y);
				}
			}

			//Portraits
			int xslot = 0;
			int yslot = 0;
			foreach (var kvp in PokemonManager.Pokemons) {
				string Name = kvp.Key;
				Pokemon pokemon = kvp.Value;
				Animation2D anim = new Animation2D(pokemon.Animation);
				anim.state.SetAnimation("idle", true);
				PokemonSlots[xslot, yslot].Pokemon = pokemon;
				PokemonSlots[xslot, yslot].Animation = anim;

				if (++xslot >= SlotXCount) {
					xslot = 0;
					yslot++;
				}
			}

			CursorPosition p1c = new CursorPosition(0, 0, SlotXCount - 1, SlotYCount - 1);
			CursorPosition p2c = new CursorPosition(4, 0, SlotXCount - 1, SlotYCount - 1);
			if (preselect) {
				for (int x = 0; x < SlotXCount; x++) {
					for (int y = 0; y < SlotYCount; y++) {
						if (PokemonSlots[x, y].Pokemon == null) {
							continue;
						}

						if (PokemonSlots[x, y].Pokemon.Name == player1pre) {
							p1c.X = x;
							p1c.Y = y;
						}

						if (PokemonSlots[x, y].Pokemon.Name == player2pre) {
							p2c.X = x;
							p2c.Y = y;
						}
					}
				}
			}
			

			cursors = new PlayerCursor[2];
			cursors[0] = new PlayerCursor(0, p1c, Controls.Player1, @"Data\Pokeball.png", @"Data\RedSlot.png", new Vector2(0, 1));
			cursors[1] = new PlayerCursor(1, p2c, Controls.Player2, @"Data\Greatball.png",@"Data\BlueSlot.png", new Vector2(1, 1));

			Camera = new Camera2D();
			Camera.OnRender += new GraphicsManager.Renderer(Camera_OnRender);

			P1Moves = new ListBox(MainCanvas.GetCanvas());
			P1Moves.SetPosition(500, 10);
			P1Moves.SetSize(200, 400);
			LoadMoves();
		}

		private void LoadMoves()
		{
			P1Moves.Clear();

			PokemonSlot slot = PokemonSlots[cursors[0].Position.X, cursors[0].Position.Y];
			if (slot.Pokemon != null) {
				foreach (string s in slot.Pokemon.Moves) {
					P1Moves.AddRow(s);
				}
			}
			P1Moves.SelectedRowIndex = 2;
		}

		public void Uninitialize() {
			MainCanvas.GetCanvas().DeleteAllChildren();
			Camera.Disable();
		}

		void Camera_OnRender() {
			foreach (var slot in PokemonSlots) {
				if (slot != null) {
					slot.Draw();
				}
			}

			foreach (PlayerCursor cursor in cursors) {
				Vector2 corner = new Vector2(cursor.Corner.X * (SlotSize.X - 30), cursor.Corner.Y * (SlotSize.Y - 30));
				Vector2 Position = PokemonSlots[cursor.Position.X, cursor.Position.Y].Position;
				if (cursor.State == CursorState.PokemonSelect) {
					cursor.Token.Draw(Position.X + corner.X, Position.Y + corner.Y, 30, 30);
				}

				float Factor = 4;
				float Width = 54 * Factor;
				float Height = 94 * Factor;
				cursor.Slot.Draw((cursor.PlayerNumber * (Width + 10)) + 10, GraphicsManager.WindowHeight - Height - 10, Width, Height);

				if (PokemonSlots[cursor.Position.X, cursor.Position.Y].Pokemon != null) {
					PokemonSlots[cursor.Position.X, cursor.Position.Y].Animation.Draw2D(((cursor.PlayerNumber + 1) * Width) - (Width / 2), GraphicsManager.WindowHeight - Height / 4, 5, 5);
				}

				if (cursor.State == CursorState.Ready) {
					CheckMark.Draw(cursor.PlayerNumber * (Width + 10), GraphicsManager.WindowHeight - Height - 10, 517 * 0.5, 700 * 0.5);
				}
			}
		}

		public void Draw(float dt) {
			
		}

		public void Update(float dt) {
			bool AllReady = true;
			foreach (PlayerCursor cursor in cursors){
				if (cursor.control.IsPressed(Control.Accept)){
					if (PokemonSlots[cursor.Position.X, cursor.Position.Y].Pokemon != null) {
						cursor.State = CursorState.Ready;
					}
				} else if (cursor.control.IsPressed(Control.Cancel)){
					cursor.State = CursorState.PokemonSelect;
				}

				if (cursor.State == CursorState.PokemonSelect) {
					if (cursor.control.IsPressed(Control.Right)) {
						cursor.Position.MoveRight();
					}

					if (cursor.control.IsPressed(Control.Left)) {
						cursor.Position.MoveLeft();
					}

					if (cursor.control.IsPressed(Control.Up)) {
						cursor.Position.MoveUp();
					}

					if (cursor.control.IsPressed(Control.Down)) {
						cursor.Position.MoveDown();
					}
				}
				AllReady &= (cursor.State == CursorState.Ready);
			}
			LoadMoves();

			if (AllReady) {
				string p1name = PokemonSlots[cursors[0].Position.X, cursors[0].Position.Y].Pokemon.Name;
				string p2name = PokemonSlots[cursors[1].Position.X, cursors[1].Position.Y].Pokemon.Name;
				Program.SwitchState(new Battle(p1name, p2name));
			}
		}
	}
}
