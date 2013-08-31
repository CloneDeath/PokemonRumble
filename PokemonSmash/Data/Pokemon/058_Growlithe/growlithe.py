import Pokemon;
import Color;
import Type;

Growlithe = Pokemon.Add("growlithe");
Growlithe.DisplayName = "Growlithe";
Growlithe.Animation = "Pokemon/058_Growlithe/growlithe"
Growlithe.HP = 55;
Growlithe.Attack = 70;
Growlithe.Defense = 45;
Growlithe.SpecialAttack = 70;
Growlithe.SpecialDefense = 50;
Growlithe.Speed = 60;
Growlithe.Color = Color.Brown;
Growlithe.PrimaryType = Type.Fire;

Growlithe.Width = .5;
Growlithe.Height = .5;
Growlithe.Weight = 19.0;

Growlithe.CanLearn("bite");
Growlithe.CanLearn("roar");
Growlithe.CanLearn("ember");
Growlithe.CanLearn("leer");
Growlithe.CanLearn("odorsleuth");
Growlithe.CanLearn("helpinghand");
Growlithe.CanLearn("flamewheel");
Growlithe.CanLearn("reversal");
Growlithe.CanLearn("firefang");
Growlithe.CanLearn("takedown");
Growlithe.CanLearn("flameburst");
Growlithe.CanLearn("agility");
Growlithe.CanLearn("retaliate");
Growlithe.CanLearn("flamethrower");
Growlithe.CanLearn("crunch");
Growlithe.CanLearn("heatwave");
Growlithe.CanLearn("outrage");
Growlithe.CanLearn("flareblitz");

Growlithe.SetMix("walk", "idle", 0.6);
Growlithe.SetMix("walk", "jump", 0.2);

Growlithe.SetMix("jump", "walk", 0.1);
Growlithe.SetMix("jump", "idle", 0.1);

Growlithe.SetMix("idle", "jump", 0.2);
Growlithe.SetMix("idle", "walk", 0.4);

Growlithe.SetMix("idle", "dead", 1);
Growlithe.SetMix("walk", "dead", 1);
Growlithe.SetMix("jump", "dead", 1);