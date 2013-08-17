from Pokemon import *;
from Move import *;

Bulbasaur = Pokemon.Add("bulbasaur");
Bulbasaur.DisplayName = "Bulbasaur";
Bulbasaur.Speed = 3;
Bulbasaur.HP = 100;

Bulbasaur.Move[0] = Move.Find("tackle");
Bulbasaur.Move[1] = Move.Find("leechseed");
Bulbasaur.Move[2] = Move.Find("growl");
Bulbasaur.Move[3] = Move.Find("razorleaf");

Bulbasaur.AddAnimationAlias("tackle", "jump");

Bulbasaur.SetMix("walk", "idle", 0.6);
Bulbasaur.SetMix("idle", "walk", 0.4);

Bulbasaur.SetMix("jump", "walk", 0.4);
Bulbasaur.SetMix("walk", "jump", 0.2);

Bulbasaur.SetMix("idle", "jump", 0.2);
Bulbasaur.SetMix("jump", "idle", 0.4);

Bulbasaur.SetMix("idle", "dead", 2);
Bulbasaur.SetMix("walk", "dead", 2);
Bulbasaur.SetMix("jump", "dead", 2);