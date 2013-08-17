from Move import *;
from Random import *;
Ember = Move.Add("ember");

def Attack(player):
	player.SetAnimation("ember", False);
	player.Disable(0.5);
	player.Cooldown = 1;
	for i in range(Random.Next(2) + 3):
		fire = player.AddProjectile(player.Direction * 0.3, 0, 0.1, 0.1);
		fire.SetVelocity(player.Direction * 10 * Random.NextDouble(), 2);
		fire.CollisionMask = 0x0001;
		fire.SetSkeleton("Moves/Ember");
		fire.SetAnimation("idle", True);
		fire.Z = Random.NextDouble() - 0.5;
		fire.Permanent = True;
		def Hit(other):
			other.TakeSpecialDamage(7, player);
			fire.SetAnimation("burn", True);
			fire.Duration = 0.5;
			fire.Permanent = False;
		fire.OnCollidePlayer = Hit;
		
		def Earth():
			fire.Permanent = False;
			fire.Unload();
		fire.OnCollideEarth = Earth;
Ember.OnUse = Attack;