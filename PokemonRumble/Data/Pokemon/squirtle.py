from Pokemon import *;
from Move import *;

Squirtle = Pokemon.Add("squirtle");
Squirtle.DisplayName = "Squirtle";
Squirtle.Animation = "Pokemon/squirtle"
Squirtle.HP = 44;
Squirtle.Attack = 48;
Squirtle.Defense = 63;
Squirtle.SpecialAttack = 50;
Squirtle.SpecialDefense = 64;
Squirtle.Speed = 43;

Squirtle.Width = .5;
Squirtle.Height =  .5;
Squirtle.Weight = 9.0;

Squirtle.Move[0] = Move.Find("tackle");
Squirtle.Move[1] = Move.Find("bubble");
Squirtle.Move[2] = Move.Find("withdraw");
Squirtle.Move[3] = Move.Find("tailwhip");

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