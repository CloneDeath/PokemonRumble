using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gwen.Control;

namespace PokemonSmash {
	class CharacterSelect : GameState {
		string Player1 = "";
		string Player2 = "";
		public void Initialize() {
			RadioButtonGroup group = new RadioButtonGroup(MainCanvas.GetCanvas());
			group.SetPosition(10, 10);
			group.SetText("Player 1");
			foreach (var kvp in PokemonManager.Pokemons) {
			    string Name = kvp.Key;
			    Pokemon pokemon = kvp.Value;
				LabeledRadioButton option = group.AddOption(pokemon.DisplayName);
				option.UserData = pokemon;
				group.SelectionChanged += delegate(Base sender, ItemSelectedEventArgs args) {
					Player1 = ((Pokemon)group.Selected.UserData).Name;
				};
			}

			RadioButtonGroup group2 = new RadioButtonGroup(MainCanvas.GetCanvas());
			group2.SetPosition(560, 10);
			group2.SetText("Player 2");
			foreach (var kvp in PokemonManager.Pokemons) {
				string Name = kvp.Key;
				Pokemon pokemon = kvp.Value;
				LabeledRadioButton option = group2.AddOption(pokemon.DisplayName);
				option.UserData = pokemon;
				group2.SelectionChanged += delegate(Base sender, ItemSelectedEventArgs args) {
					Player2 = ((Pokemon)group2.Selected.UserData).Name;
				};
			}
			group.SetSelection(0);
			group2.SetSelection(0);

			Button start = new Button(MainCanvas.GetCanvas());
			start.SetPosition(150, 10);
			start.SetSize(400, 200);
			start.Text = "FIGHT!";
			start.Clicked += delegate(Base sender, ClickedEventArgs args) {
				Program.SwitchState(new Battle(Player1, Player2));
			};
		}

		public void Uninitialize() {
			MainCanvas.GetCanvas().DeleteAllChildren();
		}

		public void Draw(float dt) {
		}

		public void Update(float dt) {
		}
	}
}
