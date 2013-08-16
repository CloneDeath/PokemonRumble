using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;
using GLImp;

namespace PokemonRumble {
	class ResourceManager {
		public static SkeletonRenderer skeletonRenderer;
		public static Skeleton skeleton;
		public static AnimationState state;
		internal static void Initialize() {
			skeletonRenderer = new SkeletonRenderer();

			String name = "Pokemon/bulbasaur"; // "goblins";

			Atlas atlas = new Atlas("Data/" + name + ".atlas", new GLImpTextureLoader());
			SkeletonJson json = new SkeletonJson(atlas);
			skeleton = new Skeleton(json.ReadSkeletonData("Data/" + name + ".json"));
			if (name == "goblins") skeleton.SetSkin("goblingirl");
			skeleton.SetSlotsToSetupPose(); // Without this the skin attachments won't be attached. See SetSkin.

			// Define mixing between animations.
			AnimationStateData stateData = new AnimationStateData(skeleton.Data);
			if (name == "bulbasaur") {
				stateData.SetMix("walk", "idle", 0.2f);
				stateData.SetMix("idle", "walk", 0.4f);
			}

			state = new AnimationState(stateData);
			state.SetAnimation("walk", false);
			state.AddAnimation("idle", false);
			state.AddAnimation("walk", true);

			//skeleton.X = 320;
			//skeleton.Y = 440;
			//skeleton.UpdateWorldTransform();

			skeleton.X = 0;
			skeleton.Y = 0.1f;
			skeleton.UpdateWorldTransform();
		}

		
		public static Texture Shadow = new Texture(@"Data\shadow.png");
	}
}
