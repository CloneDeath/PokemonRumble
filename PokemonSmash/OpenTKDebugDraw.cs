using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using Box2DX.Common;
using OpenTK.Graphics.OpenGL;

namespace PokemonSmash {
	class OpenTKDebugDraw : DebugDraw {
		const float ZLayer = -0.01f;
		public override void DrawCircle(Vec2 center, float radius, Color color) {
			float k_segments = 16.0f;
			float k_increment = 2.0f * (float)System.Math.PI / k_segments;
			float theta = 0.0f;
			GL.Color3(color.R, color.G, color.B);
			GL.Disable(EnableCap.Texture2D);
			GL.Begin(BeginMode.LineLoop);
			for (int i = 0; i < k_segments; ++i) {
				Vec2 v = center + radius * new Vec2((float)System.Math.Cos(theta), (float)System.Math.Sin(theta));
				GL.Vertex3(v.X, v.Y, ZLayer);
				theta += k_increment;
			}
			GL.End();
			GL.Enable(EnableCap.Texture2D);
		}

		public override void DrawPolygon(Vec2[] vertices, int vertexCount, Color color) {
			GL.Color3(color.R, color.G, color.B);
			GL.Disable(EnableCap.Texture2D);
			GL.Begin(BeginMode.LineLoop);
			for (int i = 0; i < vertexCount; ++i) {
				GL.Vertex3(vertices[i].X, vertices[i].Y, ZLayer);
			}
			GL.End();
			GL.Enable(EnableCap.Texture2D);
		}

		public override void DrawSegment(Vec2 p1, Box2DX.Common.Vec2 p2, Color color) {
			GL.Color3(color.R, color.G, color.B);
			GL.Disable(EnableCap.Texture2D);
			GL.Begin(BeginMode.Lines);
			{
				GL.Vertex3(p1.X, p1.Y, ZLayer);
				GL.Vertex3(p2.X, p2.Y, ZLayer);
			}
			GL.End();
			GL.Enable(EnableCap.Texture2D);
		}

		public override void DrawSolidCircle(Vec2 center, float radius, Box2DX.Common.Vec2 axis, Color color) {
			float k_segments = 16.0f;
			float k_increment = 2.0f * (float)System.Math.PI / k_segments;
			float theta = 0.0f;
			GL.Color3(0.5f * color.R, 0.5f * color.G, 0.5f * color.B);
			GL.Disable(EnableCap.Texture2D);
			GL.Begin(BeginMode.TriangleFan);
			for (int i = 0; i < k_segments; ++i) {
				Vec2 v = center + radius * new Vec2((float)System.Math.Cos(theta), (float)System.Math.Sin(theta));
				GL.Vertex3(v.X, v.Y, ZLayer);
				theta += k_increment;
			}
			GL.End();

			theta = 0.0f;
			GL.Color4(color.R, color.G, color.B, 1.0f);
			GL.Begin(BeginMode.LineLoop);
			for (int i = 0; i < k_segments; ++i) {
				Vec2 v = center + radius * new Vec2((float)System.Math.Cos(theta), (float)System.Math.Sin(theta));
				GL.Vertex3(v.X, v.Y, ZLayer);
				theta += k_increment;
			}
			GL.End();

			Vec2 p = center + radius * axis;
			GL.Begin(BeginMode.Lines);
			GL.Vertex3(center.X, center.Y, ZLayer);
			GL.Vertex3(p.X, p.Y, 0);
			GL.End();
			GL.Enable(EnableCap.Texture2D);
		}

		public override void DrawSolidPolygon(Vec2[] vertices, int vertexCount, Color color) {
			GL.Color3(0.5f * color.R, 0.5f * color.G, 0.5f * color.B);
			GL.Disable(EnableCap.Texture2D);
			GL.Begin(BeginMode.TriangleFan);
			for (int i = 0; i < vertexCount; ++i) {
				GL.Vertex3(vertices[i].X, vertices[i].Y, ZLayer);
			}
			GL.End();

			GL.Color4(color.R, color.G, color.B, 1.0f);
			GL.Begin(BeginMode.LineLoop);
			for (int i = 0; i < vertexCount; ++i) {
				GL.Vertex3(vertices[i].X, vertices[i].Y, ZLayer);
			}
			GL.End();
			GL.Enable(EnableCap.Texture2D);
		}

		public override void DrawXForm(XForm xf) {
			Vec2 p1 = xf.Position, p2;
			float k_axisScale = 0.4f;
			GL.Disable(EnableCap.Texture2D);
			GL.Begin(BeginMode.Lines);
			{
				GL.Color3(1.0f, 0.0f, 0.0f);
				GL.Vertex3(p1.X, p1.Y, ZLayer);
				p2 = p1 + k_axisScale * xf.R.Col1;
				GL.Vertex3(p2.X, p2.Y, ZLayer);

				GL.Color3(0.0f, 1.0f, 0.0f);
				GL.Vertex3(p1.X, p1.Y, ZLayer);
				p2 = p1 + k_axisScale * xf.R.Col2;
				GL.Vertex3(p2.X, p2.Y, ZLayer);
			}
			GL.End();
			GL.Enable(EnableCap.Texture2D);
		}

		public static void DrawSegment(Vec2 p1, Vec2 p2, Color color, params object[] p) {
			GL.Color3(color.R, color.G, color.B);
			GL.Disable(EnableCap.Texture2D);
			GL.Begin(BeginMode.Lines);
			{
				GL.Vertex3(p1.X, p1.Y, ZLayer);
				GL.Vertex3(p2.X, p2.Y, ZLayer);
			}
			GL.End();
			GL.Enable(EnableCap.Texture2D);
		}
	}
}
