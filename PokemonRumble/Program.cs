using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using System.Drawing;
using System.Diagnostics;
using OpenTK;

namespace PokemonRumble {
	class Program {
		static Stopwatch gameTime = new Stopwatch();

		static void Main(string[] args) {
			GraphicsManager.SetTitle("Pokemon Rumble");
			GraphicsManager.Render += new GraphicsManager.Renderer(Draw);
			GraphicsManager.SetBackground(Color.Black);
			gameTime.Start();

			GraphicsManager.CameraUp = Vector3.UnitY;

			GraphicsManager.SetCamera(new OpenTK.Vector3d(0, 1, -10));
			GraphicsManager.SetLookAt(new OpenTK.Vector3d(0, 0, 0));


			ResourceManager.Initialize();
			GraphicsManager.Start();
		}

		static void Draw() {
			ResourceManager.state.Update(gameTime.ElapsedMilliseconds / 1000f);
			gameTime.Restart();
			ResourceManager.state.Apply(ResourceManager.skeleton);
			ResourceManager.skeleton.RootBone.ScaleX = 1 / 100f;
			ResourceManager.skeleton.RootBone.ScaleY = 1 / 100f;
			ResourceManager.skeleton.UpdateWorldTransform();
			ResourceManager.skeletonRenderer.Draw(ResourceManager.skeleton);

			GraphicsManager.DrawQuad(new Vector3d(-10, 0, -10), new Vector3d(-10, 0, 10), new Vector3d(10, 0, 10), new Vector3d(10, 0, -10), ResourceManager.Dirt);
		}
	}
}
