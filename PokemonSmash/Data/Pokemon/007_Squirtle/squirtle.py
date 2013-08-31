import Pokemon;
import Move;
import Color;
import Type;

Squirtle = Pokemon.Add("squirtle");
Squirtle.DisplayName = "Squirtle";
Squirtle.Animation = "Pokemon/007_Squirtle/squirtle"
Squirtle.HP = 44;
Squirtle.Attack = 48;
Squirtle.Defense = 63;
Squirtle.SpecialAttack = 50;
Squirtle.SpecialDefense = 64;
Squirtle.Speed = 43;
Squirtle.Color = Color.Blue;
Squirtle.PrimaryType = Type.Water;

Squirtle.Width = .5;
Squirtle.Height =  .5;
Squirtle.Weight = 9.0;

Squirtle.CanLearn("tackle");
Squirtle.CanLearn("tailwhip");
Squirtle.CanLearn("bubble");
Squirtle.CanLearn("withdraw");
Squirtle.CanLearn("watergun");
Squirtle.CanLearn("bite");
Squirtle.CanLearn("rapidspin");
Squirtle.CanLearn("protect");
Squirtle.CanLearn("waterpulse");
Squirtle.CanLearn("aquatail");
Squirtle.CanLearn("skullbash");
Squirtle.CanLearn("irondefense");
Squirtle.CanLearn("raindance");
Squirtle.CanLearn("hydropump");

Squirtle.SetMix("walk", "idle", 0.6);
Squirtle.SetMix("walk", "jump", 0.2);

Squirtle.SetMix("jump", "walk", 0.1);
Squirtle.SetMix("jump", "idle", 0.1);

Squirtle.SetMix("idle", "jump", 0.2);
Squirtle.SetMix("idle", "walk", 0.4);

Squirtle.SetMix("idle", "dead", 1);
Squirtle.SetMix("walk", "dead", 1);
Squirtle.SetMix("jump", "dead", 1);

Squirtle.AddAnimationAlias("tackle", "jump");
Squirtle.AddAnimationAlias("tailwhip", "tail whip");