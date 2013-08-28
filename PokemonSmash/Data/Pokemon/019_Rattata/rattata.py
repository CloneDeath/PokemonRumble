import Pokemon;
import Move;
import Color;
import Type;

Rattata = Pokemon.Add("rattata");
Rattata.DisplayName = "Rattata";
Rattata.Animation = "Pokemon/019_Rattata/rattata"
Rattata.HP = 30;
Rattata.Attack = 56;
Rattata.Defense = 35;
Rattata.SpecialAttack = 25;
Rattata.SpecialDefense = 35;
Rattata.Speed = 72;
Rattata.Color = Color.Purple;
Rattata.PrimaryType = Type.Normal;

Rattata.Width = .5;
Rattata.Height =  .5;
Rattata.Weight = 3.5;


Rattata.SetMix("walk", "idle", 0.6);
Rattata.SetMix("walk", "jump", 0.2);

Rattata.SetMix("jump", "walk", 0.1);
Rattata.SetMix("jump", "idle", 0.1);

Rattata.SetMix("idle", "jump", 0.2);
Rattata.SetMix("idle", "walk", 0.4);

Rattata.SetMix("idle", "dead", 1);
Rattata.SetMix("walk", "dead", 1);
Rattata.SetMix("jump", "dead", 1);