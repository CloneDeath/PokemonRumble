using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;

namespace PokemonSmash.Input {
	public class JoystickAxis {
		int AxisNumber;
		double Threshold;
		bool Previous = false;
		int JoystickNumber;

		public JoystickAxis(int JoystickNumber, int AxisNumber, double Threshold) {
			this.JoystickNumber = JoystickNumber;
			this.AxisNumber = AxisNumber;
			this.Threshold = Threshold;


			GraphicsManager.Update += Update;
		}

		public bool IsDown() {
			float Axis = JoystickManager.GetAxis(JoystickNumber, AxisNumber);
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
