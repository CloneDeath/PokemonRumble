using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;

namespace PokemonRumble.Input {
	class JoystickAxis {
		int AxisNumber;
		double Threshold;
		bool Previous = false;

		public JoystickAxis(int AxisNumber, double Threshold) {
			this.AxisNumber = AxisNumber;
			this.Threshold = Threshold;


			GraphicsManager.Update += Update;
		}

		public bool IsDown() {
			float Axis = JoystickManager.GetAxis(0, AxisNumber);
			//Ensure that it is the same sign as the threshold, and beyond it.
			return (Math.Abs(Axis) >= Math.Abs(Threshold)) 
				&& (Math.Sign(Axis) == Math.Sign(Threshold));
		}

		public bool IsUp() {
			return !IsDown();
		}

		public bool IsPressed() {
			return IsDown() && !Previous;
		}

		public bool IsReleased() {
			return Previous && !IsDown();
		}

		private void Update() {
			Previous = IsDown();
		}
	}
}
