using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;

namespace PokemonRumble.Input {
	public class JoystickButton {
		int JoystickNumber;
		int ButtonNumber;

		public JoystickButton(int JoystickNumber, int ButtonNumber) {
			this.JoystickNumber = JoystickNumber;
			this.ButtonNumber = ButtonNumber;
		}

		public bool IsDown() {
			return JoystickManager.IsDown(JoystickNumber, ButtonNumber);
		}

		public bool IsUp() {
			return JoystickManager.IsUp(JoystickNumber, ButtonNumber);
		}

		public bool IsPressed() {
			return JoystickManager.IsPressed(JoystickNumber, ButtonNumber);
		}

		public bool IsReleased() {
			return JoystickManager.IsReleased(JoystickNumber, ButtonNumber);
		}
	}
}
