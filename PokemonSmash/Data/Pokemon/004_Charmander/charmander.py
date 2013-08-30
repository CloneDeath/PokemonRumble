import Pokemon;
import Color;
import Type;

Charmander = Pokemon.Add("charmander");
Charmander.DisplayName = "Charmander";
Charmander.Animation = "Pokemon/004_Charmander/charmander"
Charmander.HP = 39;
Charmander.Attack = 52;
Charmander.Defense = 43;
Charmander.SpecialAttack = 60;
Charmander.SpecialDefense = 50;
Charmander.Speed = 65;
Charmander.Color = Color.Red;
Charmander.PrimaryType = Type.Fire;

Charmander.Width = .4;
Charmander.Height =  .6;
Charmander.Weight = 8.5;

Charmander.CanLearn("scratch");
Charmander.CanLearn("growl");
Charmander.CanLearn("ember");
Charmander.CanLearn("smokescreen");
Charmander.CanLearn("dragonrage");
Charmander.CanLearn("scaryface");
Charmander.CanLearn("firefang");
Charmander.CanLearn("flameburst");
Charmander.CanLearn("slash");
Charmander.CanLearn("flamethrower");
Charmander.CanLearn("firespin");
Charmander.CanLearn("inferno");


Charmander.SetMix("walk", "idle", 0.6);
Charmander.SetMix("walk", "jump", 0.2);

Charmander.SetMix("jump", "walk", 0.1);
Charmander.SetMix("jump", "idle", 0.1);

Charmander.SetMix("idle", "jump", 0.2);
Charmander.SetMix("idle", "walk", 0.4);

Charmander.SetMix("idle", "dead", 1);
Charmander.SetMix("walk", "dead", 1);
Charmander.SetMix("jump", "dead", 1);