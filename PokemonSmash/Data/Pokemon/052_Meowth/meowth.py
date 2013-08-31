import Pokemon;
import Move;
import Color;
import Type;

Meowth = Pokemon.Add("meowth");
Meowth.DisplayName = "Meowth";
Meowth.Animation = "Pokemon/052_Meowth/meowth"
Meowth.HP = 40;
Meowth.Attack = 45;
Meowth.Defense = 35;
Meowth.SpecialAttack = 40;
Meowth.SpecialDefense = 40;
Meowth.Speed = 90;
Meowth.Color = Color.Yellow;
Meowth.PrimaryType = Type.Normal;

Meowth.Width = .5;
Meowth.Height = .5;
Meowth.Weight = 4.2;

Meowth.SetMix("walk", "idle", 0.6);
Meowth.SetMix("walk", "jump", 0.2);

Meowth.SetMix("jump", "walk", 0.1);
Meowth.SetMix("jump", "idle", 0.1);

Meowth.SetMix("idle", "jump", 0.2);
Meowth.SetMix("idle", "walk", 0.4);

Meowth.SetMix("idle", "dead", 1);
Meowth.SetMix("walk", "dead", 1);
Meowth.SetMix("jump", "dead", 1);