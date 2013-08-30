import Move;
import Random;
import Type;
Burst = Move.Add("flameburst");

def Attack(player):
	if (player.OnGround):
		player.SetAnimation("ember", False);
		player.Disable(1.0);
		player.Cooldown = 4.0;
		Hit = player.AddDamageBox(0, 0.2, 2.0, 2.0);
		Hit.Duration = 0.5;
		
		for i in range(10):
			Fire = player.AddProjectile(0, 0, .5, .5);
			Fire.SetVelocity((Random.NextDouble() - 0.5) * 10,
				Random.NextDouble() + 3)
			Fire.SetSkeleton("Moves/Flameburst/Flameburst");
			Fire.SetAnimation("idle", False);
			Fire.CollisionMask = 0x0001;
			def Destroy(self):
				self.Unload();
			Fire.OnCollideEarth = Destroy;
			
			def FireParticle(self, other):
				other.TakeSpecialDamage(5, player);
			Fire.OnCollidePlayer = FireParticle;
			
		def Explosion(self, other):
			other.SetVelocity(player.Direction * 2, 3);
			other.Disable(1.0);
			other.TakeSpecialDamage(15, player);
		Hit.OnCollidePlayer = Explosion;
Burst.OnUse = Attack;
Burst.Type = Type.Fire;
Burst.Category = Type.Special;