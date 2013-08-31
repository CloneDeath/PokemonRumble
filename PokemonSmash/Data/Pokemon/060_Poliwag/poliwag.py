import Pokemon;
import Color;
import Type;

Poliwag = Pokemon.Add("poliwag");
Poliwag.DisplayName = "Poliwag";
Poliwag.Animation = "Pokemon/060_Poliwag/poliwag"
Poliwag.HP = 40;
Poliwag.Attack = 50;
Poliwag.Defense = 40;
Poliwag.SpecialAttack = 40;
Poliwag.SpecialDefense = 40;
Poliwag.Speed = 90;
Poliwag.Color = Color.Blue;
Poliwag.PrimaryType = Type.Water;

Poliwag.Width = .5;
Poliwag.Height = .5;
Poliwag.Weight = 12.4;

Poliwag.CanLearn("watersport");
Poliwag.CanLearn("bubble");
Poliwag.CanLearn("hypnosis");
Poliwag.CanLearn("watergun");
Poliwag.CanLearn("doubleslap");
Poliwag.CanLearn("raindance");
Poliwag.CanLearn("bodyslam");
Poliwag.CanLearn("bubblebeam");
Poliwag.CanLearn("mudshot");
Poliwag.CanLearn("bellydrum");
Poliwag.CanLearn("wakeupslap");
Poliwag.CanLearn("hydropump");
Poliwag.CanLearn("mudbomb");

Poliwag.AddAnimationAlias("hypnosis", "idle");

Poliwag.SetMix("walk", "idle", 0.6);
Poliwag.SetMix("walk", "jump", 0.1);

Poliwag.SetMix("jump", "walk", 0.1);
Poliwag.SetMix("jump", "idle", 0.1);

Poliwag.SetMix("idle", "jump", 0.1);
Poliwag.SetMix("idle", "walk", 0.4);

Poliwag.SetMix("idle", "dead", 1);
Poliwag.SetMix("walk", "dead", 1);
Poliwag.SetMix("jump", "dead", 1);