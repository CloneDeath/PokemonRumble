using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using System.Drawing;
using System.Diagnostics;
using OpenTK;
using PokemonRumble.Input;

namespace PokemonRumble {
	class Program {
		static Stopwatch DrawTime = new Stopwatch();
		static Stopwatch UpdateTime = new Stopwatch();

		static BattleArena Arena;
		static Player Player1;

		static void Main(string[] args) {
			GraphicsManager.SetTitle("Pokemon Rumble");
			GraphicsManager.Render += new GraphicsManager.Renderer(Draw);
			GraphicsManager.Update += new GraphicsManager.Updater(GraphicsManager_Update);
			GraphicsManager.SetBackground(Color.SkyBlue);
			GraphicsManager.EnableMipmap = true;
			GraphicsManager.CameraUp = Vector3.UnitY;

			Initialize();
			GraphicsManager.Start();
		}

		private static void Initialize() {
			ResourceManager.Initialize();

			Arena = new BattleArena();
			Player1 = new Player(Arena);
			
			DrawTime.Start();
			UpdateTime.Start();
		}

		static float Zoom = 3;

		static void GraphicsManager_Update() {
			float dt = UpdateTime.ElapsedMilliseconds / 1000f;
			UpdateTime.Restart();

			Player1.Update(dt);

			Zoom -= MouseManager.GetMouseWheel() / 3f;

			GraphicsManager.SetCamera(new OpenTK.Vector3d(Player1.X, Player1.Y + 1, Zoom));
			GraphicsManager.SetLookAt(new OpenTK.Vector3d(Player1.X, Player1.Y, 0));
		}

		static void Draw() {
			float dt = DrawTime.ElapsedMilliseconds / 1000f;
			DrawTime.Restart();

			Arena.Update(dt);
			Arena.Draw();

			Player1.Draw(dt);

			
		}
	}
}
