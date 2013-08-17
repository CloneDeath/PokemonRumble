using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Input;

namespace PokemonRumble.Input {
	public class ControlSet {
		Dictionary<Control, List<Button>> Keymaps = new Dictionary<Control, List<Button>>();

		public ControlSet() {
		}

		public void AddKeymap(Control control, Key key) {
			AddKeymap(control, new Button(key));
		}

		public void AddKeymap(Control control, JoystickButton btn) {
			AddKeymap(control, new Button(btn));
		}

		public void AddKeymap(Control control, JoystickAxis jsa) {
			AddKeymap(control, new Button(jsa));
		}

		private void AddKeymap(Control control, Button button) {
			if (Keymaps.ContainsKey(control)) {
				Keymaps[control].Add(button);
			} else {
				Keymaps.Add(control, new List<Button>());
				Keymaps[control].Add(button);
			}
		}

		public bool IsDown(Control control) {
			List<Button> Buttons = Keymaps[control];
			foreach (Button b in Buttons) {
				if (b.IsDown()) {
					return true;
				}
			}
			return false;
		}

		public bool IsUp(Control control) {
			return !IsDown(control);
		}

		public bool IsPressed(Control control) {
			List<Button> Buttons = Keymaps[control];
			foreach (Button b in Buttons) {
				if (b.IsPressed()) {
					return true;
				}
			}
			return false;
		}

		public bool IsReleased(Control control) {
			List<Button> Buttons = Keymaps[control];
			foreach (Button b in Buttons) {
				if (b.IsReleased()) {
					return true;
				}
			}
			return false;
		}
	}
}
