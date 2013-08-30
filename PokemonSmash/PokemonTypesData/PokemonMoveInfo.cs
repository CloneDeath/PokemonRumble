using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;

namespace PokemonSmash.PokemonTypesData
{
	public class PokemonMoveInfo
	{
		public string Name;
		public Texture Image;

		public PokemonMoveInfo(string Displayname, string Imagelocation)
		{
			this.Name = Displayname;
			this.Image = new Texture(Imagelocation);
		}
	}
}
