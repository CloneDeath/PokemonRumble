﻿using System;
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

		public PlayerDef PlayerOne;
		public PlayerDef PlayerTwo;

		float DeadTimer;
		const float AftergameTime = 5.0f;

		public Battle(PlayerDef player1, PlayerDef player2) {
			PlayerOne = player1;
			PlayerTwo = player2;
		}

		public void Initialize() {

            if (LoadableBattleArena.Arenas.Count > 0)
            {
                Arena = LoadableBattleArena.Arenas[0];
                Arena.Bind();
            }
            else
            {
                Arena = new Forrest();
            }
                
			Player1 = new Player(Arena, Controls.Player1, -1, PlayerOne);
			Player2 = new Player(Arena, Controls.Player2, -2, PlayerTwo);

			Player1.SetPosition(-5, 3);
			Player2.SetPosition(5, 3);
			Player1.Direction = 1;

			Camera = new FloatingCamera(Player1, Player2);

			DeadTimer = 0.0f;
		}

		public void Uninitialize() {
			Arena = null;
			Player1.Unload();
			Player2.Unload();
			Player1 = null;
			Player2 = null;
			Camera.Disable();
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

			if (Player1.HP <= 0 || Player2.HP <= 0) {
				DeadTimer += dt;
				if (DeadTimer > AftergameTime) {
					Program.SwitchState(new CharacterSelect(Player1.Pokemon.Name, Player2.Pokemon.Name));
				}
			}
		}
	}
}
