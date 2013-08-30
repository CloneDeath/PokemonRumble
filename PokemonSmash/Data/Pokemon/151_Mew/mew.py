import Pokemon;
import Move;
import Color;
import Type;

Mew = Pokemon.Add("mew");
Mew.DisplayName = "Mew";
Mew.Animation = "Pokemon/151_Mew/mew"
Mew.HP = 100;
Mew.Attack = 100;
Mew.Defense = 100;
Mew.SpecialAttack = 100;
Mew.SpecialDefense = 100;
Mew.Speed = 100;
Mew.Color = Color.Pink;
Mew.PrimaryType = Type.Psychic;

Mew.Hovers = True;

Mew.CanLearn("pound");
Mew.CanLearn("reflecttype");
Mew.CanLearn("transform");
Mew.CanLearn("megapunch");
Mew.CanLearn("metronome");
Mew.CanLearn("psychic");
Mew.CanLearn("barrier");
Mew.CanLearn("ancientpower");
Mew.CanLearn("amnesia");
Mew.CanLearn("mefirst");
Mew.CanLearn("batonpass");
Mew.CanLearn("nastyplot");
Mew.CanLearn("aurasphere");

Mew.Width = .5;
Mew.Height =  .5;
Mew.Weight = 4.0;
