import Move;
import Random;
import Type;
Razor = Move.Add("razorleaf");

def Attack(player):
	player.SetAnimation("leech_seed", False);
	player.Disable(0.5);
	player.Cooldown = 1;
	seed = player.AddProjectile(player.Direction * 0.3, 0, 0.1, 0.1);
	seed.SetVelocity(player.Direction * 10, 2);
	seed.CollisionMask = 0x0001;
	seed.SetSkeleton("Moves/RazorLeaf/RazorLeaf");
	seed.SetAnimation("idle", True);
	seed.Z = 0.5;
	seed.Permanent = True;
	def Razor(self, other):
		other.TakeDamage(7, player);
		self.Unload();
	seed.OnCollidePlayer = Razor;
	
	def Earth(self):
		self.Unload();
	seed.OnCollideEarth = Earth;
Razor.OnUse = Attack;
Razor.Type = Type.Grass;
Razor.Category = Type.Physical;