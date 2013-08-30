using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonSmash.PokemonTypesData;

namespace PokemonSmash
{
	public static class PokemonType
	{
		public static PokemonTypeInfo None = new PokemonTypeInfo("[NONE]", @"Data\AttackTypes\Neutral.png", @"Data\MovePanels\Unknown.png");
		public static PokemonTypeInfo Normal = new PokemonTypeInfo("Normal", @"Data\PokemonTypes\Normal.png", @"Data\MovePanels\Normal.png");
		public static PokemonTypeInfo Fighting = new PokemonTypeInfo("Fighting", @"Data\PokemonTypes\Fighting.png", @"Data\MovePanels\Fighting.png");
		public static PokemonTypeInfo Flying = new PokemonTypeInfo("Flying", @"Data\PokemonTypes\Flying.png", @"Data\MovePanels\Flying.png");
		public static PokemonTypeInfo Poison = new PokemonTypeInfo("Poison", @"Data\PokemonTypes\Poison.png", @"Data\MovePanels\Poison.png");
		public static PokemonTypeInfo Ground = new PokemonTypeInfo("Ground", @"Data\PokemonTypes\Ground.png", @"Data\MovePanels\Ground.png");
		public static PokemonTypeInfo Rock = new PokemonTypeInfo("Rock", @"Data\PokemonTypes\Rock.png", @"Data\MovePanels\Rock.png");
		public static PokemonTypeInfo Bug = new PokemonTypeInfo("Bug", @"Data\PokemonTypes\Bug.png", @"Data\MovePanels\Bug.png");
		public static PokemonTypeInfo Ghost = new PokemonTypeInfo("Ghost", @"Data\PokemonTypes\Ghost.png", @"Data\MovePanels\Ghost.png");
		public static PokemonTypeInfo Steel = new PokemonTypeInfo("Steel", @"Data\PokemonTypes\Steel.png", @"Data\MovePanels\Steel.png");
		public static PokemonTypeInfo Fire = new PokemonTypeInfo("Fire", @"Data\PokemonTypes\Fire.png", @"Data\MovePanels\Fire.png");
		public static PokemonTypeInfo Water = new PokemonTypeInfo("Water", @"Data\PokemonTypes\Water.png", @"Data\MovePanels\Water.png");
		public static PokemonTypeInfo Grass = new PokemonTypeInfo("Grass", @"Data\PokemonTypes\Grass.png", @"Data\MovePanels\Grass.png");
		public static PokemonTypeInfo Electric = new PokemonTypeInfo("Electric", @"Data\PokemonTypes\Electric.png", @"Data\MovePanels\Electric.png");
		public static PokemonTypeInfo Psychic = new PokemonTypeInfo("Psychic", @"Data\PokemonTypes\Psychic.png", @"Data\MovePanels\Psychic.png");
		public static PokemonTypeInfo Ice = new PokemonTypeInfo("Ice", @"Data\PokemonTypes\Ice.png", @"Data\MovePanels\Ice.png");
		public static PokemonTypeInfo Dragon = new PokemonTypeInfo("Dragon", @"Data\PokemonTypes\Dragon.png", @"Data\MovePanels\Dragon.png");
		public static PokemonTypeInfo Dark = new PokemonTypeInfo("Dark", @"Data\PokemonTypes\Dark.png", @"Data\MovePanels\Dark.png");
		public static PokemonTypeInfo Fairy = new PokemonTypeInfo("Fairy", @"Data\PokemonTypes\Unknown.png", @"Data\MovePanels\Unknown.png");
		public static PokemonTypeInfo Unknown = new PokemonTypeInfo("???", @"Data\PokemonTypes\Unknown.png", @"Data\MovePanels\Unknown.png");

		//Move Types
		public static PokemonMoveInfo Status = new PokemonMoveInfo("Status", @"Data\AttackTypes\Neutral.png");
		public static PokemonMoveInfo Physical = new PokemonMoveInfo("Physical", @"Data\AttackTypes\Physical.png");
		public static PokemonMoveInfo Special = new PokemonMoveInfo("Special", @"Data\AttackTypes\Special.png");
	}
}
