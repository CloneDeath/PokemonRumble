using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;
using GLImp;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using System.IO;
using PokemonSmash.IronInterface;
using System.Drawing;
using System.Reflection;

namespace PokemonSmash {
	class ResourceManager {
		internal static void Initialize() {
			InitializeScripts();
		}


		public delegate object AllFunctions(params object[] input);
		private static void CreateModuleFromClass(ScriptEngine engine, Type t, string ModuleName)
		{
			var Module = engine.CreateModule(ModuleName);
			foreach (MemberInfo m in t.GetMembers(BindingFlags.Public | BindingFlags.Static)){
				
				if (m.MemberType == MemberTypes.Method){
					MethodInfo method = (MethodInfo)m;
					Module.SetVariable(m.Name, (AllFunctions)delegate(object[] input){ return method.Invoke(null, input);});
					continue;
				}

				if (m.MemberType == MemberTypes.Property){
					MethodInfo method = ((PropertyInfo)m).GetGetMethod();
					Module.SetVariable(m.Name, method.Invoke(null, null));
					continue;
				}

				Console.WriteLine("Not supported: " + m.Name + " for module: " + ModuleName);
			}
		}

		private static void CreateModuleFromClass(ScriptEngine engine, object t, string ModuleName)
		{
			var Module = engine.CreateModule(ModuleName);
			foreach (MemberInfo m in t.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)) {

				if (m.MemberType == MemberTypes.Method) {
					MethodInfo method = (MethodInfo)m;
					Module.SetVariable(m.Name, (AllFunctions)delegate(object[] input) { return method.Invoke(t, input); });
					continue;
				}

				if (m.MemberType == MemberTypes.Property) {
					MethodInfo method = ((PropertyInfo)m).GetGetMethod();
					Module.SetVariable(m.Name, method.Invoke(t, null));
					continue;
				}

				Console.WriteLine("Not supported: " + m.Name + " for module: " + ModuleName);
			}
		}

		private static void CreateModuleFromEnum(ScriptEngine engine, Type t, string ModuleName)
		{
			var Module = engine.CreateModule(ModuleName);
			foreach (MemberInfo m in t.GetMembers(BindingFlags.Public | BindingFlags.Static)) {
				if (m.MemberType == MemberTypes.Field) {
					Module.SetVariable(m.Name, ((FieldInfo)m).GetValue(null));
					continue;
				}

				Console.WriteLine("Not supported: " + m.Name + " for module: " + ModuleName);
			}
		}

		

		private static void InitializeScripts() {
			ScriptEngine engine = Python.CreateEngine();

			ScriptScope scope = engine.Runtime.CreateScope();

			CreateModuleFromClass(engine, typeof(IronPokemon), "Pokemon");
			CreateModuleFromClass(engine, typeof(Color), "Color");
			CreateModuleFromClass(engine, typeof(IronMove), "Move");
			CreateModuleFromClass(engine, new Random(), "Random");
			CreateModuleFromEnum(engine, typeof(PokemonType), "Type");

			RecursivelyRunScriptsIn(@"Data\", engine, scope);			
		}

		private static void RecursivelyRunScriptsIn(string Location, ScriptEngine engine, ScriptScope scope) {
			foreach (string file in Directory.GetFiles(Location)) {
				if (Path.GetExtension(file) == ".py") {
					ScriptScope script = engine.ExecuteFile(file, scope);
				}
			}

			foreach (string Dir in Directory.GetDirectories(Location)) {
				RecursivelyRunScriptsIn(Dir, engine, scope);
			}
		}

		
		public static Texture Shadow = new Texture(@"Data\shadow.png");
	}
}
