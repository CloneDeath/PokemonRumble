using System;
using System.Collections.Generic;
using System.Drawing;
using GLImp;
using OpenTK.Graphics.OpenGL;

namespace Spine {
	public class SkeletonRenderer {
		float[] vertices = new float[8];

		public SkeletonRenderer () {
			Bone.yDown = false;
		}

		public void Draw(Skeleton skeleton, float Z) {
			List<Slot> DrawOrder = skeleton.DrawOrder;

			float depth = Z;
			float depth_offset = 0.0001f;
			for (int i = 0; i < DrawOrder.Count; i++) {
				Slot slot = DrawOrder[i];
				RegionAttachment regionAttachment = slot.Attachment as RegionAttachment;
				if (regionAttachment != null) {
					SpriteBatchItem item = new SpriteBatchItem();
					AtlasRegion region = (AtlasRegion)regionAttachment.RendererObject;
					item.Texture = (Texture)region.page.rendererObject;

					byte r = (byte)(skeleton.R * slot.R * 255);
					byte g = (byte)(skeleton.G * slot.G * 255);
					byte b = (byte)(skeleton.B * slot.B * 255);
					byte a = (byte)(skeleton.A * slot.A * 255);
					item.vertexTL.Color = Color.FromArgb(r, g, b, a);
					item.vertexBL.Color = Color.FromArgb(r, g, b, a);
					item.vertexBR.Color = Color.FromArgb(r, g, b, a);
					item.vertexTR.Color = Color.FromArgb(r, g, b, a);

					float[] vertices = this.vertices;
					regionAttachment.ComputeVertices(skeleton.X, skeleton.Y, slot.Bone, vertices);
					item.vertexTL.Position.X = vertices[RegionAttachment.X1];
					item.vertexTL.Position.Y = vertices[RegionAttachment.Y1];
					item.vertexTL.Position.Z = depth;
					item.vertexBL.Position.X = vertices[RegionAttachment.X2];
					item.vertexBL.Position.Y = vertices[RegionAttachment.Y2];
					item.vertexBL.Position.Z = depth;
					item.vertexBR.Position.X = vertices[RegionAttachment.X3];
					item.vertexBR.Position.Y = vertices[RegionAttachment.Y3];
					item.vertexBR.Position.Z = depth;
					item.vertexTR.Position.X = vertices[RegionAttachment.X4];
					item.vertexTR.Position.Y = vertices[RegionAttachment.Y4];
					item.vertexTR.Position.Z = depth;

					float[] uvs = regionAttachment.UVs;
					item.vertexTL.TextureCoordinate.X = uvs[RegionAttachment.X1];
					item.vertexTL.TextureCoordinate.Y = uvs[RegionAttachment.Y1];
					item.vertexBL.TextureCoordinate.X = uvs[RegionAttachment.X2];
					item.vertexBL.TextureCoordinate.Y = uvs[RegionAttachment.Y2];
					item.vertexBR.TextureCoordinate.X = uvs[RegionAttachment.X3];
					item.vertexBR.TextureCoordinate.Y = uvs[RegionAttachment.Y3];
					item.vertexTR.TextureCoordinate.X = uvs[RegionAttachment.X4];
					item.vertexTR.TextureCoordinate.Y = uvs[RegionAttachment.Y4];

					this.DrawItem(item);
					depth += depth_offset;
				}
			}
		}

		private void DrawItem(SpriteBatchItem item) {
			GL.BindTexture(TextureTarget.Texture2D, item.Texture.ID);
			GL.Begin(BeginMode.Quads);
			{
				GL.TexCoord2(item.vertexTL.TextureCoordinate); GL.Color3(item.vertexTL.Color); GL.Vertex3(item.vertexTL.Position);
				GL.TexCoord2(item.vertexTR.TextureCoordinate); GL.Color3(item.vertexTR.Color); GL.Vertex3(item.vertexTR.Position);
				GL.TexCoord2(item.vertexBR.TextureCoordinate); GL.Color3(item.vertexBR.Color); GL.Vertex3(item.vertexBR.Position);
				GL.TexCoord2(item.vertexBL.TextureCoordinate); GL.Color3(item.vertexBL.Color); GL.Vertex3(item.vertexBL.Position);
			}
			GL.End();
		}
	}
}
