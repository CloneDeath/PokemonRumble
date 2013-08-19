using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using System.Drawing;
using System.Diagnostics;
using OpenTK;
using PokemonRumble.Input;
using PokemonRumble.Arenas;
using OpenTK.Graphics.OpenGL;

namespace PokemonRumble {
	class Program {
		public static bool DebugDraw = false;

		static Stopwatch DrawTime = new Stopwatch();
		static Stopwatch UpdateTime = new Stopwatch();

		static BattleArena Arena;
		static Player Player1;
		static Player Player2;
		static FloatingCamera Camera;

		public enum GameState {
			MainMenu,
			CharacterSelect,
			Battle
		}

		static void Main(string[] args) {
			GraphicsManager.UseExperimentalFullAlpha = true;
			GraphicsManager.DisableDepthTest = true;
			GraphicsManager.SetTitle("Pokemon Rumble");
			GraphicsManager.Render += new GraphicsManager.Renderer(Draw);
			GraphicsManager.Update += new GraphicsManager.Updater(GraphicsManager_Update);
			GraphicsManager.SetBackground(Color.SkyBlue);
			GraphicsManager.EnableMipmap = true;
			GraphicsManager.CameraUp = Vector3.UnitY;
			GraphicsManager.SetWindowState(WindowState.Maximized);
			
						

			Initialize();
			GraphicsManager.Start();
		}

		private static void Initialize() {
			ResourceManager.Initialize();

			Arena = new Forrest();
			Player1 = new Player(Arena, Controls.Player1, -1, "bulbasaur");
			Player2 = new Player(Arena, Controls.Player2, -2, "squirtle");

			Player1.SetPosition(-5, 3);
			Player2.SetPosition(5, 3);
			Player1.Direction = 1;

			Camera = new FloatingCamera(Player1, Player2);
			
			DrawTime.Start();
			UpdateTime.Start();
		}

		static void GraphicsManager_Update() {
			float dt = UpdateTime.ElapsedMilliseconds / 1000f;
			UpdateTime.Restart();

			Player1.Update(dt);
			Player2.Update(dt);

			if (Player1.HP <= 0) {
				Player1.Kill();
			}

			if (Player2.HP <= 0) {
				Player2.Kill();
			}
		}

		public static Action MiddleDrawQueue;
		static void Draw() {
			float dt = DrawTime.ElapsedMilliseconds / 1000f;
			DrawTime.Restart();

			
			Arena.DrawBehind();
			Arena.Update(dt);
			Player1.Draw(dt);
			Player2.Draw(dt);
			if (MiddleDrawQueue != null) {
				MiddleDrawQueue();
			}
			Arena.DrawFront();
		}
	}
}
