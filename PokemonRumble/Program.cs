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
		static Stopwatch gameTime = new Stopwatch();

		static void Main(string[] args) {
			GraphicsManager.SetTitle("Pokemon Rumble");
			GraphicsManager.Render += new GraphicsManager.Renderer(Draw);
			GraphicsManager.Update += new GraphicsManager.Updater(GraphicsManager_Update);
			GraphicsManager.SetBackground(Color.SkyBlue);
			GraphicsManager.EnableMipmap = true;
			GraphicsManager.CameraUp = Vector3.UnitY;
			ResourceManager.Initialize();

			gameTime.Start();
			GraphicsManager.Start();
		}

		static void GraphicsManager_Update() {
			if (Controls.IsDown(Control.Left)){
				if (ResourceManager.state.Animation.Name != "walk") {
					ResourceManager.state.SetAnimation("walk", true);
				}
				ResourceManager.skeleton.X -= 0.04f;
				ResourceManager.skeleton.FlipX = false;
			} else if (Controls.IsDown(Control.Right)){
				if (ResourceManager.state.Animation.Name != "walk") {
					ResourceManager.state.SetAnimation("walk", true);
				}
				ResourceManager.skeleton.X += 0.04f;
				ResourceManager.skeleton.FlipX = true;
			} else {
				if (ResourceManager.state.Animation.Name != "idle") {
					ResourceManager.state.SetAnimation("idle", true);
				}
			}

			GraphicsManager.SetCamera(new OpenTK.Vector3d(ResourceManager.skeleton.X, ResourceManager.skeleton.Y + 1, 3));
			GraphicsManager.SetLookAt(new OpenTK.Vector3d(ResourceManager.skeleton.X, ResourceManager.skeleton.Y, 0));
		}

		static void Draw() {
			ResourceManager.state.Update(gameTime.ElapsedMilliseconds / 1000f);
			gameTime.Restart();
			ResourceManager.state.Apply(ResourceManager.skeleton);
			ResourceManager.skeleton.RootBone.ScaleX = 1 / 50f;
			ResourceManager.skeleton.RootBone.ScaleY = 1 / 50f;
			ResourceManager.skeleton.UpdateWorldTransform();
			ResourceManager.skeletonRenderer.Draw(ResourceManager.skeleton);

			for (int z = -4; z <= 1; z++) {
				for (int x = -6; x < 6; x++) {
					int scale = 10;
					int left = x * scale;
					int right = (x + 1) * scale;
					int bottom = z * scale;
					int top = (z + 1) * scale;
					GraphicsManager.SetTexture(ResourceManager.Grass);
					GraphicsManager.DrawQuad(new Vector3d(left, 0, bottom), 
											 new Vector3d(left, 0, top), 
											 new Vector3d(right, 0, top), 
											 new Vector3d(right, 0, bottom),
											 new Vector2d(5, 5));
				}
			}

			GraphicsManager.SetTexture(ResourceManager.Dirt);
			GraphicsManager.DrawQuad(new Vector3d(-10, 0.00001, -5),
									 new Vector3d(-10, 0.00001, 5),
									 new Vector3d(10, 0.00001, 5),
									 new Vector3d(10, 0.00001, -5), 
									 new Vector2d(2, 4));

			GraphicsManager.DrawQuad(new Vector3d(ResourceManager.skeleton.X - 0.4, 0.00002, -0.3),
									 new Vector3d(ResourceManager.skeleton.X + 0.4, 0.00002, -0.3),
									 new Vector3d(ResourceManager.skeleton.X + 0.4, 0.00002, 0.3),
									 new Vector3d(ResourceManager.skeleton.X - 0.4, 0.00002, 0.3),
									 ResourceManager.Shadow);
			
		}
	}
}
