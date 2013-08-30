using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLImp;

namespace PokemonSmash.PokemonTypesData
{
	public class PokemonTypeInfo
	{
		public string DisplayName { get; private set; }
		public Texture Image { get; private set; }
		public Texture Panel { get; private set; }

		public PokemonTypeInfo(string Name, string FileLocation, string PanelLocation)
		{
			this.DisplayName = Name;
			this.Image = new Texture(FileLocation);
			this.Panel = new Texture(PanelLocation);
		}
	}
}
