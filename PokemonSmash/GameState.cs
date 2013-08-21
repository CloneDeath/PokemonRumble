using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonSmash {
	interface GameState {
		void Initialize();
		void Uninitialize();
		void Draw(float dt);
		void Update(float dt);
	}
}
