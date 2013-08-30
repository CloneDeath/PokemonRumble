import Move;
import Random;
import Type;
Bubble = Move.Add("bubble");
Bubble.DisplayName = "Bubble";

def ShootBubble(player):
	bubble = player.AddProjectile(player.Direction * 0.3, 0.3, 0.1, 0.1);
	bubble.SetVelocity(player.Direction * 2 * (Random.NextDouble() + 1), 0);
	bubble.CollisionMask = 0x0001;
	bubble.SetSkeleton("Moves/Bubble/Bubble");
	bubble.SetAnimation("idle", True);
	bubble.Mass = 0.001;
	bubble.Scale = 0.005;
	bubble.Duration = Random.NextDouble() + 0.5;
	
	def Hit(self, other):
		other.TakeSpecialDamage(1, player);
		self.Unload();
	bubble.OnCollidePlayer = Hit;
	
	def Earth(self):
		self.Unload();
	bubble.OnCollideEarth = Earth;
	
	def Float(self, time):
		self.ApplyForce(0, 0.008 + (Random.NextDouble() * 0.004));
			
	bubble.OnUpdate = Float;
	
	return bubble;

def Attack(player):
	player.SetAnimation("bubble", False);
	player.Disable(0.5);
	player.Cooldown = 1;
	bubble = ShootBubble(player);
	
	def Float(self, time):
		self.ApplyForce(0, 0.008 + (Random.NextDouble() * 0.004));
		if (Random.Next(5) == 0 and time < 0.5):
			for i in range(Random.Next(3) + 1):
				ShootBubble(player);
			
	bubble.OnUpdate = Float;
	
Bubble.OnUse = Attack;
Bubble.Type = Type.Water;
Bubble.Category = Type.Special;