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
			Arena = new BattleArena();
			Player1 = new Player(Arena);
			ResourceManager.Initialize();
			DrawTime.Start();
			UpdateTime.Start();
		}

		static float Zoom = 3;

		static void GraphicsManager_Update() {
			float dt = UpdateTime.ElapsedMilliseconds / 1000f;
			UpdateTime.Restart();

			Player1.Update(dt);

			Zoom -= MouseManager.GetMouseWheel() / 3f;

			GraphicsManager.SetCamera(new OpenTK.Vector3d(ResourceManager.skeleton.X, ResourceManager.skeleton.Y + 1, Zoom));
			GraphicsManager.SetLookAt(new OpenTK.Vector3d(ResourceManager.skeleton.X, ResourceManager.skeleton.Y, 0));

			
		}

		static void Draw() {
			float dt = DrawTime.ElapsedMilliseconds / 1000f;
			DrawTime.Restart();

			ResourceManager.state.Update(dt);
			

			ResourceManager.state.Apply(ResourceManager.skeleton);
			ResourceManager.skeleton.RootBone.ScaleX = 1 / 50f;
			ResourceManager.skeleton.RootBone.ScaleY = 1 / 50f;
			ResourceManager.skeleton.UpdateWorldTransform();
			ResourceManager.skeletonRenderer.Draw(ResourceManager.skeleton);

			Arena.Update(dt);
			Arena.Draw();

			GraphicsManager.DrawQuad(new Vector3d(ResourceManager.skeleton.X - 0.4, 0.00002, -0.3),
									 new Vector3d(ResourceManager.skeleton.X + 0.4, 0.00002, -0.3),
									 new Vector3d(ResourceManager.skeleton.X + 0.4, 0.00002, 0.3),
									 new Vector3d(ResourceManager.skeleton.X - 0.4, 0.00002, 0.3),
									 ResourceManager.Shadow);
			
		}
	}
}
