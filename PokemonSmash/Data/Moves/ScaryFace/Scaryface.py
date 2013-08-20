from Move import *;
Face = Move.Add("scaryface");

def Attack(player):
	player.SetAnimation("ember", False);
	player.Disable(0.25);
	player.Cooldown = 1.0;
	
	Hit = player.AddDamageBox(player.Direction * 0.8, 0, 0.8, 0.8);
	Hit.Duration = 0.5;
	
	Hit.SetSkeleton("Moves/ScaryFace/ScaryFace");
	Hit.SetAnimation("idle", False);
	if (player.Direction < 0):
		Hit.FlipAnimation();
	def DefDown(self, other):
		other.Disable(1);
		other.Defense *= 0.9;
	Hit.OnCollidePlayer = DefDown;
Face.OnUse = Attack;