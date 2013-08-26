import Pokemon;
import Move;

Diglett = Pokemon.Add("diglett");
Diglett.DisplayName = "Diglett";
Diglett.Animation = "Pokemon/050_Diglett/diglett"
Diglett.HP = 10;
Diglett.Attack = 55;
Diglett.Defense = 25;
Diglett.SpecialAttack = 35;
Diglett.SpecialDefense = 45;
Diglett.Speed = 95;

Diglett.CanJump = False;
Diglett.CastsShadow = False;

Diglett.Width = .5;
Diglett.Height =  .5;
Diglett.Weight = 0.8;

Diglett.SetMix("walk", "idle", 0.6);

Diglett.SetMix("idle", "walk", 0.4);

Diglett.SetMix("idle", "dead", 1);
Diglett.SetMix("walk", "dead", 1);