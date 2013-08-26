import Pokemon;
import Move;

Sandshrew = Pokemon.Add("sandshrew");
Sandshrew.DisplayName = "Sandshrew";
Sandshrew.Animation = "Pokemon/027_Sandshrew/sandshrew"
Sandshrew.HP = 50;
Sandshrew.Attack = 75;
Sandshrew.Defense = 85;
Sandshrew.SpecialAttack = 20;
Sandshrew.SpecialDefense = 30;
Sandshrew.Speed = 40;

Sandshrew.Width = .5;
Sandshrew.Height = .5;
Sandshrew.Weight = 12.0;


Sandshrew.SetMix("walk", "idle", 0.6);
Sandshrew.SetMix("walk", "jump", 0.2);

Sandshrew.SetMix("jump", "walk", 0.1);
Sandshrew.SetMix("jump", "idle", 0.1);

Sandshrew.SetMix("idle", "jump", 0.2);
Sandshrew.SetMix("idle", "walk", 0.4);

Sandshrew.SetMix("idle", "dead", 1);
Sandshrew.SetMix("walk", "dead", 1);
Sandshrew.SetMix("jump", "dead", 1);