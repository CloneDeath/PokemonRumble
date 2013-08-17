using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;

namespace PokemonRumble {
	class Animation {
		public SkeletonRenderer skeletonRenderer;
		public Skeleton skeleton;
		public AnimationState state;
		public AnimationStateData stateData;

		public Animation(string AnimationFile) {
			skeletonRenderer = new SkeletonRenderer();

			String name = AnimationFile;

			Atlas atlas = new Atlas("Data/" + name + ".atlas", new GLImpTextureLoader());
			SkeletonJson json = new SkeletonJson(atlas);
			skeleton = new Skeleton(json.ReadSkeletonData("Data/" + name + ".json"));
			skeleton.SetSlotsToSetupPose();

			// Define mixing between animations.
			stateData = new AnimationStateData(skeleton.Data);
			state = new AnimationState(stateData);
			state.SetAnimation("idle", true);

			skeleton.X = 0;
			skeleton.Y = 0.1f;
			skeleton.UpdateWorldTransform();
		}
	}
}
