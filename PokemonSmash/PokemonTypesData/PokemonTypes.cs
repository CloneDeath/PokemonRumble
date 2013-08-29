using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonSmash.PokemonTypesData;

namespace PokemonSmash
{
	public static class PokemonType
	{
		public static PokemonTypeInfo None = new PokemonTypeInfo("[NONE]", @"Data\AttackTypes\Neutral.png");
		public static PokemonTypeInfo Normal = new PokemonTypeInfo("Normal", @"Data\PokemonTypes\Normal.png");
		public static PokemonTypeInfo Fighting = new PokemonTypeInfo("Fighting", @"Data\PokemonTypes\Fighting.png");
		public static PokemonTypeInfo Flying = new PokemonTypeInfo("Flying", @"Data\PokemonTypes\Flying.png");
		public static PokemonTypeInfo Poison = new PokemonTypeInfo("Poison", @"Data\PokemonTypes\Poison.png");
		public static PokemonTypeInfo Ground = new PokemonTypeInfo("Ground", @"Data\PokemonTypes\Ground.png");
		public static PokemonTypeInfo Rock = new PokemonTypeInfo("Rock", @"Data\PokemonTypes\Rock.png");
		public static PokemonTypeInfo Bug = new PokemonTypeInfo("Bug", @"Data\PokemonTypes\Bug.png");
		public static PokemonTypeInfo Ghost = new PokemonTypeInfo("Ghost", @"Data\PokemonTypes\Ghost.png");
		public static PokemonTypeInfo Steel = new PokemonTypeInfo("Steel", @"Data\PokemonTypes\Steel.png");
		public static PokemonTypeInfo Fire = new PokemonTypeInfo("Fire", @"Data\PokemonTypes\Fire.png");
		public static PokemonTypeInfo Water = new PokemonTypeInfo("Water", @"Data\PokemonTypes\Water.png");
		public static PokemonTypeInfo Grass = new PokemonTypeInfo("Grass", @"Data\PokemonTypes\Grass.png");
		public static PokemonTypeInfo Electric = new PokemonTypeInfo("Electric", @"Data\PokemonTypes\Electric.png");
		public static PokemonTypeInfo Psychic = new PokemonTypeInfo("Psychic", @"Data\PokemonTypes\Psychic.png");
		public static PokemonTypeInfo Ice = new PokemonTypeInfo("Ice", @"Data\PokemonTypes\Ice.png");
		public static PokemonTypeInfo Dragon = new PokemonTypeInfo("Dragon", @"Data\PokemonTypes\Dragon.png");
		public static PokemonTypeInfo Dark = new PokemonTypeInfo("Dark", @"Data\PokemonTypes\Dark.png");
		public static PokemonTypeInfo Fairy = new PokemonTypeInfo("Fairy", @"Data\PokemonTypes\Unknown.png");
		public static PokemonTypeInfo Unknown = new PokemonTypeInfo("???", @"Data\PokemonTypes\Unknown.png");
	}
}
