using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;
using GLImp;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using System.IO;

namespace PokemonRumble {
	class ResourceManager {
		public static SkeletonRenderer skeletonRenderer;
		public static Skeleton skeleton;
		public static AnimationState state;
		internal static void Initialize() {
			InitializeScripts();
			InitializeSkeletons();
		}

		private static void InitializeSkeletons() {
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

		private static void InitializeScripts() {
			ScriptEngine engine = Python.CreateEngine();

			ScriptScope scope = engine.Runtime.CreateScope();
			//scope.SetVariable("progress", ProgressModule.Instance);
			//scope.SetVariable("spawns", SpawnsModule.Instance);

			RecursivelyRunScriptsIn(@"Data\", engine, scope);
			
			

			
		}

		private static void RecursivelyRunScriptsIn(string Location, ScriptEngine engine, ScriptScope scope) {
			foreach (string file in Directory.GetFiles(Location)) {
				if (Path.GetExtension(file) == ".py") {
					ScriptScope script = engine.ExecuteFile(file, scope);
					//PowerResult result1 = script.powerUpRack();
					//PowerResult result2 = script.powerDownRack();
				}
			}

			foreach (string Dir in Directory.GetDirectories(Location)) {
				RecursivelyRunScriptsIn(Dir, engine, scope);
			}
		}

		
		public static Texture Shadow = new Texture(@"Data\shadow.png");
	}
}
