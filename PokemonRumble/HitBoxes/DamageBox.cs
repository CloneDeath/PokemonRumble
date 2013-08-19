using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using Box2DX.Common;
using GLImp;
using System.Diagnostics;

namespace PokemonRumble.HitBoxes {
	public class DamageBox : HitBox {
		public DamageBox(Player creator, float x, float y, float width, float height) : base(creator, x, y) {
			PolygonDef def = new PolygonDef();
			def.SetAsBox(width / 2, height / 2, new Vec2(x, y), 0);
			def.IsSensor = true;
			def.UserData = this;

			fixture = creator.body.CreateFixture(def);
			fixture.UserData = this;
		}
	}
}
