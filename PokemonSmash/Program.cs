using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using System.Drawing;
using System.Diagnostics;
using OpenTK;
using PokemonSmash.Input;
using PokemonSmash.Arenas;
using OpenTK.Graphics.OpenGL;

namespace PokemonSmash {
	class Program {
		public static bool DebugDraw = false;

		static Stopwatch DrawTime = new Stopwatch();
		static Stopwatch UpdateTime = new Stopwatch();

		static GameState CurrentState = null;
		public static Action MiddleDrawQueue;

		static void Main(string[] args) {
			GraphicsManager.UseExperimentalFullAlpha = true;
			GraphicsManager.DisableDepthTest = true;
			GraphicsManager.SetTitle("Pokemon Smash");
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

			SwitchState(new CharacterSelect());
			//SwitchState(new Battle("squirtle", "squirtle"));

			DrawTime.Start();
			UpdateTime.Start();
		}

		public static void SwitchState(GameState newstate) {
			if (CurrentState != null) {
				CurrentState.Uninitialize();
			}
			newstate.Initialize();
			CurrentState = newstate;
		}

		static void GraphicsManager_Update() {
			float dt = UpdateTime.ElapsedMilliseconds / 1000f;
			UpdateTime.Restart();

			CurrentState.Update(dt);
		}

		
		static void Draw() {
			float dt = DrawTime.ElapsedMilliseconds / 1000f;
			DrawTime.Restart();

			CurrentState.Draw(dt);
		}
	}
}
