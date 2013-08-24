using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gwen.Control;
using GLImp;
using PokemonSmash.Input;
using OpenTK;
using System.Drawing;

namespace PokemonSmash {
	class CharacterSelect : GameState {
		PlayerCursor[] cursors;
		Camera2D Camera;

		static Texture CheckMark = new Texture(@"Data\check_mark.png");

		List<PokemonSlot> PokemonSlots;

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

		public void Initialize() {
			PokemonSlots = new List<PokemonSlot>();

			//Portraits
			foreach (var kvp in PokemonManager.Pokemons) {
				string Name = kvp.Key;
				Pokemon pokemon = kvp.Value;
				Animation2D anim = new Animation2D(pokemon.Animation);
				PokemonSlots.Add(new PokemonSlot(pokemon, anim));
			}

			int p1idx = 0;
			int p2idx = PokemonSlots.Count() - 1;
			if (preselect) {
				p1idx = PokemonSlots.FindIndex((p) => p.pokemon.Name == player1pre);
				p2idx = PokemonSlots.FindIndex((p) => p.pokemon.Name == player2pre);
			}
			

			cursors = new PlayerCursor[2];
			cursors[0] = new PlayerCursor(0, p1idx, Controls.Player1, @"Data\Pokeball.png", @"Data\RedSlot.png", new Vector2(-1, 1));
			cursors[1] = new PlayerCursor(1, p2idx, Controls.Player2, @"Data\Greatball.png",@"Data\BlueSlot.png", new Vector2(1, 1));

			Camera = new Camera2D();
			Camera.OnRender += new GraphicsManager.Renderer(Camera_OnRender);
		}

		public void Uninitialize() {
			MainCanvas.GetCanvas().DeleteAllChildren();
			Camera.Disable();
		}

		void Camera_OnRender() {
			double Size = 50;
			double xloc = Size;
			double xmax = Camera.Width - Size;
			double yloc = Size;
			foreach (var slot in PokemonSlots) {
				Pokemon pokemon = slot.pokemon;
				Animation2D anim = slot.animation;
				anim.Draw2D((float)xloc, (float)yloc);

				xloc += Size;
				if (xloc > xmax) {
					xloc = Size;
					yloc += Size;
				}
			}

			foreach (PlayerCursor cursor in cursors) {
				Vector2 corner = cursor.Corner * 15;
				cursor.Token.Draw((cursor.Position * Size) + Size + corner.X - 10, 20 + corner.Y, 20, 20);

				float Factor = 4;
				float Width = 54 * Factor;
				float Height = 94 * Factor;
				cursor.Slot.Draw((cursor.PlayerNumber * (Width + 10)) + 10, GraphicsManager.WindowHeight - Height - 10, Width, Height);

				PokemonSlots[cursor.Position].animation.Draw2D(((cursor.PlayerNumber + 1) * Width) - (Width / 2), GraphicsManager.WindowHeight - Height / 4, 5, 5);

				if (cursor.ready) {
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
					cursor.ready = true;
				} else if (cursor.control.IsPressed(Control.Cancel)){
					cursor.ready = false;
				}

				if (!cursor.ready) {
					if (cursor.control.IsPressed(Control.Right)) {
						cursor.Position += 1;
					}

					if (cursor.control.IsPressed(Control.Left)) {
						cursor.Position -= 1;
					}

					while (cursor.Position < 0) {
						cursor.Position += PokemonSlots.Count();
					}

					while (cursor.Position >= PokemonSlots.Count()) {
						cursor.Position -= PokemonSlots.Count();
					}
				}
				AllReady &= cursor.ready;
			}

			if (AllReady) {
				string p1name = PokemonSlots[cursors[0].Position].pokemon.Name;
				string p2name = PokemonSlots[cursors[1].Position].pokemon.Name;
				Program.SwitchState(new Battle(p1name, p2name));
			}
		}
	}
}
