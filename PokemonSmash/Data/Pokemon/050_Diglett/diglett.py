import Pokemon;
import Move;
import Color;
import Type;

Diglett = Pokemon.Add("diglett");
Diglett.DisplayName = "Diglett";
Diglett.Animation = "Pokemon/050_Diglett/diglett"
Diglett.HP = 10;
Diglett.Attack = 55;
Diglett.Defense = 25;
Diglett.SpecialAttack = 35;
Diglett.SpecialDefense = 45;
Diglett.Speed = 95;
Diglett.Color = Color.Brown;
Diglett.PrimaryType = Type.Ground;

Diglett.CanJump = False;
Diglett.CastsShadow = False;

Diglett.CanLearn("scratch");
Diglett.CanLearn("sandattack");
Diglett.CanLearn("growl");
Diglett.CanLearn("astonish");
Diglett.CanLearn("mudslap");
Diglett.CanLearn("magnitude");
Diglett.CanLearn("bulldoze");
Diglett.CanLearn("suckerpunch");
Diglett.CanLearn("mudbomb");
Diglett.CanLearn("earthpower");
Diglett.CanLearn("dig");
Diglett.CanLearn("slash");
Diglett.CanLearn("earthquake");
Diglett.CanLearn("fissure");

Diglett.Width = .5;
Diglett.Height =  .5;
Diglett.Weight = 0.8;

Diglett.SetMix("walk", "idle", 0.6);

Diglett.SetMix("idle", "walk", 0.4);

Diglett.SetMix("idle", "dead", 1);
Diglett.SetMix("walk", "dead", 1);