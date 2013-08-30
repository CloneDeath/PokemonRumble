import Move;
import Random;
import Type;
Ember = Move.Add("ember");

def Attack(player):
	player.SetAnimation("ember", False);
	player.Disable(0.5);
	player.Cooldown = 1;
	for i in range(Random.Next(3) + 6):
		fire = player.AddProjectile(player.Direction * 0.3, 0.3, 0.1, 0.1);
		fire.SetVelocity(player.Direction * (Random.NextDouble() + 4), 2 + (Random.NextDouble()/3));
		fire.CollisionMask = 0x0001;
		fire.SetSkeleton("Moves/Ember/Ember");
		fire.SetAnimation("idle", True);
		fire.Z = Random.NextDouble() - 0.5;
		fire.Permanent = True;
		def Hit(self, other):
			other.TakeSpecialDamage(2, player);
			self.SetAnimation("burn", True);
			self.Duration = 0.5;
			self.Permanent = False;
		fire.OnCollidePlayer = Hit;
		
		def Earth(self):
			self.Permanent = False;
			self.Unload();
		fire.OnCollideEarth = Earth;
Ember.OnUse = Attack;
Ember.Type = Type.Fire;
Ember.Category = Type.Special;