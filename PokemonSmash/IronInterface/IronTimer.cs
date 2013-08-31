using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using IronPython.Runtime;

namespace PokemonSmash.IronInterface
{
	public static class IronTimer
	{
		public static void Schedule(PythonFunction func, double Time)
		{
			Timer timer = new Timer();
			timer.Interval = Time * 1000;
			timer.Elapsed += delegate(object sender, ElapsedEventArgs args){
				timer.Stop();
				ResourceManager.Engine.Operations.Invoke(func);
			};
			timer.Start();
		}
	}
}
