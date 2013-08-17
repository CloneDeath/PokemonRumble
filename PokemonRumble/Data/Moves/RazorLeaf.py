from Move import *;
from Random import *;
Razor = Move.Add("razorleaf");

def Attack(player):
	player.SetAnimation("leech_seed", False);
	player.Disable(0.5);
	player.Cooldown = 1;
	seed = player.AddProjectile(player.Direction * 0.3, 0, 0.1, 0.1);
	seed.SetVelocity(player.Direction * 10, 2);
	seed.CollisionMask = 0x0001;
	seed.SetSkeleton("Moves/RazorLeaf");
	seed.SetAnimation("idle", True);
	seed.Z = 0.5;
	seed.Permanent = True;
	def Leech(other):
		other.HP -= 7;
		seed.Unload();
	seed.OnCollidePlayer = Leech;
	
	def Earth():
		seed.Unload();
	seed.OnCollideEarth = Earth;
Razor.OnUse = Attack;