using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonSmash.Arenas;
using PokemonSmash.Input;

namespace PokemonSmash {
	class Battle : GameState {
		BattleArena Arena;
		Player Player1;
		Player Player2;
		FloatingCamera Camera;

		public string PlayerOne;
		public string PlayerTwo;

		public Battle(string player1, string player2) {
			PlayerOne = player1;
			PlayerTwo = player2;
		}

		public void Initialize() {
			Arena = new Forrest();
			Player1 = new Player(Arena, Controls.Player1, -1, PlayerOne);
			Player2 = new Player(Arena, Controls.Player2, -2, PlayerTwo);

			Player1.SetPosition(-5, 3);
			Player2.SetPosition(5, 3);
			Player1.Direction = 1;

			Camera = new FloatingCamera(Player1, Player2);
		}

		public void Uninitialize() {
			Arena = null;
			Player1 = null;
			Player2 = null;
			Camera = null;
		}

		public void Draw(float dt) {
			Arena.DrawBehind();
			Arena.Update(dt);
			Player1.Draw(dt);
			Player2.Draw(dt);
			if (Program.MiddleDrawQueue != null) {
				Program.MiddleDrawQueue();
			}
			Arena.DrawFront();
		}

		public void Update(float dt) {
			Player1.Update(dt);
			Player2.Update(dt);

			if (Player1.HP <= 0) {
				Player1.Kill();
			}

			if (Player2.HP <= 0) {
				Player2.Kill();
			}
		}
	}
}
