using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;
using OpenTK.Input;

namespace PokemonSmash.Input {
	class Controls {
		public static ControlSet Player1 = new ControlSet();
		public static ControlSet Player2 = new ControlSet();

		static Controls() {
			Player1.AddKeymap(Control.Accept,	new JoystickButton(0, 0));
			Player1.AddKeymap(Control.Cancel,	new JoystickButton(0, 1));
			Player1.AddKeymap(Control.Down,		new JoystickAxis(0, 6, -0.5));
			Player1.AddKeymap(Control.Left,		new JoystickAxis(0, 5, -0.5));
			Player1.AddKeymap(Control.Right,	new JoystickAxis(0, 5, 0.5));
			Player1.AddKeymap(Control.Up,		new JoystickAxis(0, 6, 0.5));
			Player1.AddKeymap(Control.Jump,		new JoystickAxis(0, 6, 0.5));
			Player1.AddKeymap(Control.Move0,	new JoystickButton(0, 0));
			Player1.AddKeymap(Control.Move1,	new JoystickButton(0, 2));
			Player1.AddKeymap(Control.Move2,	new JoystickButton(0, 1));
			Player1.AddKeymap(Control.Move3,	new JoystickButton(0, 3));
			Player1.AddKeymap(Control.Pause,	new JoystickButton(0, 7));


			Player2.AddKeymap(Control.Accept,	new JoystickButton(1, 0));
			Player2.AddKeymap(Control.Cancel,	new JoystickButton(1, 1));
			Player2.AddKeymap(Control.Down,		new JoystickAxis(1, 6, -0.5));
			Player2.AddKeymap(Control.Left,		new JoystickAxis(1, 5, -0.5));
			Player2.AddKeymap(Control.Right,	new JoystickAxis(1, 5, 0.5));
			Player2.AddKeymap(Control.Up,		new JoystickAxis(1, 6, 0.5));
			Player2.AddKeymap(Control.Jump,		new JoystickAxis(1, 6, 0.5));
			Player2.AddKeymap(Control.Move0,	new JoystickButton(1, 0));
			Player2.AddKeymap(Control.Move1,	new JoystickButton(1, 2));
			Player2.AddKeymap(Control.Move2,	new JoystickButton(1, 1));
			Player2.AddKeymap(Control.Move3,	new JoystickButton(1, 3));
			Player1.AddKeymap(Control.Pause,	new JoystickButton(1, 7));
		}
	}
}
