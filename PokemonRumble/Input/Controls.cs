using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using OpenTK.Input;

namespace PokemonRumble.Input {
	class Controls {
		static Dictionary<Control, List<Button>> Keymaps = new Dictionary<Control, List<Button>>();

		static Controls() {
			//Joystick controls are for a PS3 controller configured as a PS2 controller.
			//This won't be so specific once we add a menu to change controls.
			AddKeymap(Control.Up, Key.Up);
			AddKeymap(Control.Up, new JoystickAxis(6, 0.5));

			AddKeymap(Control.Down, Key.Down);
			AddKeymap(Control.Down, new JoystickAxis(6, -0.5));

			AddKeymap(Control.Left, Key.Left);
			AddKeymap(Control.Left, new JoystickAxis(5, -0.5));

			AddKeymap(Control.Right, Key.Right);
			AddKeymap(Control.Right, new JoystickAxis(5, 0.5));

			AddKeymap(Control.Jump, Key.Space);
			AddKeymap(Control.Jump, Key.D);
			AddKeymap(Control.Jump, 0);

			AddKeymap(Control.Attack, Key.LShift);
			AddKeymap(Control.Attack, Key.F);
			AddKeymap(Control.Attack, 2);

			AddKeymap(Control.BackDash, Key.LControl);
			AddKeymap(Control.BackDash, Key.A);
			AddKeymap(Control.BackDash, 4);

			AddKeymap(Control.Pause, Key.Escape);
			AddKeymap(Control.Pause, 7);

			AddKeymap(Control.Accept, Key.Enter);
			AddKeymap(Control.Accept, Key.Space);
			AddKeymap(Control.Accept, Key.F);
			AddKeymap(Control.Accept, 0);

			AddKeymap(Control.Cancel, Key.Escape);
			AddKeymap(Control.Cancel, 1);
		}

		public static void AddKeymap(Control control, Key key) {
			AddKeymap(control, new Button(key));
		}

		public static void AddKeymap(Control control, int btn) {
			AddKeymap(control, new Button(btn));
		}

		public static void AddKeymap(Control control, JoystickAxis jsa) {
			AddKeymap(control, new Button(jsa));
		}

		public static void AddKeymap(Control control, Button button) {
			if (Keymaps.ContainsKey(control)) {
				Keymaps[control].Add(button);
			} else {
				Keymaps.Add(control, new List<Button>());
				Keymaps[control].Add(button);
			}
		}

		public static bool IsDown(Control control) {
			List<Button> Buttons = Keymaps[control];
			foreach (Button b in Buttons) {
				if (b.IsDown()) {
					return true;
				}
			}
			return false;
		}

		public static bool IsUp(Control control) {
			return !IsDown(control);
		}

		public static bool IsPressed(Control control) {
			List<Button> Buttons = Keymaps[control];
			foreach (Button b in Buttons) {
				if (b.IsPressed()) {
					return true;
				}
			}
			return false;
		}

		public static bool IsReleased(Control control) {
			List<Button> Buttons = Keymaps[control];
			foreach (Button b in Buttons) {
				if (b.IsReleased()) {
					return true;
				}
			}
			return false;
		}

		//public static List<Control> GetPressed() {
		//    List<Control> ret = new List<Control>();
		//    foreach (Key k in KeyboardManager.GetAllPressedKeys()) {
		//        if (translate.Keys.Contains(k)) {
		//            ret.Add(translate[k]);
		//        }
		//    }

		//    return ret;
		//}

		//public static List<Control> GetReleased() {
		//    List<Control> ret = new List<Control>();
		//    foreach (Key k in KeyboardManager.GetAllReleasedKeys()) {
		//        if (translate.Keys.Contains(k)) {
		//            ret.Add(translate[k]);
		//        }
		//    }

		//    return ret;
		//}
	}
}
