using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;
using GLImp;
using System.Diagnostics;

namespace PokemonSmash {
	public class Animation2D {
		public SkeletonRenderer skeletonRenderer;
		public Skeleton skeleton;
		public AnimationState state;
		public AnimationStateData stateData;
		public Stopwatch drawtime;
		public float Scale = 1.0f;

		public Animation2D(string AnimationFile) {
			skeletonRenderer = new SkeletonRenderer(true);

			String name = AnimationFile;

			Atlas atlas = new Atlas("Data/" + name + ".atlas", new GLImpTextureLoader());
			SkeletonJson json = new SkeletonJson(atlas);
			skeleton = new Skeleton(json.ReadSkeletonData("Data/" + name + ".json"));
			skeleton.SetSlotsToSetupPose();

			// Define mixing between animations.
			stateData = new AnimationStateData(skeleton.Data);
			state = new AnimationState(stateData);

			skeleton.X = 0;
			skeleton.Y = 0.1f;
			skeleton.UpdateWorldTransform();

			drawtime = new Stopwatch();
			drawtime.Start();
		}


		public void Draw2D(float X, float Y) {
			float dt = drawtime.ElapsedMilliseconds / 1000.0f;
			drawtime.Restart();

			state.Update(dt);
			state.Apply(skeleton);
			skeleton.X = X;
			skeleton.Y = Y;
			skeleton.RootBone.ScaleX = Scale;
			skeleton.RootBone.ScaleY = Scale;
			skeleton.UpdateWorldTransform();
			skeletonRenderer.Draw(skeleton);
		}

		public void Draw2D(float X, float Y, float width, float height) {
			float dt = drawtime.ElapsedMilliseconds / 1000.0f;
			drawtime.Restart();

			state.Update(dt);
			state.Apply(skeleton);
			skeleton.X = X;
			skeleton.Y = Y;
			float scaleX = skeleton.RootBone.ScaleX;
			float scaleY = skeleton.RootBone.ScaleY;
			skeleton.RootBone.ScaleX = width;
			skeleton.RootBone.ScaleY = height;
			skeleton.UpdateWorldTransform();
			skeletonRenderer.Draw(skeleton);
			skeleton.RootBone.ScaleX = scaleX;
			skeleton.RootBone.ScaleY = scaleY;
		}
	}
}
