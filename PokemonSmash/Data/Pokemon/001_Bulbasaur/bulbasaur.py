import Pokemon;
import Color;
import Type;

Bulbasaur = Pokemon.Add("bulbasaur");
Bulbasaur.DisplayName = "Bulbasaur";
Bulbasaur.Animation = "Pokemon/001_Bulbasaur/bulbasaur"
Bulbasaur.HP = 45;
Bulbasaur.Attack = 49;
Bulbasaur.Defense = 49;
Bulbasaur.SpecialAttack = 65;
Bulbasaur.SpecialDefense = 65;
Bulbasaur.Speed = 45;
Bulbasaur.Color = Color.Green;
Bulbasaur.PrimaryType = Type.Grass;
Bulbasaur.SecondaryType = Type.Poison;

Bulbasaur.Width = .8;
Bulbasaur.Height = .5;
Bulbasaur.Weight = 6.9;
 
Bulbasaur.CanLearn("tackle");
Bulbasaur.CanLearn("growl");
Bulbasaur.CanLearn("leechseed");
Bulbasaur.CanLearn("vinewhip");
Bulbasaur.CanLearn("poisonpowder");
Bulbasaur.CanLearn("sleeppowder");
Bulbasaur.CanLearn("takedown");
Bulbasaur.CanLearn("razorleaf");
Bulbasaur.CanLearn("sweetscent");
Bulbasaur.CanLearn("growth");
Bulbasaur.CanLearn("doubleedge");
Bulbasaur.CanLearn("worryseed");
Bulbasaur.CanLearn("synthesis");
Bulbasaur.CanLearn("seedbomb");

Bulbasaur.AddAnimationAlias("tackle", "jump");
Bulbasaur.AddAnimationAlias("vinewhip", "leech_seed");

Bulbasaur.SetMix("walk", "idle", 0.6);
Bulbasaur.SetMix("walk", "jump", 0.2);
Bulbasaur.SetMix("walk", "leech_seed", 0.2);

Bulbasaur.SetMix("jump", "walk", 0.1);
Bulbasaur.SetMix("jump", "idle", 0.1);

Bulbasaur.SetMix("idle", "jump", 0.2);
Bulbasaur.SetMix("idle", "walk", 0.4);
Bulbasaur.SetMix("idle", "leech_seed", 0.2);

Bulbasaur.SetMix("leech_seed", "walk", 0.2);
Bulbasaur.SetMix("leech_seed", "idle", 0.2);

Bulbasaur.SetMix("idle", "dead", 1);
Bulbasaur.SetMix("walk", "dead", 1);
Bulbasaur.SetMix("jump", "dead", 1);
Bulbasaur.SetMix("leech_seed", "dead", 1);