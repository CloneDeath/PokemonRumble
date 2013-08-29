using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;

namespace PokemonSmash.PokemonTypesData
{
	public class PokemonTypeInfo
	{
		public string DisplayName;
		public Texture Image;

		public PokemonTypeInfo(string Name, string FileLocation)
		{
			this.DisplayName = Name;
			this.Image = new Texture(FileLocation);
		}
	}
}
