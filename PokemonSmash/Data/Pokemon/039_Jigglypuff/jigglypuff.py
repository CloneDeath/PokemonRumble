import Pokemon;
import Move;
import Color;
import Type;

Jigglypuff = Pokemon.Add("jigglypuff");
Jigglypuff.DisplayName = "Jigglypuff";
Jigglypuff.Animation = "Pokemon/039_Jigglypuff/jigglypuff"
Jigglypuff.HP = 115;
Jigglypuff.Attack = 45;
Jigglypuff.Defense = 20;
Jigglypuff.SpecialAttack = 45;
Jigglypuff.SpecialDefense = 25;
Jigglypuff.Speed = 20;
Jigglypuff.Color = Color.Pink;
Jigglypuff.PrimaryType = Type.Normal;
Jigglypuff.SecondaryType = Type.Fairy;

Jigglypuff.Width = .5;
Jigglypuff.Height = .5;
Jigglypuff.Weight = 5.5;

Jigglypuff.CanLearn("sing");
Jigglypuff.CanLearn("defensecurl");
Jigglypuff.CanLearn("pound");
Jigglypuff.CanLearn("disable");
Jigglypuff.CanLearn("round");
Jigglypuff.CanLearn("rollout");
Jigglypuff.CanLearn("doubleslap");
Jigglypuff.CanLearn("rest");
Jigglypuff.CanLearn("bodyslam");
Jigglypuff.CanLearn("gyroball");
Jigglypuff.CanLearn("wakeupslap");
Jigglypuff.CanLearn("mimic");
Jigglypuff.CanLearn("hypervoice");
Jigglypuff.CanLearn("doubleedge");


Jigglypuff.SetMix("walk", "idle", 0.6);
Jigglypuff.SetMix("walk", "jump", 0.2);

Jigglypuff.SetMix("jump", "walk", 0.1);
Jigglypuff.SetMix("jump", "idle", 0.1);

Jigglypuff.SetMix("idle", "jump", 0.2);
Jigglypuff.SetMix("idle", "walk", 0.4);

Jigglypuff.SetMix("idle", "dead", 1);
Jigglypuff.SetMix("walk", "dead", 1);
Jigglypuff.SetMix("jump", "dead", 1);