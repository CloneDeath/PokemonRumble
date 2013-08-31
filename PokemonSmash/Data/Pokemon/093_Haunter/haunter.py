import Pokemon;
import Move;
import Color;
import Type;

Haunter = Pokemon.Add("haunter");
Haunter.DisplayName = "Haunter";
Haunter.Animation = "Pokemon/093_Haunter/haunter"
Haunter.HP = 45;
Haunter.Attack = 50;
Haunter.Defense = 45;
Haunter.SpecialAttack = 115;
Haunter.SpecialDefense = 55;
Haunter.Speed = 95;
Haunter.Color = Color.Purple;
Haunter.PrimaryType = Type.Ghost;
Haunter.SecondaryType = Type.Poison;

Haunter.Hovers = True;
Haunter.CastsShadow = False;

Haunter.CanLearn("hypnosis");
Haunter.CanLearn("lick");
Haunter.CanLearn("spite");
Haunter.CanLearn("meanlook");
Haunter.CanLearn("curse");
Haunter.CanLearn("nightshade");
Haunter.CanLearn("confuseray");
Haunter.CanLearn("suckerpunch");
Haunter.CanLearn("shadowpunch");
Haunter.CanLearn("payback");
Haunter.CanLearn("shadowball");
Haunter.CanLearn("dreameater");
Haunter.CanLearn("darkpulse");
Haunter.CanLearn("destinybond");
Haunter.CanLearn("hex");
Haunter.CanLearn("nightmare");

Haunter.AddAnimationAlias("hypnosis", "idle");

Haunter.Width = .5;
Haunter.Height =  .5;
Haunter.Weight = 0.1;
