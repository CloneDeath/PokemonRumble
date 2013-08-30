using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using System.Drawing;
using OpenTK;

namespace PokemonSmash {
	class FloatingCamera {
		private Player Player1;
		private Player Player2;
		Camera2D Camera;

		public FloatingCamera(Player Player1, Player Player2) {
			this.Player1 = Player1;
			this.Player2 = Player2;

			GraphicsManager.Update += GraphicsManager_Update;
			Camera = new Camera2D();
			Camera.OnRender += new GraphicsManager.Renderer(Camera_OnRender);
		}

		void Camera_OnRender() {
			//P1 Health
			Color HPColor = Color.Green;
			if (Player1.HP < Player1.Pokemon.HP / 2) HPColor = Color.Yellow;
			if (Player1.HP < Player1.Pokemon.HP / 5) HPColor = Color.Red;
			GraphicsManager.DrawRectangle(10, 10, Player1.Pokemon.HP + 2, 20, Color.Black);
			GraphicsManager.DrawRectangle(11, 11, Player1.HP, 18, HPColor);

			//P2 Health
			HPColor = Color.Green;
			if (Player2.HP < Player2.Pokemon.HP / 2) HPColor = Color.Yellow;
			if (Player2.HP < Player2.Pokemon.HP / 5) HPColor = Color.Red;
			int Width = GraphicsManager.WindowWidth;
			GraphicsManager.DrawRectangle(Width - 10, 10, -(Player2.Pokemon.HP + 2), 20, Color.Black);
			GraphicsManager.DrawRectangle(Width - 11, 11, -(Player2.HP), 18, HPColor);
		}

		public Vector2d Center {
			get {
				return new Vector2d((Player1.X + Player2.X) / 2, (Player1.Y + Player2.Y) / 2);
			}
		}

		public double Distance {
			get {
				return Math.Sqrt(Math.Pow(Player1.X - Player2.X, 2) + Math.Pow(Player1.Y - Player2.Y, 2));
			}
		}

		void GraphicsManager_Update() {
			double Height = Distance / 2;
			double DegToRad = Math.PI / 180;

			double Dist = ((1.0 / Math.Tan(30 * DegToRad)) * Height);
			if (Dist < 3) Dist = 3;
			GraphicsManager.SetCamera(new Vector3d(Center.X, Center.Y - 1 + Math.Sqrt(Dist), Dist));
			GraphicsManager.SetLookAt(new Vector3d(Center.X, Center.Y, 0));
		}

		internal void Disable() {
			Camera.Disable();
			GraphicsManager.Update -= GraphicsManager_Update;
		}
	}
}
