import Pokemon;
import Move;
import Color;
import Type;

Caterpie = Pokemon.Add("caterpie");
Caterpie.DisplayName = "Caterpie";
Caterpie.Animation = "Pokemon/010_Caterpie/caterpie"
Caterpie.HP = 45;
Caterpie.Attack = 30;
Caterpie.Defense = 35;
Caterpie.SpecialAttack = 20;
Caterpie.SpecialDefense = 20;
Caterpie.Speed = 45;
Caterpie.Color = Color.Green;
Caterpie.PrimaryType = Type.Bug;

Caterpie.Width = .5;
Caterpie.Height =  .5;
Caterpie.Weight = 2.9;

Caterpie.CanLearn("tackle");
Caterpie.CanLearn("stringshot");
Caterpie.CanLearn("bugbite");

Caterpie.SetMix("walk", "idle", 0.6);
Caterpie.SetMix("walk", "jump", 0.2);

Caterpie.SetMix("jump", "walk", 0.1);
Caterpie.SetMix("jump", "idle", 0.1);

Caterpie.SetMix("idle", "jump", 0.2);
Caterpie.SetMix("idle", "walk", 0.4);

Caterpie.SetMix("idle", "dead", 1);
Caterpie.SetMix("walk", "dead", 1);
Caterpie.SetMix("jump", "dead", 1);

Caterpie.AddAnimationAlias("tackle", "jump");