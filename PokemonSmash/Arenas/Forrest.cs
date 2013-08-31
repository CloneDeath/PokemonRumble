using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using OpenTK;

namespace PokemonSmash.Arenas {
	class Forrest : BattleArena {
		private static Texture DirtTex = new Texture(@"Data\Dirt.png", false);
		private static Texture GrassTex = new Texture(@"Data\Grass.png");
		private static Texture TreeTex = new Texture(@"Data\Tree.png", false);

		List<Vector3d> BackTrees = new List<Vector3d>();
		List<Vector3d> FrontTrees = new List<Vector3d>();
		

		public Forrest() : base(){

            Bind();

			Random rand = new Random();
			List<Vector3d> Trees = new List<Vector3d>();
			for (int i = 0; i < 900; i++) {
				Trees.Add(new Vector3d((rand.NextDouble() - 0.5) * 200, (-(rand.NextDouble()) * 75) + 2, (rand.NextDouble() + 5)));
			}

			Trees.Sort((Comparison<Vector3d>)delegate(Vector3d a, Vector3d b) { return b.Y > a.Y ? -1 : 1; });

			foreach (var tree in Trees) {
				if (tree.Y <= 0) {
					BackTrees.Add(tree);
				} else {
					FrontTrees.Add(tree);
				}
			}
		}

		public override void DrawBehind() {
			DrawGround();

			
			DrawTreeList(BackTrees);
		}

		

		public override void DrawFront() {
			DrawTreeList(FrontTrees);
		}

		private static void DrawTreeList(List<Vector3d> Trees) {
			foreach (var tree in Trees) {
				if (Math.Abs(tree.X) < 12 && Math.Abs(tree.Y) < 8) {
					continue;
				}
				double left = tree.X - 3;
				double right = tree.X + 3;

				
				GraphicsManager.DrawQuad(new Vector3d(left, tree.Z, tree.Y),
										 new Vector3d(right, tree.Z, tree.Y),
										 new Vector3d(right, -0.5, tree.Y),
										 new Vector3d(left, -0.5, tree.Y),
										 TreeTex);
			}
		}

		private static void DrawGround() {
			for (int z = -8; z <= 4; z++) {
				for (int x = -10; x < 10; x++) {
					int scale = 10;
					int left = x * scale;
					int right = (x + 1) * scale;
					int bottom = z * scale;
					int top = (z + 1) * scale;
					GraphicsManager.SetTexture(GrassTex);
					GraphicsManager.DrawQuad(new Vector3d(left, 0, bottom),
											 new Vector3d(left, 0, top),
											 new Vector3d(right, 0, top),
											 new Vector3d(right, 0, bottom),
											 new Vector2d(5, 5));
				}
			}

			GraphicsManager.SetTexture(DirtTex);
			GraphicsManager.DrawQuad(new Vector3d(-10, 0.0001, -5),
									 new Vector3d(-10, 0.0001, 5),
									 new Vector3d(10, 0.0001, 5),
									 new Vector3d(10, 0.0001, -5),
									 new Vector2d(1, 1));
		}
	}
}
