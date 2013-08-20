using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonSmash {
	public interface IEntity {
		void OnCollides(IEntity other);
		void OnSeperate(IEntity other);
	}
}
