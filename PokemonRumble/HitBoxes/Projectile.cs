using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using Box2DX.Common;
using GLImp;
using System.Diagnostics;
using Box2DX.Collision;

namespace PokemonRumble.HitBoxes {
	public class Projectile : HitBox {
		Body projectile;
		Fixture fix2;

		public Projectile(Player creator, float x, float y, float width, float height) : base(creator, 0, 0) {
			/* Create New Projectile Body */
			BodyDef def = new BodyDef();
			def.IsBullet = true;
			def.Position = creator.body.GetPosition() + new Vec2(x, y);
			projectile = creator.body.GetWorld().CreateBody(def);

			/* Create a fixture for the projectile */
			PolygonDef fixdef = new PolygonDef();
			fixdef.Density = 1.0f;
			fixdef.SetAsBox(width / 2, height / 2);
			fixdef.Filter.GroupIndex = creator.ID;
			fixture = projectile.CreateFixture(fixdef);
			fixture.Filter.CategoryBits = 0x0004;
			fixture.Filter.MaskBits = 0xFFFF;

			/* Made a 2nd fixture, one to observe all collisions */
			fixdef.IsSensor = true;
			fix2 = projectile.CreateFixture(fixdef);
			fix2.UserData = this;

			/* Finally, give this projectile some mass */
			projectile.SetMassFromShapes();

			/* Also, make sure we destroy the projectile when it is time */
			this.OnDestroy += Cleanup;
		}

		void Cleanup(HitBox target) {
			projectile.DestroyFixture(fix2);
			projectile.GetWorld().DestroyBody(projectile);
		}

		public void ApplyForce(float x, float y) {
			projectile.ApplyForce(new Vec2(x, y), projectile.GetMassData().Center);
		}

		public void SetVelocity(float x, float y) {
			projectile.SetLinearVelocity(new Vec2(x, y));
		}

		public float Mass {
			get {
				return projectile.GetMass();
			}
			set {
				MassData data = projectile.GetMassData();
				data.Mass = value;
				projectile.SetMass(data);
			}
		}
	}
}
