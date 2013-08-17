from Move import *;
from Random import *;
Tackle = Move.Add("leechseed");

def Attack(player):
	if (player.OnGround):
		player.SetAnimation("leech_seed", False);
		player.Disable(1);
		player.SetVelocity(0, 0);
		seed = player.AddProjectile(0, 0, 0.1, 0.1);
		seed.SetVelocity((Random.NextDouble() + 1) * player.Direction, 3 + (Random.NextDouble() * 2));
		seed.CollisionMask = 0x0001;
		seed.SetSkeleton("Moves/LeechSeed");
		seed.SetAnimation("seed", False);
		seed.Z = Random.NextDouble() - 0.5;
		seed.Permanent = True;
		def Leech(other):
			amount = other.HP / 20;
			other.HP -= amount;
			player.HP += amount;
		seed.OnCollidePlayer = Leech;
		
		def Earth():
			seed.SetAnimation("plant", False);
		seed.OnCollideEarth = Earth;
		
			
Tackle.OnUse = Attack;