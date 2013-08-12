using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Input;
using GLImp;

namespace PokemonRumble.Input {
	class Button {
		enum ButtonType {
			Key, JSButton, Axis
		}

		Key key;
		int btn;
		JoystickAxis axis;
		ButtonType buttonType;


		public Button(Key key) {
			this.key = key;
			buttonType = ButtonType.Key;
		}

		public Button(int JoystickButton) {
			btn = JoystickButton;
			buttonType = ButtonType.JSButton;
		}

		public Button(JoystickAxis Axis) {
			axis = Axis;
			buttonType = ButtonType.Axis;
		}

		public bool IsDown() {
			switch (buttonType) {
				case ButtonType.Axis:
					return axis.IsDown();
				case ButtonType.JSButton:
					return JoystickManager.IsDown(0, btn);
				case ButtonType.Key:
					return KeyboardManager.IsDown(key);
				default:
					return false;
			}
		}

		public bool IsPressed() {
			switch (buttonType) {
				case ButtonType.Axis:
					return axis.IsPressed();
				case ButtonType.JSButton:
					return JoystickManager.IsPressed(0, btn);
				case ButtonType.Key:
					return KeyboardManager.IsPressed(key);
				default:
					return false;
			}
		}

		public bool IsReleased() {
			switch (buttonType) {
				case ButtonType.Axis:
					return axis.IsReleased();
				case ButtonType.JSButton:
					return JoystickManager.IsReleased(0, btn);
				case ButtonType.Key:
					return KeyboardManager.IsReleased(key);
				default:
					return false;
			}
		}
	}
}
